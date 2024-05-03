using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class ManageScoreForm : Form
    {
        public ManageScoreForm()
        {
            InitializeComponent();
        }
        SCORE score = new SCORE();
        STUDENT student = new STUDENT();
        COURSE course = new COURSE();
        MY_DB mydb = new MY_DB();
        string data = "score";

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            // Load dữ liệu khóa học vào ComboBox
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = null;

            dataGridView1.DataSource = score.getStudentScore();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các điều khiển trên giao diện
                int studentId = Convert.ToInt32(textBoxStudentID.Text);
                int courseId = Convert.ToInt32(comboBoxCourse.SelectedValue);
                double scoreValue = Convert.ToDouble(textBoxScore.Text);
                string description = textBoxDescription.Text;

                // Kiểm tra xem điểm của sinh viên cho khóa học đã tồn tại chưa
                if (!score.studentScoreExists(studentId, courseId))
                {
                    // Nếu chưa tồn tại, chèn điểm mới vào cơ sở dữ liệu
                    if (score.insertScore(studentId, courseId, scoreValue, description))
                    {
                        MessageBox.Show("Student Score Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = score.getStudentScore();

                    }
                    else
                    {
                        MessageBox.Show("Student Score Not Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("This Score For This Course Already Exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int studentId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            int courseId = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

            if (MessageBox.Show("Do You Want To Delete This Score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (score.deleteScore(studentId, courseId))
                {
                    MessageBox.Show("Score Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = score.getStudentScore();

                }
                else
                {
                    MessageBox.Show("Error", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                MessageBox.Show("Empty Fields", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data == "student")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (data =="score")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBoxCourse.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
            }
        }

        private void buttonShowStudent_Click(object sender, EventArgs e)
        {
            data = "student";
            SqlCommand command = new SqlCommand("SELECT id, fname, lname, bdate FROM std");
            dataGridView1.DataSource = student.getStudents(command);

        }

        private void buttonShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.getStudentScore();

        }

        private void buttonAvg_Click(object sender, EventArgs e)
        {
            avgScoreByCourseForm avgScoreByCourseForm = new avgScoreByCourseForm();
            avgScoreByCourseForm.Show();
        }
    }
}
