using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Collections;
using Utilities;
using System.Data;
using System.Data.SqlClient;
using Config = System.Configuration.ConfigurationManager;

namespace DataAccessLayer
{
    /// <summary>
    /// DataAccess 的摘要描述
    /// </summary>
    public class DataAccess
    {
        private static Iface.IPerRequestDataStorage _transactionStorage;

        private static DbTransaction Transaction
        {
            get
            {
                DbTransaction result;
                if (DataAccess._transactionStorage.Items.Contains("DbTransaction"))
                {
                    result = (DbTransaction)DataAccess._transactionStorage.Items["DbTransaction"];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                DataAccess._transactionStorage.Items["DbTransaction"] = value;
            }
        }

        static DataAccess()
        {
            DataAccess._transactionStorage = new SystemServices.HttpContextPerRequestDataStorage();
        }

        private static DbCommand[] SplitBatchCommandNoParameter(DbCommand command)
        {
            string commandText = command.CommandText;
            ArrayList arrayList = new ArrayList();
            string[] array = commandText.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < array.Length; i++)
            {
                DbCommand dbCommand = command.Connection.CreateCommand();
                dbCommand.Connection = command.Connection;
                dbCommand.Transaction = command.Transaction;
                dbCommand.CommandType = command.CommandType;
                dbCommand.CommandText = array[i] + ";";
                arrayList.Add(dbCommand);
            }
            DbCommand[] array2 = new DbCommand[arrayList.Count];
            arrayList.CopyTo(array2);
            return array2;
        }

        private static DbCommand[] SplitBatchCommand(DbCommand command, DbParameter[] parameters)
        {
            string commandText = command.CommandText;
            ArrayList arrayList = new ArrayList();
            string[] array = commandText.Split(new char[]
			{
				';'
			});
            int num = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Trim() != "")
                {
                    DbCommand dbCommand = command.Connection.CreateCommand();
                    dbCommand.Connection = command.Connection;
                    dbCommand.Transaction = command.Transaction;
                    dbCommand.CommandType = command.CommandType;
                    dbCommand.CommandText = array[i] + ";";
                    int numberOfParameters = DataAccess.GetNumberOfParameters(array[i]);
                    string[] array2 = null;
                    if (numberOfParameters != 0)
                    {
                        array2 = DataAccess.GetParameterNames(array[i].Trim());
                    }
                    for (int j = 0; j < numberOfParameters; j++)
                    {
                        parameters[num].ParameterName = array2[j];
                        dbCommand.Parameters.Add(parameters[num]);
                        num++;
                    }
                    arrayList.Add(dbCommand);
                }
            }
            DbCommand[] array3 = new DbCommand[arrayList.Count];
            arrayList.CopyTo(array3);
            return array3;
        }

        private static string[] GetParameterNames(string sql)
        {
            ArrayList arrayList = new ArrayList();
            TokenProcessor tokenProcessor = new TokenProcessor(sql);
            tokenProcessor.SkipTo('@');
            while (!tokenProcessor.IsEndOfString())
            {
                tokenProcessor.SeekRelative(1);
                if (tokenProcessor.GetRelativeChar(0) != '@')
                {
                    arrayList.Add(tokenProcessor.ExtractTo(TokenProcessor.CharType.NonLetterDigit));
                }
                tokenProcessor.SkipTo('@');
            }
            string[] array = new string[arrayList.Count];
            arrayList.CopyTo(array);
            return array;
        }

        private static int GetNumberOfParameters(string sql)
        {
            int num = StringUtilities.CountOccurrencesInString(sql, '@');
            int num2 = StringUtilities.CountOccurrencesInString(sql, "@@");
            return num - 2 * num2;
        }

        public static DataTable ExecuteSelect(string sqlString)
        {
            DbCommand dbCommand = new SqlCommand();
            dbCommand.CommandText = sqlString;
            return DataAccess.ExecuteSelect(dbCommand);
        }

        public static DataTable ExecuteSelect(string sqlString, params DbParameter[] parameters)
        {
            DbCommand dbCommand = new SqlCommand();
            dbCommand.CommandText = sqlString;
            return DataAccess.ExecuteSelect(dbCommand, parameters);
        }

        public static DataTable ExecuteSelect(DbCommand command)
        {
            return DataAccess.ExecuteSelect(command, null);
        }

        public static DataTable ExecuteSelect(DbCommand command, params DbParameter[] parameters)
        {
            DataTable result;
            try
            {
                command.Connection = new SqlConnection(Config.ConnectionStrings["StoreConnection"].ConnectionString);
              
                if (DataAccess.Transaction == null)
                {
                    result = DataAccess.ExecuteSelectNoTransaction(command, parameters);
                }
                else
                {
                    result = DataAccess.ExecuteSelectWithTransaction(command, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static DataTable ExecuteSelectNoTransaction(DbCommand command, params DbParameter[] parameters)
        {
            DataTable result = null;
            try
            {
                command.Connection.Open();
                result = DataAccess.ExecuteSelectImplementation(command, parameters);
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        public static DataTable ExecuteSelectWithTransaction(DbCommand command, params DbParameter[] parameters)
        {
            command.Transaction = DataAccess.Transaction;
            return DataAccess.ExecuteSelectImplementation(command, parameters);
        }

        public static DataTable ExecuteSelectImplementation(DbCommand command, params DbParameter[] parameters)
        {
            DataTable dataTable;
            try
            {
                DbCommand[] array = DataAccess.SplitBatchCommand(command, parameters);
                DbDataReader dbDataReader = null;
                DbCommand[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    DbCommand dbCommand = array2[i];
                    if (dbDataReader != null)
                    {
                        dbDataReader.Close();
                    }
                    dbCommand.CommandTimeout = 300;
                    dbDataReader = dbCommand.ExecuteReader();
                }
                dataTable = new DataTable();
                dataTable.Load(dbDataReader);
                dbDataReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public static DbParameter CreateParameterString(string value)
        {
            DbParameter dbParameter = new SqlParameter();
            dbParameter.DbType = DbType.String;
            dbParameter.Value = value;
            return dbParameter;
        }
        

    }

}