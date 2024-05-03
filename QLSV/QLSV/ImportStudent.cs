using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace QLSV
{
    public partial class ImportStudent : Form
    {
        MY_DB mydb = new MY_DB();
        public ImportStudent()
        {
            InitializeComponent();
        }

        private void buttonImportData_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                EXCEL.Application exApp = new EXCEL.Application();
                EXCEL.Workbook exBook = exApp.Workbooks.Open(filename);
                EXCEL.Worksheet exSheet = exBook.Sheets[1]; // Assuming data is on the first sheet
                EXCEL.Range exRange = exSheet.UsedRange;

                for (int exRow = 2; exRow <= exRange.Rows.Count; exRow++)
                {
                    int STT;
                    if (!int.TryParse(exRange.Cells[exRow, 1].Text, out STT))
                    {
                        MessageBox.Show("Invalid input: Serial Number must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue; // Skip adding this row
                    }

                    int MaSV;
                    if (!int.TryParse(exRange.Cells[exRow, 2].Text, out MaSV))
                    {
                        MessageBox.Show("Invalid input: Student ID must be an integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    // Validate input for Họ và Tên (Full Name)
                    string Ho = exRange.Cells[exRow, 3].Text;
                    if (string.IsNullOrEmpty(Ho))
                    {
                        MessageBox.Show("Invalid input: Ho cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    string Ten = exRange.Cells[exRow, 5].Text;
                    if (string.IsNullOrEmpty(Ten))
                    {
                        MessageBox.Show("Invalid input: Ten cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    string dateString = exRange.Cells[exRow, 6].Text;
                    DateTime NgaySinh;
                    NgaySinh = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    string Email = MaSV + "@student.hcmute.edu.vn";
                    dataGridView1.Rows.Add(STT, MaSV, Ho, Ten, NgaySinh, Email);

                }


                exBook.Close(false);
                exApp.Quit();
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ImportStudent_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("STT", "STT");
            dataGridView1.Columns.Add("MaSV", "Mã SV");
            dataGridView1.Columns.Add("Ho", "Họ");
            dataGridView1.Columns.Add("Ten", "Ten");
            dataGridView1.Columns.Add("NgaySinh", "Ngày sinh");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";


        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow)
                    continue; // Bỏ qua dòng mới nếu nó trống

                int STT = (int)row.Cells["STT"].Value;
                int MaSV = (int)row.Cells["MaSV"].Value;
                string Ho = row.Cells["Ho"].Value.ToString();
                string Ten = row.Cells["Ten"].Value.ToString();
                DateTime NgaySinh = (DateTime)row.Cells["NgaySinh"].Value;
                string Email = row.Cells["Email"].Value.ToString();

                string checkQuery = "SELECT COUNT(*) FROM sinhVien WHERE STT = @STT AND MaSV = @MaSV AND CAST(Ho AS NVARCHAR(MAX)) = @Ho AND CAST(Ten AS NVARCHAR(MAX)) = @Ten AND NgaySinh = @NgaySinh AND CAST(Email AS NVARCHAR(MAX)) = @Email";
                using (SqlCommand command = new SqlCommand(checkQuery,mydb.getConnection))
                {
                    command.Parameters.AddWithValue("@STT", STT);
                    command.Parameters.AddWithValue("@MaSV", MaSV);
                    command.Parameters.AddWithValue("@Ho", Ho);
                    command.Parameters.AddWithValue("@Ten", Ten);
                    command.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                    command.Parameters.AddWithValue("@Email", Email);

                    mydb.openConnection();
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show($"Dữ liệu với STT = {STT}, MaSV = {MaSV}, Ho = {Ho}, Ten = {Ten}, NgaySinh = {NgaySinh}, Email = {Email} đã tồn tại trong cơ sở dữ liệu.");
                        continue;
                    }
                }

                // Nếu dữ liệu không tồn tại, thêm dữ liệu vào cơ sở dữ liệu
                string insertQuery = "INSERT INTO sinhVien (STT, MaSV, Ho, Ten, NgaySinh, Email) VALUES (@STT, @MaSV, CAST(@Ho AS NVARCHAR(MAX)), CAST(@Ten AS NVARCHAR(MAX)), @NgaySinh, CAST(@Email AS NVARCHAR(MAX)))";
                using (SqlCommand command = new SqlCommand(insertQuery, mydb.getConnection))
                {
                    command.Parameters.AddWithValue("@STT", STT);
                    command.Parameters.AddWithValue("@MaSV", MaSV);
                    command.Parameters.AddWithValue("@Ho", Ho);
                    command.Parameters.AddWithValue("@Ten", Ten);
                    command.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                    command.Parameters.AddWithValue("@Email", Email);

                    command.ExecuteNonQuery();
                }
            }
            mydb.closeConnection();

            MessageBox.Show("Dữ liệu đã được lưu vào cơ sở dữ liệu!");
        }
    }
}

