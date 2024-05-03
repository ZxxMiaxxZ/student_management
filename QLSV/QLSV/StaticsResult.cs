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
    public partial class StaticsResult : Form
    {
        MY_DB mydb = new MY_DB();
        public StaticsResult()
        {
            InitializeComponent();
        }

        private void StaticsResult_Load(object sender, EventArgs e)
        {
            string query = @"
                SELECT 
                    c.label AS Course,
                    AVG(sc.score) AS AverageScore,
                    MIN(sc.score) AS MinimumScore,
                    MAX(sc.score) AS MaximumScore
                FROM 
                    course c
                JOIN 
                    score sc ON c.Id = sc.courseId
                GROUP BY 
                    c.label
                ORDER BY 
                    AverageScore DESC;";
            SqlCommand command = new SqlCommand(query,mydb.getConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource= table;

            label3.Text = "Fail: " + getFail();
            label4.Text = "Pass: " + getPass();
        }



        public string getPass()
        {
            string query = @"
                SELECT
                    COUNT(*) AS pass_count
                FROM
                (
                        SELECT
                            AVG(score) AS avg_score
                        FROM
                            score
                        GROUP BY
                            studentId
                    ) AS subquery
                WHERE
                    avg_score >= 5;";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            string count = command.ExecuteScalar().ToString();
            mydb.closeConnection();
            return count;
        }
        public string getFail()
        {
            string query = @"
                SELECT
                    COUNT(*) AS pass_count
                FROM
                (
                        SELECT
                            AVG(score) AS avg_score
                        FROM
                            score
                        GROUP BY
                            studentId
                    ) AS subquery
                WHERE
                    avg_score < 5;";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            string count = command.ExecuteScalar().ToString();
            mydb.closeConnection();
            return count;
        }
    }

}


