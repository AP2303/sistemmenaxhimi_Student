using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_Management_System
{
    public partial class AddCourse : Form
    {
        // Krijo një objekt të klases për menaxhimin e kurseve
        CourseClass course = new CourseClass();

        // Konstruktori i formës
        public AddCourse()
        {
            InitializeComponent();
        }

        // Ngarkohet kur forma ngarkohet
        private void AddCourse_Load(object sender, EventArgs e)
        {
            // për të shfaqur listën e kurseve
            showTable();
        }

        // Funksioni për të shfaqur tabelën e kurseve
        private void showTable()
        {
            DataGridView_courses.DataSource = course.GetCourse(new MySqlCommand("SELECT * FROM `courses`"));
        }

        // Ngjarja e shtypjes së butonit për shtimin e një kursi të ri
        private void button_addcourse_Click(object sender, EventArgs e)
        {
            if (textbox_coursename.Text == "" || textbox_coursedur.Text == "")
            {
                showTable();
                MessageBox.Show("Ju lutem vendosni detajet e kursit", "Gabim në Fushë", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Merr vlerat nga kutitë e tekstit
                string Cname = textbox_coursename.Text;
                int dur = Convert.ToInt32(textbox_coursedur.Text);
                string desc = textBox_coursedes.Text;

                // Provo të shtosh kursin dhe kthe statusin e shtimit
                if (course.InsertCourse(Cname, dur, desc))
                {
                    // Pastron kutitë e tekstit
                    button_clearcourse.PerformClick();
                    MessageBox.Show("Kursi i ri është shtuar me sukses", "Shto Kurs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gabim në shtimin e kursit", "Shto Kurs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ngjarja e shtypjes së butonit për pastrimin e të dhënave në kutitë e tekstit
        private void button_clearcourse_Click(object sender, EventArgs e)
        {
            textbox_coursename.Clear();
            textbox_coursedur.Clear();
            textBox_coursedes.Clear();
        }

        // Ngjarja e klikimit të një qelie në DataGridView
        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vendos vlerat e qelisë së selektuar në kutitë e tekstit për editim
            textbox_coursename.Text = DataGridView_courses.CurrentRow.Cells[1].Value.ToString();
            textbox_coursedur.Text = DataGridView_courses.CurrentRow.Cells[2].Value.ToString();
            textBox_coursedes.Text = DataGridView_courses.CurrentRow.Cells[2].Value.ToString();
        }
    }
}
