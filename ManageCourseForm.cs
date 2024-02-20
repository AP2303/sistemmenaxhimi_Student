using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Student_Management_System
{
    public partial class ManageCourseForm : Form
    {
        // Krijimi i një instance të klases CourseClass
        CourseClass course = new CourseClass();

        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            // Shfaqja e tabelës së kurseve në ngarkim të formës
            showTable();
        }

        // Metoda për shfaqjen e të dhënave të kurseve në DataGridView
        private void showTable()
        {
            DataGridView_courses.DataSource = course.GetCourse(new MySqlCommand("SELECT * FROM `courses`"));
        }

        // Butoni për pastrimin e fushave të kurseve
        private void button_clearcourse_Click(object sender, EventArgs e)
        {
            textBox_courseID.Clear();
            textbox_coursename.Clear();
            textbox_coursedur.Clear();
            textBox_coursedes.Clear();
        }

        // Butoni për përditësimin e të dhënave të një kursi
        private void button_updatecourse_Click(object sender, EventArgs e)
        {
            // Kontrollon nqs fushat e nevojshme janë të plotësuara
            if (textbox_coursename.Text == "" || textbox_coursedur.Text == "" || textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Ju lutem plotësoni të dhënat e kursit", "Gabim në fusha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Marrja e vlerave nga fushat
                int id = Convert.ToInt32(textBox_courseID.Text);
                string Cname = textbox_coursename.Text;
                int dur = Convert.ToInt32(textbox_coursedur.Text);
                string desc = textBox_coursedes.Text;

                // Përditëson të dhënat e kursit në databazë
                if (course.UpdateCourse(id, Cname, dur, desc))
                {
                    showTable();
                    button_clearcourse.PerformClick();
                    MessageBox.Show("Kursi u përditësua me sukses", "Përditëso Kursin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gabim gjatë përditësimit të kursit", "Përditëso Kursin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Butoni për fshirjen e një kursi
        private void button_deletecourse_Click(object sender, EventArgs e)
        {
            // Kontrollon nqs është vendosur ID e kursit
            if (textBox_courseID.Text.Equals(""))
            {
                MessageBox.Show("Ju lutem vendosni ID e kursit", "Gabim në fusha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    // Marrja e vlerave nga fushat
                    int id = Convert.ToInt32(textBox_courseID.Text);
                    string Cname = textbox_coursename.Text;
                    int dur = Convert.ToInt32(textbox_coursedur.Text);
                    string desc = textBox_coursedes.Text;

                    // Fshirja e kursit nga databaza
                    if (course.deleteCourse(id))
                    {
                        showTable();
                        button_clearcourse.PerformClick();
                        MessageBox.Show("Kursi u fshi me sukses", "Fshi Kursin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fshi Kursin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Ngjarja kur përdoruesi klikon në një rresht të DataGridView
        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Mbush fushat me të dhënat e rreshtit të zgjedhur
            textBox_courseID.Text = DataGridView_courses.CurrentRow.Cells[0].Value.ToString();
            textbox_coursename.Text = DataGridView_courses.CurrentRow.Cells[1].Value.ToString();
            textbox_coursedur.Text = DataGridView_courses.CurrentRow.Cells[2].Value.ToString();
            textBox_coursedes.Text = DataGridView_courses.CurrentRow.Cells[3].Value.ToString();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            // Metodë e zbrazët, mund të shtohet kodi për kërkimin e kursit
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            // Metodë e zbrazët, mund të shtohet kodi për kërkimin e kursit
        }
    }
}
