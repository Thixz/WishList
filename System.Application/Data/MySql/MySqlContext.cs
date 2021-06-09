using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Application.Data.MySql
{
    public class MySqlContext
    {
        private readonly string strConnection = "Server=localhost;DataBase=wishlist;Uid=root;Pwd=;SslMode=none";
        private MySqlConnection connection;

        public MySqlConnection Conectar()
        {
            connection = new MySqlConnection(strConnection);
            connection.Open();
            return connection;
        }
    }
}
