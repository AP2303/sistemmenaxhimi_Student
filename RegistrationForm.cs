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
    public partial class RegistrationForm : Form
    {
        StudentClass student = new StudentClass(); // Krijo një instancë të klases StudentClass

        public RegistrationForm()
        {
            InitializeComponent();
        }

        // Ngjarja kur përdoruesi klikon butonin "Ngarko Foto"
        private void button_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                picturebox_student.Image = Image.FromFile(opf.FileName);
        }

        // Ngjarja kur përdoruesi klikon butonin "Shto Student"
        private void button_add_Click_1(object sender, EventArgs e)
        {
            // Merr të dhënat nga fushatë në formë
            string fname = textbox_Fname.Text;
            string lname = textbox_Lname.Text;
            DateTime bdate = dateTimePicker1.Value;
            string phone = textbox_contactnum.Text;
            string address = textBox_address.Text;
            string gender = radioButton_Male.Checked ? "Male" : "Female";

            // Merr fotografinë nga picturebox
            MemoryStream ms = new MemoryStream();
            picturebox_student.Image.Save(ms, picturebox_student.Image.RawFormat);
            byte[] img = ms.ToArray();

            // Verifikon nëse të gjitha fushat janë të mbushura
            if (verify())
            {
                try
                {
                    // Thirr metoda insertStudent e klases StudentClass për të shtuar studentin në bazën e të dhënave
                    if (student.insertStudent(fname, lname, bdate, gender, phone, address, img))
                    {
                        showTable(); // Rifresko datagridview
                        MessageBox.Show("Studenti i ri u shtua me sukses", "Shto Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Gabim", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Fusha e zbrazët", "Shto detaje të studentit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Funksioni për të verifikuar nëse të gjitha fushat janë të mbushura
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

        // Ngjarja e ngarkuar kur hapet forma
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            showTable(); // Shfaq listën e studentëve në DataGridView
        }

        // Shfaq listën e studentëve në DataGridView
        public void showTable()
        {
            DataGridView_student.DataSource = student.getStudentlist();
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        // Ngjarja e ngarkuar kur përdoruesi klikon butonin "Pastro"
        private void button_clear_Click(object sender, EventArgs e)
        {
            // Pastro të gjitha fushat
            textbox_Fname.Clear();
            textbox_Lname.Clear();
            textbox_contactnum.Clear();
            textBox_address.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            picturebox_student.Image = null;
        }
    }
}
