using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace INFC20BackendFinal.Utilities
{
    public static class Utils
    {
        public static List<object> Get(Type targetType, string procedure, Dictionary<string, object> parameters, string[] exceptionNames)
        {
            List<object> tuples = new List<object>();
            if (targetType != null && procedure != null)
            {
                using (SqlConnection con = Connector.Connect())
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                            foreach (var entry in parameters)
                                cmd.Parameters.AddWithValue(entry.Key, entry.Value);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read() && dataReader.HasRows)
                            {
                                var target = Activator.CreateInstance(targetType);
                                foreach (var prop in target.GetType().GetProperties()) // <--- ERROR
                                {
                                    if (prop != null && prop.CanWrite && IsPropAllowedOnType(prop, exceptionNames))
                                    {
                                        try
                                        {
                                            prop.SetValue(target, dataReader[prop.Name], null); // <--- ERROR
                                        }
                                        catch (IndexOutOfRangeException e)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                tuples.Add(target);
                            }
                        }
                    }
                }
            }
            return tuples;
        }

        public static object InsertEntity(object entity, string procedure, string[] exceptionParams)
        {
            if (entity != null && procedure != null)
            {
                Dictionary<string, object> parameters = Utils.GetParams(entity, exceptionParams);
                object newId = Utils.Insert(procedure, parameters, true);
                return newId;
            }
            return null;
        }

        public static void UpdateEntity(object entity, string procedure, string[] exceptionParams)
        {
            if (entity != null && procedure != null)
            {
                Dictionary<string, object> parameters = Utils.GetParams(entity, exceptionParams);
                Utils.Insert(procedure, parameters, false);
            }
        }

        public static object Insert(string procedure, Dictionary<string, object> parameters, bool returnNewId = false)
        {
            if (procedure != null && parameters != null)
            {
                using (SqlConnection con = Connector.Connect())
                {
                    using (SqlCommand cmd = new SqlCommand(procedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var entry in parameters)
                            cmd.Parameters.AddWithValue(entry.Key, entry.Value);


                        if (returnNewId)
                        {

                            SqlParameter newId = new SqlParameter("@NewIdentity", SqlDbType.Int);
                            newId.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(newId);
                            cmd.ExecuteNonQuery();
                            return newId.Value.ToString();

                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }

            return null;
        }

        private static bool IsPropAllowedOnType(PropertyInfo prop, string[] exceptions)
        {
            if (exceptions != null && exceptions.Contains(prop.Name))
            {
                return false;
            }
            else
            {
                return true; 
            }
        }

        public static Dictionary<string, object> GetParams(object target, string[] exceptions)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (target != null)
            {
                foreach (var prop in target.GetType().GetProperties())
                {
                    if (exceptions == null)
                        parameters.Add(prop.Name, prop.GetValue(target, null));

                    else if (exceptions != null && !exceptions.Contains<string>(prop.Name))
                        parameters.Add(prop.Name, prop.GetValue(target, null));

                    else continue;
                }
            }
            return parameters;
        }
    }
}