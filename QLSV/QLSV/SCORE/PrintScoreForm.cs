using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class PrintScoreForm : Form
    {
        public PrintScoreForm()
        {
            InitializeComponent();
        }
        SCORE score = new SCORE();
        STUDENT student = new STUDENT();
        COURSE course = new COURSE();


        private void PrintScoreForm_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = course.getAllCourses();
            listBox1.DisplayMember = "label";
            listBox1.ValueMember = "Id";

            SqlCommand command = new SqlCommand("SELECT id, fname, lname FROM std");
            dataGridView2.DataSource = student.getStudents(command);
            dataGridView1.DataSource = score.getStudentScore();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = score.getCourseScore(int.Parse(listBox1.SelectedValue.ToString()));
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = score.getCourseScore(int.Parse(listBox1.SelectedValue.ToString()));

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        { 
            dataGridView1.DataSource = score.getStudentScore(int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\scores_list.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter writer = new StreamWriter(path))

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    writer.WriteLine();
                }
            MessageBox.Show("Printed");
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = score.getStudentScore();

        }
    }
}
