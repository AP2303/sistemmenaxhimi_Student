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

namespace Student_Management_System
{
    public partial class ManageStudent : Form
    {
        // Krijimi i një instance të klases StudentClass
        StudentClass student = new StudentClass();

        public ManageStudent()
        {
            InitializeComponent();
        }

        private void ManageStudent_Load(object sender, EventArgs e)
        {
            // Shfaqja e tabelës së studentëve në ngarkim të formës
            showTable();
        }

        // Metoda për shfaqjen e të dhënave të studentëve në DataGridView
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist();
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        // Ngjarja kur përdoruesi klikon në një rresht të DataGridView
        private void DataGridView_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Mbush fushat me të dhënat e rreshtit të zgjedhur
            textBox_ID.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            textbox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            textbox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Male")
                radioButton_Male.Checked = true;

            textbox_contactnum.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            textBox_address.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[7].Value;
            MemoryStream ms = new MemoryStream(img);
            picturebox_student.Image = Image.FromStream(ms);
        }

        // Butoni për pastrimin e fushave të studentit
        private void button_clear_Click(object sender, EventArgs e)
        {
            // Pastro informacionin
            textbox_Fname.Clear();
            textBox_ID.Clear();
            textbox_Lname.Clear();
            textbox_contactnum.Clear();
            textBox_address.Clear();
            textBox_ID.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            picturebox_student.Image = null;
        }

        // Butoni për ngarkimin e fotosë së studentit
        private void button_upload_Click(object sender, EventArgs e)
        {
            // Ngarko foton
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                picturebox_student.Image = Image.FromFile(opf.FileName);
        }

        // Butoni për kërkimin e studentëve
        private void button_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchStudent(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        // Metodë për verifikimin e të dhënave të studentit
        bool verify()
        {
            if ((textbox_Fname.Text == "") || (textbox_Lname.Text == "") ||
                (textbox_contactnum.Text == "") || (textBox_address.Text == "") ||
                (picturebox_student.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Butoni për përditësimin e të dhënave të studentit
        private void button_update_Click(object sender, EventArgs e)
        {
            // Përditëso Studentin
            int id = Convert.ToInt32(textBox_ID.Text);
            string fname = textbox_Fname.Text;
            string lname = textbox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textbox_contactnum.Text;
            string address = textBox_address.Text;
            string gender = radioButton_Male.Checked ? "Male" : "Female";

            // Merr foto nga PictureBox
            MemoryStream ms = new MemoryStream();
            picturebox_student.Image.Save(ms, picturebox_student.Image.RawFormat);
            byte[] img = ms.ToArray();

            // Kontrollo moshën e studentit
            int year_born = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - year_born) < 10 || (this_year - year_born) > 100)
            {
                MessageBox.Show("Moshi i studentit duhet të jetë midis 10 dhe 100 vjet", "Invalid Date of Birth", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    if (student.updateStudent(id, fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable();
                        MessageBox.Show("Detajet e studentit u përditësuan me sukses", "Përditëso Detajet e Studentit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Fusha bosh", "Përditëso detajet e studentit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // Metodë e zbrazët, mund të shtohet kodi për fshirjen e studentit
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            // Metodë e zbrazët, mund të shtohet kodi për kërkimin e studentit
        }
    }
}
