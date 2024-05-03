using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLSV
{
    public partial class aVGByScore : Form
    {
        MY_DB mydb = new MY_DB();
        SCORE score = new SCORE();
        public aVGByScore()
        {
            InitializeComponent();
        }

     

        private void aVGByScore_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = score.getavg();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\avgByscore.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                // Writing the headers
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    // Write each header cell followed by a tab
                    writer.Write(dataGridView1.Columns[i].HeaderText + "\t" + "|");
                }
                writer.WriteLine(); // Move to the next line after writing headers

                // Writing the data rows
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Handling potential null values in cells
                        writer.Write((dataGridView1.Rows[i].Cells[j].Value ?? "").ToString() + "\t" + "|");
                    }
                    writer.WriteLine();
                }
            }

            MessageBox.Show("Printed");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            // Determine whether the input is numeric (likely an ID) or alphabetic/text (likely a name)
            bool isNumeric = int.TryParse(TextBoxSearch.Text, out int numericId);

            string getColumnsQuery = @"
        SELECT STRING_AGG(QUOTENAME(label), ', ') AS Columns
        FROM (SELECT DISTINCT label FROM course) AS course;";

            SqlCommand getColumnsCommand = new SqlCommand(getColumnsQuery, mydb.getConnection);
            mydb.openConnection();

            string columns = getColumnsCommand.ExecuteScalar()?.ToString();
            mydb.closeConnection();

            if (string.IsNullOrEmpty(columns))
            {
                MessageBox.Show("No courses found.");
                return;
            }

            string dynamicSql = $@"
        SELECT studentId, FirstName, LastName, {columns}, 
            (CASE WHEN AvgScore > 5 THEN 'Pass' ELSE 'Fail' END) AS Result, AvgScore
        FROM (
            SELECT
                s.id AS studentId,
                s.fname AS FirstName,
                s.lname AS LastName,
                c.label AS CourseLabel,
                sc.score,
                AVG(sc.score) OVER (PARTITION BY s.id) AS AvgScore
            FROM
                score sc
            INNER JOIN
                std s ON sc.studentId = s.id
            INNER JOIN
                course c ON sc.courseId = c.Id
            WHERE
                (@isNumeric = 1 AND s.id = @studentId)
                OR (@isNumeric = 0 AND s.fname LIKE '%' + @firstName + '%')
        ) AS SourceData
        PIVOT (
            MAX(score) FOR CourseLabel IN ({columns})
        ) AS PivotTable
        ORDER BY studentId;";

            SqlCommand command = new SqlCommand(dynamicSql, mydb.getConnection);
            command.Parameters.AddWithValue("@studentId", isNumeric ? numericId : (object)DBNull.Value);
            command.Parameters.AddWithValue("@firstName", isNumeric ? (object)DBNull.Value : TextBoxSearch.Text);
            command.Parameters.AddWithValue("@isNumeric", isNumeric);

            System.Data.DataTable dataTable = new System.Data.DataTable();
            mydb.openConnection();

            // Execute the query and fill the DataTable with the results
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }

            mydb.closeConnection();

            // Display data in DataGridView
            dataGridView1.DataSource = dataTable;
        }


    }
}
