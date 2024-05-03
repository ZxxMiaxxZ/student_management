using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Windows.Forms;

namespace QLSV
{
    class SCORE
    {
        MY_DB mydb = new MY_DB();
        public bool insertScore(int studentId, int courseId, double score, string description)
        {
            SqlCommand command = new SqlCommand("INSERT INTO score(studentId, courseId, score, description) VALUES (@sid,@cid,@scr,@dscr)", mydb.getConnection);
            command.Parameters.Add("@sid", SqlDbType.Int).Value = studentId; 
            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseId;
            command.Parameters.Add("@scr", SqlDbType.Float).Value = score; 
            command.Parameters.Add("@dscr", SqlDbType.Text).Value = description;
            mydb.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool studentScoreExists(int studentId, int courseId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM score WHERE studentId=@sid AND courseId=@cid", mydb.getConnection);
            command.Parameters.Add("@sid", SqlDbType.Int).Value = studentId;
            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseId;

            mydb.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable getStudentScore()
        {
            SqlCommand command  = new SqlCommand("SELECT score.studentId, std.fname, std.lname,score.courseId, course.label,score.score FROM std INNER JOIN score ON std.id = score.studentId INNER JOIN course ON score.courseId = course.Id",mydb.getConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool deleteScore(int studentId,int courseId)
        {
            SqlCommand command = new SqlCommand("DELETE FROM score WHERE studentId = @sid AND courseId = @cid", mydb.getConnection);
            command.Parameters.Add("@sid", SqlDbType.Int).Value = studentId;
            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseId;

            mydb.openConnection();
            bool success = command.ExecuteNonQuery() == 1;
            mydb.closeConnection();

            return success;
        }

        public DataTable acgScoreByCourse()
        {
            SqlCommand command = new SqlCommand("SELECT course.label, avg(score.score) as 'Average Score' FROM course, score WHERE course.Id = score.courseId GROUP BY course.label", mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataTable getCourseScore(int courseId)
        {
            SqlCommand command = new SqlCommand("SELECT score.studentId, std.fname, std.lname, score.courseId, course.label, score.score FROM score INNER JOIN std ON score.studentId = std.id INNER JOIN course ON score.courseId = course.Id WHERE score.courseId = @CourseId", mydb.getConnection);
            command.Parameters.AddWithValue("@CourseId", courseId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable getStudentScore(int studentId)
        {
            SqlCommand command = new SqlCommand("SELECT score.studentId, std.fname, std.lname, score.courseId, course.label, score.score FROM score INNER JOIN std ON score.studentId = std.id INNER JOIN course ON score.courseId = course.Id WHERE score.studentId = @StudentId", mydb.getConnection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }


        public DataTable getavg()
        {
            SqlCommand command = new SqlCommand("DECLARE @columns AS NVARCHAR(MAX), \r\n        @columnsSelect AS NVARCHAR(MAX),\r\n        @sql AS NVARCHAR(MAX);\r\n\r\n-- Tạo danh sách các cột cho các môn học\r\nSELECT @columns = STRING_AGG(QUOTENAME(label), ', ') \r\nFROM (SELECT DISTINCT label FROM course) AS course;\r\n\r\n-- Tạo danh sách các cột để chọn và tính điểm trung bình\r\nSELECT @columnsSelect = STRING_AGG('ISNULL(' + QUOTENAME(label) + ', 0)', '+') \r\nFROM (SELECT DISTINCT label FROM course) AS course;\r\n\r\n-- Xây dựng câu truy vấn động với PIVOT và tính toán điểm trung bình và kết quả\r\nSET @sql = N'\r\nSELECT studentId, FirstName, LastName, ' + @columns + ', \r\n    (CASE \r\n        WHEN AvgScore > 5 THEN ''Pass'' \r\n        ELSE ''Fail'' \r\n    END) AS Result,\r\n    AvgScore\r\nFROM \r\n(\r\n    SELECT \r\n        s.id AS studentId, \r\n        s.fname AS FirstName, \r\n        s.lname AS LastName, \r\n        c.label AS CourseLabel, \r\n        sc.score,\r\n        AVG(sc.score) OVER (PARTITION BY s.id) AS AvgScore\r\n    FROM \r\n        score sc\r\n    INNER JOIN \r\n        std s ON sc.studentId = s.id\r\n    INNER JOIN \r\n        course c ON sc.courseId = c.Id\r\n) AS SourceData\r\nPIVOT\r\n(\r\n    MAX(score) FOR CourseLabel IN (' + @columns + N')\r\n) AS PivotTable\r\nORDER BY studentId';\r\n\r\n-- Thực thi câu truy vấn\r\nEXEC sp_executesql @sql;\r\n", mydb.getConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }

    
}
 