using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class RemoveCourse : Form
    {
        public RemoveCourse()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            COURSE course = new COURSE();
            int courseId = Convert.ToInt32(txtRemoveCourse.Text);
            if (verif())
                if (course.removeCourse(courseId))
                {
                    MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                MessageBox.Show("Empty Fields", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        bool verif()
        {
            if ((txtRemoveCourse.Text.Trim() == ""))
           
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
