using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Collections;
using Utilities;

namespace DataAccessLayer { 
  

    public class DataAccess
    {
	    public DataAccess()
	    {
		
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