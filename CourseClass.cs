using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Student_Management_System
{
    class CourseClass
    {
        // Krijo një objekt të lidhjes me bazën e të dhënave
        DBconnect connect = new DBconnect();

        // Krijo një funksion për futjen e kursit
        public bool InsertCourse(string Cname, int dur, string desc)
        {
            // Përgatiti komandën SQL për futjen e të dhënave në tabelën 'courses'
            MySqlCommand command = new MySqlCommand("INSERT INTO `courses`(`Course Name`, `Course Duration`, `Description`) VALUES (@cn, @cd, @desc)", connect.getconnection);
            
            // Vendos vlerat e parametrave në komandë
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = Cname;
            command.Parameters.Add("@cd", MySqlDbType.Int32).Value = dur;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;

            // Hap lidhjen me bazën e të dhënave
            connect.openConnect();

            // Kontrollo nëse futja e të dhënave ka përfunduar me sukses
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

        // Krijo një funksion për të marrë listën e kurseve
        public DataTable GetCourse(MySqlCommand command)
        {
            // Vendos lidhjen e komandës me bazën e të dhënave
            command.Connection = connect.getconnection;
            
            // Krijo një adapter për të marrë të dhënat nga tabela
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            
            // Krijo një tabelë për të mbajtur të dhënat
            DataTable table = new DataTable();
            
            // Mbush tabelën me të dhënat nga adapteri
            adapter.Fill(table);

            // Kthe tabelën me të dhënat
            return table;
        }

        // Krijo një funksion për përditësimin e kurseve
        public bool UpdateCourse(int id, string Cname, int dur, string desc)
        {
            // Përgatiti komandën SQL për përditësimin e të dhënave në tabelën 'courses'
            MySqlCommand command = new MySqlCommand("UPDATE `courses` SET `Course Name`=@cn, `Course Duration`=@dur, `Description`=@desc WHERE `Course ID`=@id", connect.getconnection);
            
            // Vendos vlerat e parametrave në komandë
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = Cname;
            command.Parameters.Add("@dur", MySqlDbType.Int32).Value = dur;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            // Hap lidhjen me bazën e të dhënave
            connect.openConnect();

            // Kontrollo nëse përditësimi ka përfunduar me sukses
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

        // Krijo një funksion për të fshirë të dhënat e një kursi
        public bool deleteCourse(int id)
        {
            // Përgatiti komandën SQL për të fshirë të dhënat nga tabela 'courses'
            MySqlCommand command = new MySqlCommand("DELETE FROM `courses` WHERE `Course ID`=@id", connect.getconnection);
            
            // Vendos vlerën e parametrit në komandë
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            // Hap lidhjen me bazën e të dhënave
            connect.openConnect();

            // Kontrollo nëse fshirja ka përfunduar me sukses
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
    }
}
