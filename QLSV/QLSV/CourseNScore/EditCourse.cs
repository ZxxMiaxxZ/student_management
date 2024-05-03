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
    public partial class EditCourse : Form
    {
        public EditCourse()
        {
            InitializeComponent();
        }
        COURSE course = new COURSE();

        private void EditCourse_Load(object sender, EventArgs e)
        {
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = null;
        }
        public void fillCombo(int index)
        {
/*            COURSE course = new COURSE();*/
/*            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = index;*/
        } 

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
/*            COURSE course = new COURSE();
*/
            try
            {
                int id = Convert.ToInt32(comboBoxCourse.SelectedValue);
                DataTable table = new DataTable();
                table = course.getCourseById(id);
                txtLabel.Text = table.Rows[0][1].ToString();
                numericUpDown1.Value = Int32.Parse(table.Rows[0][2].ToString());
                txtDescription.Text = table.Rows[0][3].ToString();
            }
            catch
            {
               
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
/*            COURSE course = new COURSE();
*/            string name = txtLabel.Text;
            int hrs = (int) numericUpDown1.Value;
            string descr = txtDescription.Text;
            int id = (int) comboBoxCourse.SelectedValue;

            if (!course.checkCourseName(name, Convert.ToInt32((comboBoxCourse.SelectedValue))))
            {
                MessageBox.Show("This course Name is Already Existed", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (course.updateCourse(id,name,hrs,descr))
            {
                MessageBox.Show("Course Updated", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fillCombo(comboBoxCourse.SelectedIndex);
            }
            else 
            {
                MessageBox.Show("Course Not Update", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
