using INFC20BackendFinal.Models;
using INFC20BackendFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class UserDAL
    {
        private static Type type = new User().GetType();
        private static Dictionary<string, object> parameters;
        private static string procedure;
        private static string[] exceptionParams;

        public static User GetUser(string email)
        {
            procedure = UserProcedure.USP_GET_USER.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("Email", email);

            return Utils.Get(type, procedure, parameters, exceptionParams).FirstOrDefault() as User;
        }

        public static void AddUser(User user)
        {
            procedure = UserProcedure.USP_ADD_USER.ToString();
            Utils.InsertEntity(user, procedure, new string[] { "NbrOfBids" }, false);
        }

        public static void UpdateUser(User user)
        {
            procedure = UserProcedure.USP_UPDATE_USER.ToString();
            Utils.InsertEntity(user, procedure, new string[] { "NbrOfBids" }, false);
        }

        public static void RemoveUser(User user)
        {
            procedure = UserProcedure.USP_REMOVE_USER.ToString();
            Utils.InsertEntity(user, procedure, exceptionParams);
        }

        public static void RemoveUser(string email)
        {
            procedure = UserProcedure.USP_REMOVE_USER.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("Email", email);

            Utils.Insert(procedure, parameters);
        }
    }
}