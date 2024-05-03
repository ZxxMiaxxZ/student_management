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
    public partial class studentListForm : Form
    {
        public studentListForm()
        {
            InitializeComponent();
        }

        STUDENT student = new STUDENT();
        private void studentListForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viduDBDataSet1.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter1.Fill(this.viduDBDataSet1.std);
            // TODO: This line of code loads data into the 'viduDBDataSet.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.viduDBDataSet.std);
            SqlCommand command = new SqlCommand("Select * FROM std");
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();

            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.Columns[3].DefaultCellStyle.Format = "MM/dd/yyyy";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStf = new UpdateDeleteStudentForm();
            updateDeleteStf.TextBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStf.TextBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); 
            updateDeleteStf.TextBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStf.DateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if ((dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female"))
            {
                updateDeleteStf.RadioButtonFemale.Checked = true;
            }

            updateDeleteStf.TextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            updateDeleteStf.TextBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            byte[] pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            using (MemoryStream picture = new MemoryStream(pic))
            {
                updateDeleteStf.PictureBoxStudentImage.Image = Image.FromStream(picture);
            }


            updateDeleteStf.Show();
        }






        private void btn_Reload_Click(object sender, EventArgs e)
        {
            student.reload();
            dataGridView1.ReadOnly = true;
        }
    }
}
