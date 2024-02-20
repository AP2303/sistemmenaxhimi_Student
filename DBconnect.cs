using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Student_Management_System
{
    /*
     * Lidhja mes aplikacionit dhe bazës së të dhënave MySQL
     */
    class DBconnect
    {
        // Krijimi i lidhjes MySQL
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=studentdb");

        // Merrja e lidhjes
        public MySqlConnection getconnection
        {
            get
            {
                return connect;
            }
        }

        // Funksioni për hapjen e lidhjes
        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        // Funksioni për mbylljen e lidhjes
        public void closeConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
    }
}
