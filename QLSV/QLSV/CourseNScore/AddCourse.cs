using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private void AddCourse_Load(object sender, EventArgs e)
        {
            // Add the values 1, 2, and 3 to the ComboBox
            comboBox1.Items.AddRange(new object[] { 1, 2, 3 });

            // Optionally, set the default selected item
            comboBox1.SelectedIndex = 0;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                COURSE course = new COURSE();
                int id = Convert.ToInt32(txtCourseID.Text);
                string label = txtLabel.Text;
                int period = Convert.ToInt32(txtPeriod.Text);
                string description = txtDescription.Text;
                int selectedValue = (int)comboBox1.SelectedItem;

                if (course.checkCourseName(label, id))
                {
                    if (verif())
                        if (course.addCourse(id, label, period, description, selectedValue))
                        {
                            MessageBox.Show("New Course Added", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Error", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    else
                    {
                        MessageBox.Show("Empty Fields", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("This course Name Already Exists", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error");
            }


        }

        //  chuc nang kiem tra du lieu input
        bool verif()
        {
            if ((txtCourseID.Text.Trim() == "")
                        || (txtLabel.Text.Trim() == "")
                        || (txtPeriod.Text.Trim() == "")
                        || (txtDescription.Text.Trim() == ""))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    
    }
}
