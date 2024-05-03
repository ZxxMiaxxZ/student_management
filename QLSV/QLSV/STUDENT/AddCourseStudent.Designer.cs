namespace QLSV
{
    partial class AddCourseStudent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btb_Save = new System.Windows.Forms.Button();
            this.listBoxAvailableCourse = new System.Windows.Forms.ListBox();
            this.courseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viduDBDataSet4 = new QLSV.viduDBDataSet4();
            this.listBoxSelectedCourse = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.courseTableAdapter = new QLSV.viduDBDataSet4TableAdapters.courseTableAdapter();
            this.comboBoxstudentID = new System.Windows.Forms.ComboBox();
            this.stdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viduDBDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viduDBDataSet1 = new QLSV.viduDBDataSet1();
            this.stdTableAdapter = new QLSV.viduDBDataSet1TableAdapters.stdTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student ID: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(396, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Semester:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(62, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Avaiable Course";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(503, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Selected Course";
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.Coral;
            this.btn_Add.Location = new System.Drawing.Point(324, 183);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(91, 39);
            this.btn_Add.TabIndex = 1;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btb_Save
            // 
            this.btb_Save.BackColor = System.Drawing.Color.Orange;
            this.btb_Save.Location = new System.Drawing.Point(324, 241);
            this.btb_Save.Name = "btb_Save";
            this.btb_Save.Size = new System.Drawing.Size(91, 36);
            this.btb_Save.TabIndex = 1;
            this.btb_Save.Text = "Save";
            this.btb_Save.UseVisualStyleBackColor = false;
            this.btb_Save.Click += new System.EventHandler(this.btb_Save_Click);
            // 
            // listBoxAvailableCourse
            // 
            this.listBoxAvailableCourse.DataSource = this.courseBindingSource;
            this.listBoxAvailableCourse.DisplayMember = "label";
            this.listBoxAvailableCourse.FormattingEnabled = true;
            this.listBoxAvailableCourse.ItemHeight = 16;
            this.listBoxAvailableCourse.Location = new System.Drawing.Point(57, 167);
            this.listBoxAvailableCourse.Name = "listBoxAvailableCourse";
            this.listBoxAvailableCourse.Size = new System.Drawing.Size(217, 132);
            this.listBoxAvailableCourse.TabIndex = 2;
            this.listBoxAvailableCourse.ValueMember = "Id";
            this.listBoxAvailableCourse.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableCourse_SelectedIndexChanged);
            // 
            // courseBindingSource
            // 
            this.courseBindingSource.DataMember = "course";
            this.courseBindingSource.DataSource = this.viduDBDataSet4;
            // 
            // viduDBDataSet4
            // 
            this.viduDBDataSet4.DataSetName = "viduDBDataSet4";
            this.viduDBDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listBoxSelectedCourse
            // 
            this.listBoxSelectedCourse.FormattingEnabled = true;
            this.listBoxSelectedCourse.ItemHeight = 16;
            this.listBoxSelectedCourse.Location = new System.Drawing.Point(459, 167);
            this.listBoxSelectedCourse.Name = "listBoxSelectedCourse";
            this.listBoxSelectedCourse.Size = new System.Drawing.Size(252, 132);
            this.listBoxSelectedCourse.TabIndex = 2;
            this.listBoxSelectedCourse.SelectedIndexChanged += new System.EventHandler(this.listBoxSelectedCourse_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(590, 68);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // courseTableAdapter
            // 
            this.courseTableAdapter.ClearBeforeFill = true;
            // 
            // comboBoxstudentID
            // 
            this.comboBoxstudentID.DataSource = this.stdBindingSource;
            this.comboBoxstudentID.DisplayMember = "id";
            this.comboBoxstudentID.FormattingEnabled = true;
            this.comboBoxstudentID.Location = new System.Drawing.Point(176, 71);
            this.comboBoxstudentID.Name = "comboBoxstudentID";
            this.comboBoxstudentID.Size = new System.Drawing.Size(143, 24);
            this.comboBoxstudentID.TabIndex = 5;
            this.comboBoxstudentID.ValueMember = "id";
            this.comboBoxstudentID.SelectedIndexChanged += new System.EventHandler(this.comboBoxstudentID_SelectedIndexChanged);
            // 
            // stdBindingSource
            // 
            this.stdBindingSource.DataMember = "std";
            this.stdBindingSource.DataSource = this.viduDBDataSet1BindingSource;
            // 
            // viduDBDataSet1BindingSource
            // 
            this.viduDBDataSet1BindingSource.DataSource = this.viduDBDataSet1;
            this.viduDBDataSet1BindingSource.Position = 0;
            // 
            // viduDBDataSet1
            // 
            this.viduDBDataSet1.DataSetName = "viduDBDataSet1";
            this.viduDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stdTableAdapter
            // 
            this.stdTableAdapter.ClearBeforeFill = true;
            // 
            // AddCourseStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBoxstudentID);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listBoxSelectedCourse);
            this.Controls.Add(this.listBoxAvailableCourse);
            this.Controls.Add(this.btb_Save);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddCourseStudent";
            this.Text = "AddCourseStudent";
            this.Load += new System.EventHandler(this.AddCourseStudent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viduDBDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btb_Save;
        private System.Windows.Forms.ListBox listBoxAvailableCourse;
        private System.Windows.Forms.ListBox listBoxSelectedCourse;
        private System.Windows.Forms.ComboBox comboBox1;
        private viduDBDataSet4 viduDBDataSet4;
        private System.Windows.Forms.BindingSource courseBindingSource;
        private viduDBDataSet4TableAdapters.courseTableAdapter courseTableAdapter;
        private System.Windows.Forms.ComboBox comboBoxstudentID;
        private System.Windows.Forms.BindingSource viduDBDataSet1BindingSource;
        private viduDBDataSet1 viduDBDataSet1;
        private System.Windows.Forms.BindingSource stdBindingSource;
        private viduDBDataSet1TableAdapters.stdTableAdapter stdTableAdapter;
    }
}