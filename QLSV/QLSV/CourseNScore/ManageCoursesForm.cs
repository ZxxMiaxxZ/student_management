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
    public partial class ManageCoursesForm : Form
    {
        COURSE course = new COURSE();
        int pos;
        public ManageCoursesForm()
        {
            InitializeComponent();
        }

        private void ManageCoursesForm_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'viduDBDataSet2.course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.viduDBDataSet2.course);
            reloadListBoxData();

        }

        private void reloadListBoxData()
        {
            listBox1.DataSource = course.getAllCourses();
            listBox1.ValueMember = "id";
            listBox1.DisplayMember = "label";
            listBox1.SelectedItem = null;
            label_total_courses.Text = ("Total Courses: " + course.totalCourses());
        }

        void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];
            listBox1.SelectedIndex = index;
            TextBoxID.Text = dr.ItemArray[0].ToString();
            TextBoxLabel.Text = dr.ItemArray[1].ToString();
            numericUpDown1.Value = int.Parse(dr.ItemArray[2].ToString());   
            TextBoxDescription.Text = dr.ItemArray[3].ToString();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            pos = listBox1.SelectedIndex;
            showData(pos);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxID.Text);
            string name = TextBoxLabel.Text;
            int hrs = (int)numericUpDown1.Value;
            string descr = TextBoxDescription.Text;

            if (name.Trim() == "")
            {
                MessageBox.Show("Add A Course Name","Add Course",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            }
            else if (course.checkCourseName(name))
            {
                if (course.addCourse(id,name,hrs,descr))
                {
                    MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("This Course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string name = TextBoxLabel.Text;
            int hrs = (int)numericUpDown1.Value;
            string descr = TextBoxDescription.Text;
            int id = int.Parse(TextBoxID.Text);

            if (!course.checkCourseName(name, Convert.ToInt32(TextBoxID.Text))) 
            {
                MessageBox.Show("This Course Name Already Exist", "Edit Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (course.addCourse(id,name,hrs,descr))
            {
                MessageBox.Show("Course updated","Edit Course",MessageBoxButtons.OK, MessageBoxIcon.Information);
                reloadListBoxData();
            }
            else
            {
                MessageBox.Show("Course Not Updated","Edit Course",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            pos = 0;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                int courseID = Convert.ToInt32(TextBoxID.Text);
                if (MessageBox.Show("Are you sure you want to delete this course?", "Remove Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.removeCourse(courseID))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxID.Text = "";
                        TextBoxLabel.Text = "";
                        numericUpDown1.Value = 10;
                        TextBoxDescription.Text = "";

                        reloadListBoxData();
                    }    
                    else
                    {
                        MessageBox.Show("Course Not Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Enter A Valid Numeric ID", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            pos = 0;
        }

        private void btn_First_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(pos);
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            if (pos <(course.getAllCourses().Rows.Count-1))
            {
                pos++;
                showData(pos);
            }
        }

        private void btn_Previous_Click(object sender, EventArgs e)
        {
            if (pos >0)
            {
                pos--;
                showData(pos);
            }
        }

        private void btn_Last_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count - 1;
            showData(pos);
        }
    }
}
