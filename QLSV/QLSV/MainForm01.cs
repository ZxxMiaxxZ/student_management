using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{ 
    public partial class MainForm01 : Form
    {
        public MainForm01()
        {
            InitializeComponent();
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudentForm addStdF = new AddStudentForm();
            addStdF.Show(this);
        }

        private void StudentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentListForm stListf = new studentListForm();
            stListf.Show(this);
        }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.Show(this);
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStudentsForm manageStudentsForm = new ManageStudentsForm();
            manageStudentsForm.Show(this);
        }

        private void removeCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCourse removeCourse = new RemoveCourse();
            removeCourse.Show(this);    
        }

        private void manageCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCoursesForm manageCoursesForm = new ManageCoursesForm();
            manageCoursesForm.Show(this);
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintCourseForm printCourseForm = new PrintCourseForm();
            printCourseForm.Show(this);
        }

        private void editCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCourse editCourse = new EditCourse();
            editCourse.Show(this);
        }

        private void editRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStudentForm = new UpdateDeleteStudentForm();
            updateDeleteStudentForm.Show(this);
        }

        private void staticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticsForm staticsForm = new StaticsForm();
            staticsForm.Show(this);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStudentForm printStudentForm = new PrintStudentForm();
            printStudentForm.Show(this);
        }

        private void MainForm01_Load(object sender, EventArgs e)
        {

        }

        private void addScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScoreForm addScoreForm = new AddScoreForm();
            addScoreForm.Show(this);
        }

        private void removeScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveScoreForm removeScoreForm = new RemoveScoreForm();
            removeScoreForm.Show(this);
        }

        private void manageScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageScoreForm manageScoreForm = new ManageScoreForm();
            manageScoreForm.Show(this);
        }

        private void avgScoreByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            avgScoreByCourseForm avgScoreByCourseForm = new avgScoreByCourseForm();
            avgScoreByCourseForm.Show();
        }

        private void printToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PrintScoreForm printScoreForm = new PrintScoreForm();
            printScoreForm.Show(this);
        }

        private void addStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportStudent importStudent = new ImportStudent();
            importStudent.Show(this);
        }

        private void aVGByScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aVGByScore aVGByScore = new aVGByScore();
            aVGByScore.Show(this);
        }

        private void finalResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticsResult staticsResult = new StaticsResult();
            staticsResult.Show(this);
        }
    }
}
