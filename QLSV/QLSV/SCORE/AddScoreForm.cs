using System;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddScoreForm : Form
    {
        MY_DB mydb = new MY_DB();
        STUDENT student = new STUDENT();
        COURSE course = new COURSE();
        SCORE score = new SCORE();

        public AddScoreForm()
        {
            InitializeComponent();
        }

        private void AddScoreForm_Load(object sender, EventArgs e)
        {
            // Load dữ liệu sinh viên vào ComboBox
            this.stdTableAdapter.Fill(this.viduDBDataSet6.std);

            // Load dữ liệu khóa học vào ComboBox
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "label";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Khi người dùng nhấp vào một ô trong DataGridView, ID sinh viên của ô đó sẽ được hiển thị trong textBoxStudentID
            textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
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
    }
}
