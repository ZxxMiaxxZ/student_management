using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
        }

        STUDENT student = new STUDENT();
        MY_DB mydb = new MY_DB();

        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viduDBDataSet5.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.viduDBDataSet5.std);
            // TODO: This line of code loads data into the 'viduDBDataSet5.std' table. You can move, or remove it, as needed.
            /*            this.stdTableAdapter.Fill(this.viduDBDataSet5.std);*/
            /*            SqlCommand command = new SqlCommand(@"
                                    SELECT std.id, std.fname, std.lname, std.bdate, std.gender, std.phone, std.address, std.picture, course.label AS selected_course
                                    FROM std
                                    LEFT JOIN course_student ON std.id = course_student.id_std
                                    LEFT JOIN course ON course_student.id_course = course.Id", mydb.getConnection);
                        dataGridView1.DataSource = student.getStudents(command);*/


            dataGridView1.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy";
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            labelTotalStudent.Text = "Total Student: "+ student.totalStudent();

        }

        private void labelTotalStudent_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
  //          pictureDataGridViewImageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TextBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TextBoxLname.Text =dataGridView1.CurrentRow.Cells[2].Value.ToString();

            //DateTime dateValue = ((DateTime)dataGridView1.CurrentRow.Cells[3].Value).Date;


            DateTimePicker1.Format = DateTimePickerFormat.Custom;
            DateTimePicker1.CustomFormat = "dd/MM/yyyy";

            // Assign the date value from the DataGridView to the DateTimePicker
            DateTime dateValue = ((DateTime)dataGridView1.CurrentRow.Cells[3].Value).Date;
            DateTimePicker1.Value = dateValue;



            string cellValue = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();
            if (cellValue == "Female")
            {
                RadioButtonFemale.Checked = true;
            }
            else
            {
                RadioButtonMale.Checked = true;
            }
            /*            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
                        {
                            RadioButtonFemale.Checked = true;
                        }
                        else
                        {
                            RadioButtonMale.Checked = true;
                        }*/

            TextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            TextBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            PictureBoxStudentImage.Image = Image.FromStream(picture);
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            txtStudentID.Text = "";
            TextBoxLname.Text = "";
            TextBoxPhone.Text = "";
            TextBoxFname.Text = "";
            TextBoxAddress.Text = "";
            RadioButtonMale.Checked = false;
            RadioButtonFemale.Checked = false;
            DateTimePicker1.Value = DateTime.Now;
            PictureBoxStudentImage.Image = null;
            this.stdTableAdapter.Fill(this.viduDBDataSet5.std);

        }



        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            STUDENT student = new STUDENT();
            int id = Convert.ToInt32(txtStudentID.Text);
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            DateTime bdate = DateTimePicker1.Value;
            string phone = TextBoxPhone.Text;
            string adrs = TextBoxAddress.Text;
            string gender = "Male";

            if (RadioButtonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();
            int born_year = DateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            //  sv tu 10-100,  co the thay doi
            if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                if (student.insertStudent(id, fname, lname, bdate, gender, phone, adrs, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }


        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            int id;
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            DateTime bdate = DateTimePicker1.Value;
            string phone = TextBoxPhone.Text;
            string adrs = TextBoxAddress.Text;
            string gender = "Male";

            if (RadioButtonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();
            int born_year = DateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            //  sv tu 10-100,  co the thay doi
            if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                try
                {
                    id = Convert.ToInt32(txtStudentID.Text);
                    PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, bdate, gender, phone, adrs, pic))
                    {
                        MessageBox.Show("Student Information Update", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Empty Fields", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtStudentID.Text);
            if (MessageBox.Show("Are you sure you want to delete this student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (student.deleteStudent(id))
                {
                    MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStudentID.Text = "";
                    TextBoxFname.Text = "";
                    TextBoxLname.Text = "";
                    TextBoxPhone.Text =
                    TextBoxAddress.Text = "";
                    DateTimePicker1.Value = DateTime.Now;
                    PictureBoxStudentImage.Image = null;
                }
                else
                {
                    MessageBox.Show("Student Not Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonDownloadImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = "Student_"+txtStudentID.Text;

            if(PictureBoxStudentImage.Image == null)
            {
                MessageBox.Show("No Image In The PictureBox");
            }
            else if (svf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxStudentImage.Image.Save(svf.FileName+ ("."+ImageFormat.Jpeg.ToString()));
            }

        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
/*            string test = "SELECT * FROM std WHEHRE CONCAT(fname,lname,address) LIKE%" + textBoxSearch.Text + "%";
*/            SqlCommand command = new SqlCommand("SELECT * FROM std WHERE CONCAT(id,fname,lname,address,phone,gender) LIKE '%" + textBoxSearch.Text + "%'", mydb.getConnection);
/*            student.execCount(test);
*/            mydb.openConnection();
            DataTable dataTable = new DataTable();

            // Thực thi truy vấn và đổ dữ liệu vào DataTable
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            // Hiển thị dữ liệu trên DataGridView
            dataGridView1.DataSource= dataTable;
        }

        bool verif()
        {
            if ((TextBoxFname.Text.Trim() == "")
                        || (TextBoxLname.Text.Trim() == "")
                        || (TextBoxAddress.Text.Trim() == "")
                        || (TextBoxPhone.Text.Trim() == "")
                        || (PictureBoxStudentImage.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
