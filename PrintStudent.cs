﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DGVPrinterHelper;

namespace Student_Management_System
{
    public partial class PrintStudent : Form
    {
        // Krijimi i një instance të klases StudentClass
        StudentClass student = new StudentClass();
        // Krijimi i një instance të klases DGVPrinter
        DGVPrinter printer = new DGVPrinter();

        public PrintStudent()
        {
            InitializeComponent();
        }

        private void PrintStudent_Load(object sender, EventArgs e)
        {
            // Shfaqja e të dhënave në DataGridView në ngarkim të formës
            showData(new MySqlCommand("SELECT * FROM `student`"));
        }

        // Metoda për shfaqjen e të dhënave të studentëve në DataGridView
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            DataGridView_student.DataSource = student.getList(command);
            // Kolona 7 është indeksi i kolonës së imazhit
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[7];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void radioButton_Male_CheckedChanged(object sender, EventArgs e)
        {
            // Kjo metodë është bosh, mund të shtohen veprime sipas nevojës
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kjo metodë është bosh, mund të shtohen veprime sipas nevojës
        }

        private void label_class_Click(object sender, EventArgs e)
        {
            // Kjo metodë është bosh, mund të shtohen veprime sipas nevojës
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            // Kontrollon butonin e radio
            string selectQuery;
            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `student`";
            }
            else if (radioButton_Male.Checked)
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`='Male'";
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender`= 'Female'";
            }
            showData(new MySqlCommand(selectQuery));
        }

        private void button_print_Click(object sender, EventArgs e)
        {
            // Nevojitet DGVPrinterHelper për të shtypur skedarin PDF
            printer.Title = "Charm School List";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "foxlearn";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);
        }
    }
}
