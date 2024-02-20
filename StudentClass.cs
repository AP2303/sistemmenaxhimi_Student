using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_Management_System
{
    class StudentClass
    {
        DBconnect connect = new DBconnect(); // Krijo një instancë të klases DBconnect për të lidhur me bazën e të dhënave

        // Funksioni për të shtuar një student të ri në bazën e të dhënave
        public bool insertStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`First Name`, `Last Name`, `D.O.B`, `Gender`, `Contact Number`, `Address`, `Photo`) VALUES(@fn, @ln, @db, @gd, @ph, @adr, @img)", connect.getconnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@db", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        // Funksioni për të marrë listën e studentëve nga tabela
        public DataTable getStudentlist()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        // Funksioni për të ekzekutuar një query për numërim (p.sh., totali i studentëve, numri i studentëve mashkull, numri i studentëve femër)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }

        // Funksioni për të marrë numrin total të studentëve
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student");
        }

        // Funksioni për të marrë numrin e studentëve mashkull
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM `student` WHERE `Gender`= 'Male'");
        }

        // Funksioni për të marrë numrin e studentëve femër
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM `student` WHERE `Gender`= 'Female'");
        }

        // Funksioni për të kërkuar studentët në bazë të një fjalë kyçe
        public DataTable searchStudent(string searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT (`First Name`,`Last Name`,`Address`) LIKE '%" + searchdata + "%'", connect.getconnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        // Funksioni për të përditësuar informacionin e një studenti
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`First Name`, `Last Name`, `D.O.B`, `Gender`, `Contact Number`, `Address`, `Photo`) VALUES(@fn, @ln, @db, @gd, @ph, @adr, @img)", connect.getconnection);

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@db", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }

        // Funksioni për të marrë një listë të dhënash në bazë të një komande të caktuar
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
