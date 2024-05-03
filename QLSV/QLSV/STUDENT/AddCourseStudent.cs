using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddCourseStudent : Form
    {
        MY_DB mydb = new MY_DB();
        public AddCourseStudent()
        {
            InitializeComponent();
        }

        private void AddCourseStudent_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viduDBDataSet1.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.viduDBDataSet1.std);
            // TODO: This line of code loads data into the 'viduDBDataSet4.course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.viduDBDataSet4.course);
            int idstudentcourse = Convert.ToInt32(comboBoxstudentID.SelectedValue);

            // Add the values 1, 2, and 3 to the ComboBox
            comboBox1.Items.AddRange(new object[] { 1, 2, 3 });

            // Optionally, set the default selected item
            comboBox1.SelectedIndex = 0;



            try
            {

                string query = "SELECT label FROM dbo.course";
                SqlCommand command = new SqlCommand(query, mydb.getConnection);
                mydb.openConnection();
                SqlDataReader reader = command.ExecuteReader();
                List<string> courseLabels = new List<string>();
                while (reader.Read())
                {
                    string courseName = reader.GetString(0);
                    courseLabels.Add(courseName);
                }
                listBoxAvailableCourse.DataSource = courseLabels;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mydb.closeConnection();
            }





            

        }


        private void listBoxSelectedCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.courseTableAdapter.FillBy(this.viduDBDataSet4.course);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void listBoxAvailableCourse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btb_Save_Click(object sender, EventArgs e)
        {
            // Retrieve the student's ID from comboBoxstudentID
            int studentId = Convert.ToInt32(comboBoxstudentID.SelectedValue);
            List<string> selectedCourses = listBoxSelectedCourse.Items.Cast<string>().ToList();

            // This assumes you have a method to get course ID by course name
            foreach (string courseName in selectedCourses)
            {
                int courseId = GetCourseIdByName(courseName);
                if (!IsCourseExists(studentId, courseId))
                {
                    AddCourseToDatabase(studentId, courseId);
                }
            }

            MessageBox.Show("Các khóa học đã được lưu thành công.");
        }

        private int GetCourseIdByName(string courseName)
        {
            int courseId = 0;
            string query = "SELECT Id FROM course WHERE label = @label";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@label", courseName);
            mydb.openConnection();

            try
            {
                courseId = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the course ID: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }

            return courseId;
        }

        private bool IsCourseExists(int studentId, int courseId)
        {
            bool exists = false;
            string query = "SELECT COUNT(*) FROM course_student WHERE id_std = @studentId AND id_course = @courseId";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@studentId", studentId);
            command.Parameters.AddWithValue("@courseId", courseId);
            mydb.openConnection();

            try
            {
                int count = (int)command.ExecuteScalar();
                exists = count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while checking course existence: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }

            return exists;
        }

        private void AddCourseToDatabase(int studentId, int courseId)
        {
            string query = "INSERT INTO course_student (id_std, id_course) VALUES (@studentId, @courseId)";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@studentId", studentId);
            command.Parameters.AddWithValue("@courseId", courseId);
            mydb.openConnection();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the course to the database: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
        }


        private void btn_Add_Click(object sender, EventArgs e)
        {

            string selectedCourse = listBoxAvailableCourse.SelectedItem.ToString();

            List<string> selectedCourses = (List<string>)listBoxSelectedCourse.DataSource;

            // Thêm giá trị mới vào danh sách
            selectedCourses.Add(selectedCourse);

            listBoxSelectedCourse.DataSource = null; // Xóa DataSource cũ
            listBoxSelectedCourse.DataSource = selectedCourses; // Gán DataSource mới
        }

        private void comboBoxstudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có một mục được chọn không
            if (comboBoxstudentID.SelectedItem != null)
            {
                // Lấy giá trị ID của sinh viên được chọn từ ComboBox
                int idstudentcourse = Convert.ToInt32(comboBoxstudentID.SelectedValue);

                try
                {

                    string query = "SELECT c.label " +
                                   "FROM course c " +
                                   "JOIN course_student cs ON c.Id = cs.id_course " +
                                   "JOIN std s ON cs.id_std = s.id " +
                                   "WHERE s.id = @studentId";

                    SqlCommand command = new SqlCommand(query, mydb.getConnection);
                    command.Parameters.AddWithValue("@studentId", idstudentcourse); // Thêm tham số studentId vào truy vấn
                    mydb.openConnection();
                    SqlDataReader reader = command.ExecuteReader();
                    List<string> courseLabels = new List<string>();
                    while (reader.Read())
                    {
                        string courseName = reader.GetString(0);
                        courseLabels.Add(courseName);
                    }
                    // Gán danh sách các khóa học cho DataSource của ListBox hoặc ComboBox trong bảng SelectCourse
                    listBoxSelectedCourse.DataSource = courseLabels;
                }
                catch (Exception ex)
                {
                    // Xử lý các ngoại lệ
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    mydb.closeConnection();
                }
            }
        }



        private bool IsCourseExists(int idstudentcourse)
        {
            bool exists = false;
            try
            {
                // Tạo câu truy vấn SQL để kiểm tra xem khóa học có tồn tại không
                string query = "SELECT COUNT(*) " +
                               "FROM course c " +
                               "JOIN course_student cs ON c.Id = cs.id_course " +
                               "JOIN std s ON cs.id_std = s.id " +
                               "WHERE s.id = @studentId";

                SqlCommand command = new SqlCommand(query, mydb.getConnection);
                command.Parameters.AddWithValue("@studentId", idstudentcourse);
                mydb.openConnection();
                int count = (int)command.ExecuteScalar();

                // Nếu số lượng bản ghi trả về lớn hơn 0, có nghĩa là khóa học đã tồn tại trong cơ sở dữ liệu
                exists = count > 0;
                Console.WriteLine(exists);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
            return exists;
        }


        private void AddCourseToDatabase(string courseName)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idstudentcourse = Convert.ToInt32(comboBoxstudentID.SelectedValue);
            int selectedValue = (int)comboBox1.SelectedItem;
            string query = "SELECT  c.label " +
                           "FROM course c " +
                           "JOIN course_student cs ON c.Id = cs.id_course " +
                           "JOIN std s ON cs.id_std = s.id " +
                           "WHERE s.id = @studentId AND semester = @Sem";

            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            command.Parameters.AddWithValue("@studentId", idstudentcourse); // Add the studentId parameter to the query
            command.Parameters.AddWithValue("@Sem", selectedValue); // Add the semester parameter to the query
            mydb.openConnection();

            SqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();
                List<string> courseLabels = new List<string>();
                while (reader.Read())
                {
                    string courseName = reader.GetString(0);
                    courseLabels.Add(courseName);
                }
                listBoxSelectedCourse.DataSource = courseLabels;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the SqlDataReader and the database connection in the finally block
                if (reader != null)
                    reader.Close();

                mydb.closeConnection();
            }
        }

    }
}
