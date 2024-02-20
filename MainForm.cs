using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management_System
{
    public partial class MainForm : Form
    {
        // Krijimi i një instance të klases StudentClass
        StudentClass student = new StudentClass();

        public MainForm()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Shfaqja e vlerave për numrin total të studentëve
            label_total_students.Text = "Numri i Studentëve:  " + student.totalStudent();
            label_male.Text = "Meshkuj:  " + student.maleStudent();
            label_female.Text = "Femra:  " + student.femaleStudent();
        }

        private void panel_slide_Paint(object sender, PaintEventArgs e)
        {
            // Metodë e zbrazët
        }

        private void customizeDesign()
        {
            // Përcakton dizajnin e panelave të nënmenysë në fillim
            panel_stdsubmenu.Visible = false;
            panel_coursesubmenu.Visible = false;
            panel_gradesubmenu.Visible = false;
        }

        private void hideSubmenu()
        {
            // Fshin të gjitha nënmenytë
            if (panel_stdsubmenu.Visible == true)
                panel_stdsubmenu.Visible = false;
            if (panel_coursesubmenu.Visible == true)
                panel_coursesubmenu.Visible = false;
            if (panel_gradesubmenu.Visible == true)
                 panel_gradesubmenu.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            // Shfaq ose fsheh një nënmenu duke e bërë të dukshme ose jo
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void button_std_Click(object sender, EventArgs e)
        {
            // Shfaq nënmenunë e studentit
            showSubmenu(panel_stdsubmenu);
        }

        #region StdSubmenu
        private void button_registration_Click(object sender, EventArgs e)
        {
            // Hap formën për regjistrim të studentit
            openChildForm(new RegistrationForm());
            hideSubmenu();
        }

        private void button_stdmanagement_Click(object sender, EventArgs e)
        {
            // Hap formën për menaxhimin e studentëve
            openChildForm(new ManageStudent());
            hideSubmenu();
        }

        private void button_status_Click(object sender, EventArgs e)
        {
            // Fshin nënmenunë
            hideSubmenu();
        }

        private void button_printstd_Click(object sender, EventArgs e)
        {
            // Hap formën për printimin e informacionit të studentit
            openChildForm(new PrintStudent());
            hideSubmenu();
        }
        #endregion StdSubmenu

        private void button_course_Click(object sender, EventArgs e)
        {
            // Shfaq nënmenunë e kursit
            showSubmenu(panel_coursesubmenu);
        }

        #region CourseSubmenu
        private void button_addcourse_Click(object sender, EventArgs e)
        {
            // Hap formën për shtimin e një kursi të ri
            openChildForm(new AddCourse());
            hideSubmenu();
        }

        private void button_printcourse_Click(object sender, EventArgs e)
        {
            // Fshin nënmenunë
            hideSubmenu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Hap formën për menaxhimin e kurseve
            openChildForm(new ManageCourseForm());
            hideSubmenu();
        }
        #endregion CourseSubmenu

        private void button_grades_Click(object sender, EventArgs e)
        {
            // Shfaq nënmenunë e notave
            showSubmenu(panel_gradesubmenu);
        }

        #region GradesSubmenu
        private void button_addgrade_Click(object sender, EventArgs e)
        {
            // Fshin nënmenunë
            hideSubmenu();
        }

        private void button_grademngt_Click(object sender, EventArgs e)
        {
            // Fshin nënmenunë
            hideSubmenu();
        }

        private void button_printgrade_Click(object sender, EventArgs e)
        {
            // Fshin nënmenunë
            hideSubmenu();
        }
        #endregion GradesSubmenu

        // Shfaq formën e regjistrimit në faqen kryesore
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void dashboard_button_Click(object sender, EventArgs e)
        {
            // Mbyll formën aktive dhe shfaq panelin kryesor
            if (activeForm != null)
                activeForm.Close();
            panel_main.Controls.Add(panel_cover);
        }
    }
}
