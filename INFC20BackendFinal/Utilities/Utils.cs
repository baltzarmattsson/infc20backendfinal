using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Utilities
{
    public static class Utils
    {
        public static List<object> Get(Type targetType, string procedure, Dictionary<string, object> parameters)
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
                                foreach (var prop in target.GetType().GetProperties())
                                {
                                    if (prop != null && prop.CanWrite)
                                    {
                                        try
                                        {
                                            prop.SetValue(target, dataReader[prop.Name], null);
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

        public static void InsertEntity(object entity, string procedure, string[] exceptionParams)
        {
            if (entity != null && procedure != null)
            {
                Dictionary<string, object> parameters = Utils.GetParams(entity, exceptionParams);
                Utils.Insert(procedure, parameters);
            }
        }

        public static void Insert(string procedure, Dictionary<string, object> parameters)
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

                        cmd.ExecuteNonQuery();
                    }
                }
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