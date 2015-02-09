using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Collections;
using Utilities;
using System.Data;

namespace DataAccessLayer { 
  

    public class DataAccess
    {
        private static IPerRequestDataStorage _transactionStorage = new HttpContextPerRequestDataStorage();

        private static DbTransaction Transaction
        {
            get
            {
                if (_transactionStorage.Items.Contains("DbTransaction"))
                    return (DbTransaction)_transactionStorage.Items["DbTransaction"];
                else
                    return null;
            }
            set
            {
                _transactionStorage.Items["DbTransaction"] = value;
            }
        }

	    public DataAccess()
	    {
		
	    }

        public static DataTable ExecuteSelect(string sqlString)
        {
            DbCommand dbCommand = DataAccess.CreateTextCommand();
            dbCommand.CommandText = sqlString;
            return DataAccess.ExecuteSelect(dbCommand);
        }

        public static DbCommand CreateTextCommand()
        {
            return DataAccess.CreateCommand();
        }

        public static DbParameter CreateParameterString(string value)
        {
            DbParameter dbParameter = DataAccess.DbFactory.CreateParameter();
            dbParameter.DbType = DbType.String;
            dbParameter.Value = value;
            return dbParameter;
        }


        public static DataTable ExecuteSelect(DbCommand command, params DbParameter[] parameters)
        {
            DataTable result;
            try
            {
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
                //if (DataAccessHelper.ShowVerboseError)
                //{
                //    throw DataAccess.CreateVerboseException(ex, command, parameters);
                //}
                throw ex;
            }
            return result;
        }

        public static DataTable ExecuteSelectWithTransaction(DbCommand command, params DbParameter[] parameters)
        {
            command.Transaction = DataAccess.Transaction;
            return DataAccess.ExecuteSelectImplementation(command, parameters);
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
                //if (DataAccessHelper.LogError != null)
                //{
                //    DataAccessHelper.LogError(ex);
                //}
                throw ex;
            }
            return dataTable;
        }


        public static int ExecuteNonQueryNoParameter(DbCommand command)
        {
            if (Transaction == null)
                return ExecuteNonQueryNoParameterNoTransaction(command);
            else
                return ExecuteNonQueryNoParameterWithTransaction(command);
        }

        private static int ExecuteNonQueryNoParameterWithTransaction(DbCommand command)
        {
            command.Transaction = Transaction;
            return ExecuteNonQueryNoParameterImplementation(command);
        }


        // ExecuteNonQueryNoParameter-Related Functions
        private static int ExecuteNonQueryNoParameterImplementation(DbCommand command)
        {
            int affectedRows = -1;
            string commandTextLine = String.Empty;
            try
            {
                DbCommand[] commandList = SplitBatchCommandNoParameter(command);

                foreach (DbCommand current in commandList)
                {
                    commandTextLine = current.CommandText;
                    current.CommandTimeout = 300;
                    affectedRows = current.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //DataAccessHelper.LogError(ex);
                throw new Exception(ex.Message + "<br/> Command :" + commandTextLine);
            }
            return affectedRows;
        }

        private static int ExecuteNonQueryNoParameterNoTransaction(DbCommand command)
        {
            int affectedRows = -1;
            try
            {
                command.Connection.Open();
                affectedRows = ExecuteNonQueryNoParameterImplementation(command);
            }
            finally
            {
                command.Connection.Close();
            }

            return affectedRows;
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
    }

}