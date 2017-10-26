using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class ExceptionHandler
    {
        public const int PrimaryKey = 2627;
        public const int ForeignKey = 547;
        public const int DataWouldBeTruncated = 8152;
        public const int SomethingIsNull = 515;

        public static string HandleSqlException(SqlException sqle)
        {
            string message = "Unknown error, please contact system administrator";
            string[] split = sqle.Message.Split('-');
            if (split.Length > 0)
            {
                //try
                //{
                    int sqlNbr = Convert.ToInt32(split[0].Trim());

                    switch (sqlNbr)
                    {
                        case PrimaryKey:
                            var regmatch = Regex.Match(sqle.Message, "(?<=\\()(.*?)(?=\\))").Groups[0]; //finds (Names within paranthesis)
                            string duplicateValue = regmatch.Captures[0].ToString();
                            message = string.Format("There's already a item with value {0}, please choose another", duplicateValue);
                            break;
                        case ForeignKey:
                            //Getting tablename
                            var tableRegmatch = Regex.Match(sqle.Message, "(?<=table \\\")(.*?)(?=\\\")").Groups[0]; //Finds tablename within \" \", like \"dbo.Person\"
                            string table = tableRegmatch.Captures[0].ToString();
                            int indexOfLastDot = table.LastIndexOf('.');
                            if (indexOfLastDot != -1)
                                table = table.Substring(indexOfLastDot + 1, (table.Length - 1) - indexOfLastDot); //Extracts tablename: dbo.Person -> Person

                            //Getting column name
                            var columnRegmatch = Regex.Match(sqle.Message, "(?<=column ')(.*?)(?=')"); //Finds columnname within ' ', like 'name' or 'bName'
                            string column = columnRegmatch.Captures[0].ToString();

                            message = string.Format("Kunde inte hitta {0} med {1}, vänligen försök igen", table.ToLower(), column.ToLower());
                            break;

                        case DataWouldBeTruncated:
                            message = "A value is too long, please try again";
                            break;

                        case SomethingIsNull:
                            columnRegmatch = Regex.Match(sqle.Message, "(?<=column ')(.*?)(?=')");
                            column = columnRegmatch.Captures[0].ToString();
                            message = String.Format("The field \"{0}\" is empty, please try again", column);
                            break;

                        default:
                            throw sqle;
                    }
                //} catch (FormatException) { }
            }
            return message;
        }
    }
}