using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{

    public partial class Form1 : Form
    {
        private bool changeAllowed = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeAllowed)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Programs.GetPrograms();
                bindingSource1.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeAllowed)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Courses.GetCourses();
                bindingSource2.Sort = "CId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["CId"].HeaderText = "Course ID";
                dataGridView1.Columns["CId"].DisplayIndex = 0;
                dataGridView1.Columns["CName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeAllowed)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource3.DataSource = Data.Students.GetStudents();
                bindingSource3.Sort = "StId";
                dataGridView1.DataSource = bindingSource3;

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["StName"].DisplayIndex = 1;
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeAllowed)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource4.DataSource = Data.Enrollments.GetEnrollments();
                bindingSource4.Sort = "StId";
                dataGridView1.DataSource = bindingSource4;

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StId"].DisplayIndex = 0;
                dataGridView1.Columns["CId"].DisplayIndex = 1;
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 2;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            BussinessLayer.Programs.UpdatePrograms();
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (BussinessLayer.Courses.UpdateCourses() == -1)
            {
                Validate();
            }
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            if (BussinessLayer.Students.UpdateStudents() == -1)
            {
                Validate();
            }
        }

        private void bindingSource4_CurrentChanged(object sender, EventArgs e)
        {
            if (BussinessLayer.Enrollment.UpdateEnrollment() == -1)
            {
                Validate();
            }
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            changeAllowed = true;

            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            Validate();

            if (temp == bindingSource1)
            {
                if (BussinessLayer.Programs.UpdatePrograms() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BussinessLayer.Courses.UpdateCourses() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource3)
            {
                if (BussinessLayer.Students.UpdateStudents() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource4)
            {
                if (BussinessLayer.Enrollment.UpdateEnrollment() == -1)
                {
                    changeAllowed = false;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update / delete");
            e.Cancel = false;

            changeAllowed = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            if (temp == bindingSource1)
            {
                if (BussinessLayer.Programs.UpdatePrograms() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BussinessLayer.Courses.UpdateCourses() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource3)
            {
                if (BussinessLayer.Students.UpdateStudents() == -1)
                {
                    changeAllowed = false;
                }
            }
            else if (temp == bindingSource4)
            {
                if (BussinessLayer.Enrollment.UpdateEnrollment() == -1)
                {
                    changeAllowed = false;
                }
            }

            if (!changeAllowed)
            {
                e.Cancel = true;
                changeAllowed = true;
            }
        }

        //Error Messages

        internal static void msgBLL1()
        {
            MessageBox.Show("All courses must belong to one and only one program");
        }

        internal static void msgBLL2()
        {
            MessageBox.Show("All students must be in one and only one program");
        }

        internal static void msgBLL3()
        {
            MessageBox.Show("A student can enrol only to courses in the student's program");
        }

        internal static void msgBLL4()
        {
            MessageBox.Show("Valid values of FinalGrade are null (no grade) or an integer between 0 and 100");
        }

        internal static void msgBLL5()
        {
            MessageBox.Show("All enrollments are created with FinalGrade set to null");
        }

        internal static void msgBLL7()
        {
            MessageBox.Show("Once the FinalGrade is assigned, we cannot delete the enrollment and the only modification possible in the enrollment is to remove the final grade.");

        }


    }
}
