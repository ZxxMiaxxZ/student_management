using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class PrintCourseForm : Form
    {
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viduDBDataSet3.course' table. You can move, or remove it, as needed.
            this.courseTableAdapter.Fill(this.viduDBDataSet3.course);

        }





        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\courses_list.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter writer = new StreamWriter(path))

                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        writer.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    writer.WriteLine();
                }
            
            MessageBox.Show("Printed");
        }
    }

}

