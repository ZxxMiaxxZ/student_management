using QLSV.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class PrintStudentForm : Form
    {
        STUDENT student =   new STUDENT();
        MY_DB mydb = new MY_DB();
        public PrintStudentForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PrintStudentForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viduDBDataSet5.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.viduDBDataSet5.std);
            if(radioButtonNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }

        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string strings = "";
            if (radioButtonYes.Checked)
            {
                if (radioButtonMale.Checked)
                {
                    strings = "SELECT * FROM std WHERE bdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    strings = "SELECT * FROM std WHERE bdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Female'";
                }
                else
                {
                    strings = "SELECT * FROM std WHERE bdate BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }
            }
            else
            {
                if (radioButtonMale.Checked)
                {
                    strings = "SELECT * FROM std WHERE  gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    strings = "SELECT * FROM std WHERE  gender = 'Female'";
                }
                else
                {
                    strings = "SELECT * FROM std";
                }
            }

            SqlCommand command = new SqlCommand(strings, mydb.getConnection);
            mydb.openConnection();
            DataTable dataTable = new DataTable();

            // Thực thi truy vấn và đổ dữ liệu vào DataTable
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            // Hiển thị dữ liệu trên DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\student_list.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter writer = new StreamWriter(path))

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                }
                writer.WriteLine();
            }
            
            MessageBox.Show("Printed");
           
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void stdBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

