using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StaticsForm_Load(object sender, EventArgs e)
        {
            STUDENT student = new STUDENT();
            double totalStudents = Convert.ToDouble(student.totalStudent());
            double totalMaleStudents = Convert.ToDouble(student.totalMaleStudent());
            double totalFemaleStudents = Convert.ToDouble(student.totalFemaleStudent());

            double malePercentage = totalMaleStudents / totalStudents * 100;
            double femalePercentage = totalFemaleStudents / totalStudents * 100;

            labelTotal.Text = "Total Students: " + totalStudents.ToString();
            labelMale.Text = "Male: " + totalMaleStudents.ToString();
            labelFemale.Text = "Female: " + totalFemaleStudents.ToString();
        }

    }
}
