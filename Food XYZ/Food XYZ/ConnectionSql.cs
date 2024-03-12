using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_XYZ
{
    public sealed class ConnectionSql
    {
        private static readonly string ConnectionString = @"Data Source = DESKTOP-1HS4PIK\SQLEXPRESS02; Initial Catalog=db_foodXYZ; Integrated Security=true";
        private static SqlConnection _connection;
        private static readonly object LockObject = new object();

        private ConnectionSql()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        public static SqlConnection GetConnection()
        {
             if(_connection == null)
            {
                lock (LockObject)
                {
                    if(_connection == null)
                    {
                        _connection = new SqlConnection(ConnectionString);
                    }
                }
            }
             return _connection;
        }

        public static void OpenConnection()
        {
            if (GetConnection().State != System.Data.ConnectionState.Open)
            {
                GetConnection().Open();
            }
        }

        public static void CloseConnection()
        {
            if (GetConnection().State != System.Data.ConnectionState.Closed)
            {
                GetConnection().Close();
            }
        }
    }
}

/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SMK_NUSANTARA
{
    public sealed class ConnectionSql
    {
        private static readonly string ConnectionString = @"Data Source=DESKTOP-1HS4PIK\SQLEXPRESS02;Initial Catalog=smk_nusantara;Integrated Security=True";
        private static SqlConnection _connection;
        private static readonly object LockObject = new object();

        private ConnectionSql()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        public static SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                lock (LockObject)
                {
                    if (_connection == null)
                    {
                        _connection = new SqlConnection(ConnectionString);
                    }
                }     
            }
            return _connection;
        }

        public static void OpenConnection()
        {
            if(GetConnection().State != System.Data.ConnectionState.Open)
            {
                GetConnection().Open();
            }
        }
         
        public static void CloseConnection()
        {
            if(GetConnection().State != System.Data.ConnectionState.Closed)
            {
                GetConnection().Close();
            }
        }
    }
}

 
 
 */
