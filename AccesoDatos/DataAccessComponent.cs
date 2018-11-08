using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace AccesoDatos
{
    public abstract class DataAccessComponent

    {
        protected const string ConnectionName = "DefaultConnection";
        protected const string ConnectionNameMaster = "DefaultConnectionMaster";

        static DataAccessComponent()
        {
            // Enterprise Library DAAB 6.0 - Database Factories.
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(), false);
        }

        protected int PageSize => Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
       
        protected static T GetDataValue<T>(IDataReader dr, string columnName)
        {
            var i = dr.GetOrdinal(columnName);

            if (!dr.IsDBNull(i))
                return (T)dr.GetValue(i);
            return default(T);
        }

        protected string FormatFilterStatement(string filter)
        {
            return Regex.Replace(filter, "^(AND|OR)", string.Empty);
        }
    }

}