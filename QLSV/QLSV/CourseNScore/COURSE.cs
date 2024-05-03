using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using static System.Windows.Forms.AxHost;
using System.Net;
using System.Xml.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace QLSV
{
    class COURSE
    {

        MY_DB mydb = new MY_DB();

        public bool checkCourseName(string courseName, int courseId=0)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE label=@cName AND id<>@cID", mydb.getConnection);
            /*            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE label=@cName", mydb.getConnection);*/
            command.Parameters.Add("@cName",SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@cID",SqlDbType.Int).Value = courseId;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return (table.Rows.Count == 0);

        }

        public bool addCourse(int courseId, string label, int period, string description)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO course (id, label, period, description)" +
                    " VALUES (@id,@label, @period, @decription)", mydb.getConnection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = courseId;
                command.Parameters.Add("@label", SqlDbType.VarChar).Value = label;
                command.Parameters.Add("@period", SqlDbType.Int).Value = period;
                command.Parameters.Add("@decription", SqlDbType.NVarChar).Value = description;

                mydb.openConnection();

                if ((command.ExecuteNonQuery() == 1))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            return true;

        }

        public bool addCourse(int courseId, string label, int period, string description, int semester)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO course (id, label, period, description)" +
                    " VALUES (@id,@label, @period, @decription)", mydb.getConnection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = courseId;
                command.Parameters.Add("@label", SqlDbType.VarChar).Value = label;
                command.Parameters.Add("@period", SqlDbType.Int).Value = period;
                command.Parameters.Add("@decription", SqlDbType.NVarChar).Value = description;
                mydb.openConnection();


                if ((command.ExecuteNonQuery() == 1))
                {
                    mydb.closeConnection();
                    return true;
                }
                else
                {
                    mydb.closeConnection();
                    return false;
                }

                SqlCommand command2 = new SqlCommand("INSERT INTO course_student (id,semester)" +
                    " VALUES (@id,@semester)", mydb.getConnection);
                command.Parameters.Add("@id", SqlDbType.Int).Value = courseId;
                command.Parameters.Add("@semester", SqlDbType.Int).Value = semester;
                mydb.openConnection();


                if ((command.ExecuteNonQuery() == 1))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            return true;

        }

        public bool removeCourse(int courseId)
        {
            SqlCommand command = new SqlCommand("DELETE FROM course WHERE id = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = courseId;

            mydb.openConnection();
            bool success = command.ExecuteNonQuery() == 1;
            mydb.closeConnection();

            return success;
        }



        public DataTable getAllCourses()
        {
            SqlCommand command = new SqlCommand("SElECT * from course",mydb.getConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public int totalCourses()
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM course",mydb.getConnection);
            mydb.openConnection();
            int totalCourses = (int)command.ExecuteScalar();
            mydb.closeConnection();
            return totalCourses;
        }

        public bool updateCourse(int courseID, string courseName, int hoursNumber, string description)
        {
            SqlCommand command = new SqlCommand("UPDATE course SET label=@name, period=@hrs, description=@descr WHERE id = @cid", mydb.getConnection);

            command.Parameters.Add("@cid", SqlDbType.Int).Value = courseID;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", SqlDbType.Int).Value = hoursNumber;
            command.Parameters.Add("@descr", SqlDbType.NVarChar).Value = description;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
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


        public DataTable getCourseById(int courseID)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE id =@id", mydb.getConnection);
            command.Parameters.Add("@id",SqlDbType.Int).Value = courseID;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }


    }
}
