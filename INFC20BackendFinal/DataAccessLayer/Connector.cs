using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class Connector
    {
        private static readonly string username = "sadmin",
                                       password = "super",
                                       database = "infc20",
                                       //server = "DESKTOP-MNFMFBJ\\SQLEXPRESS", //DESKTOP-MNFMFBJ\SQLEXPRESS
                                       //server = "DESKTOP-18HO2QU\\SQLEXPRESS", // Baltzar laptop
                                       server = "DESKTOP-VFTNTHL", // Baltzar desktop
                                       url = "user id = " + username + ";" +
                                             "password = " + password + ";" +
                                             "server = " + server + ";" +
                                             "database = " + database + ";" +
                                             "connection timeout = 10;";

        public static SqlConnection Connect()
        {
            try
            {
                SqlConnection con = new SqlConnection(url);
                con.Open();
                //Console.WriteLine("Connection opened");
                return con;
            }
            catch (SqlException se)
            {
                //Console.WriteLine(se.Message);
                throw se;
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                throw e;
                Environment.Exit(0);
            }
            return null;
        }
    }
}