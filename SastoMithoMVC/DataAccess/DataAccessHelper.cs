using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace SastoMithoMVC.DataAccess
{
    public static class DataAccessHelper
    {
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        public static IDbConnection Connection1()
        {
            return new System.Data.SqlClient.SqlConnection(CnnVal("SastoMitho"));
        }
    }
}