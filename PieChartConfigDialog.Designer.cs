namespace PieChartEffect
{
    partial class PieChartConfigDialog
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColumnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnExploded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBoxAngle = new System.Windows.Forms.GroupBox();
            this.txtAngle1 = new System.Windows.Forms.NumericUpDown();
            this.angleSelector1 = new AngleControl.AngleSelector();
            this.groupBoxOutline = new System.Windows.Forms.GroupBox();
            this.radioNone = new System.Windows.Forms.RadioButton();
            this.radioBlack = new System.Windows.Forms.RadioButton();
            this.radioGray = new System.Windows.Forms.RadioButton();
            this.radioWhite = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxScale = new System.Windows.Forms.GroupBox();
            this.numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            this.groupBoxStyle = new System.Windows.Forms.GroupBox();
            this.checkBoxLabels = new System.Windows.Forms.CheckBox();
            this.checkBoxDonut = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle1)).BeginInit();
            this.groupBoxOutline.SuspendLayout();
            this.groupBoxScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).BeginInit();
            this.groupBoxStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 338);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pie Chart Slices";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(6, 309);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Load CSV";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIcon,
            this.ColumnColor,
            this.ColumnName,
            this.ColumnValue,
            this.ColumnExploded});
            this.dataGridView1.Location = new System.Drawing.Point(6, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(248, 286);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_DefaultValuesNeeded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserAddedRow);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragOver);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseMove);
            // 
            // ColumnIcon
            // 
            this.ColumnIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.ColumnIcon.HeaderText = "";
            this.ColumnIcon.Name = "ColumnIcon";
            this.ColumnIcon.ReadOnly = true;
            this.ColumnIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnIcon.Width = 20;
            // 
            // ColumnColor
            // 
            this.ColumnColor.HeaderText = "colorValue";
            this.ColumnColor.Name = "ColumnColor";
            this.ColumnColor.Visible = false;
            // 
            // ColumnName
            // 
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.Width = 61;
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnValue.HeaderText = "Value";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Width = 61;
            // 
            // ColumnExploded
            // 
            this.ColumnExploded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColumnExploded.HeaderText = "Exploded";
            this.ColumnExploded.Name = "ColumnExploded";
            this.ColumnExploded.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnExploded.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnExploded.Width = 80;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(116, 482);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 482);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBoxAngle
            // 
            this.groupBoxAngle.Controls.Add(this.txtAngle1);
            this.groupBoxAngle.Controls.Add(this.angleSelector1);
            this.groupBoxAngle.Location = new System.Drawing.Point(101, 356);
            this.groupBoxAngle.Name = "groupBoxAngle";
            this.groupBoxAngle.Size = new System.Drawing.Size(82, 108);
            this.groupBoxAngle.TabIndex = 4;
            this.groupBoxAngle.TabStop = false;
            this.groupBoxAngle.Text = "Rotation";
            // 
            // txtAngle1
            // 
            this.txtAngle1.DecimalPlaces = 2;
            this.txtAngle1.Location = new System.Drawing.Point(6, 82);
            this.txtAngle1.Maximum = new decimal(new int[] {
            181,
            0,
            0,
            0});
            this.txtAngle1.Minimum = new decimal(new int[] {
            181,
            0,
            0,
            -2147483648});
            this.txtAngle1.Name = "txtAngle1";
            this.txtAngle1.Size = new System.Drawing.Size(60, 22);
            this.txtAngle1.TabIndex = 0;
            this.txtAngle1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAngle1.ValueChanged += new System.EventHandler(this.txtAngle1_ValueChanged);
            // 
            // angleSelector1
            // 
            this.angleSelector1.Angle = 0D;
            this.angleSelector1.Location = new System.Drawing.Point(6, 16);
            this.angleSelector1.Name = "angleSelector1";
            this.angleSelector1.Size = new System.Drawing.Size(60, 60);
            this.angleSelector1.TabIndex = 1;
            this.angleSelector1.AngleChanged += new AngleControl.AngleSelector.AngleChangedDelegate(this.angleSelector1_AngleChanged);
            // 
            // groupBoxOutline
            // 
            this.groupBoxOutline.Controls.Add(this.radioNone);
            this.groupBoxOutline.Controls.Add(this.radioBlack);
            this.groupBoxOutline.Controls.Add(this.radioGray);
            this.groupBoxOutline.Controls.Add(this.radioWhite);
            this.groupBoxOutline.Location = new System.Drawing.Point(12, 356);
            this.groupBoxOutline.Name = "groupBoxOutline";
            this.groupBoxOutline.Size = new System.Drawing.Size(82, 108);
            this.groupBoxOutline.TabIndex = 3;
            this.groupBoxOutline.TabStop = false;
            this.groupBoxOutline.Text = "Outline";
            // 
            // radioNone
            // 
            this.radioNone.AutoSize = true;
            this.radioNone.Location = new System.Drawing.Point(6, 18);
            this.radioNone.Name = "radioNone";
            this.radioNone.Size = new System.Drawing.Size(53, 17);
            this.radioNone.TabIndex = 3;
            this.radioNone.TabStop = true;
            this.radioNone.Text = "None";
            this.radioNone.UseVisualStyleBackColor = true;
            this.radioNone.CheckedChanged += new System.EventHandler(this.radioNone_CheckedChanged);
            // 
            // radioBlack
            // 
            this.radioBlack.AutoSize = true;
            this.radioBlack.Location = new System.Drawing.Point(6, 40);
            this.radioBlack.Name = "radioBlack";
            this.radioBlack.Size = new System.Drawing.Size(52, 17);
            this.radioBlack.TabIndex = 0;
            this.radioBlack.TabStop = true;
            this.radioBlack.Text = "Black";
            this.radioBlack.UseVisualStyleBackColor = true;
            this.radioBlack.CheckedChanged += new System.EventHandler(this.radioBlack_CheckedChanged);
            // 
            // radioGray
            // 
            this.radioGray.AutoSize = true;
            this.radioGray.Location = new System.Drawing.Point(6, 62);
            this.radioGray.Name = "radioGray";
            this.radioGray.Size = new System.Drawing.Size(48, 17);
            this.radioGray.TabIndex = 1;
            this.radioGray.TabStop = true;
            this.radioGray.Text = "Gray";
            this.radioGray.UseVisualStyleBackColor = true;
            this.radioGray.CheckedChanged += new System.EventHandler(this.radioGray_CheckedChanged);
            // 
            // radioWhite
            // 
            this.radioWhite.AutoSize = true;
            this.radioWhite.Location = new System.Drawing.Point(6, 84);
            this.radioWhite.Name = "radioWhite";
            this.radioWhite.Size = new System.Drawing.Size(56, 17);
            this.radioWhite.TabIndex = 2;
            this.radioWhite.TabStop = true;
            this.radioWhite.Text = "White";
            this.radioWhite.UseVisualStyleBackColor = true;
            this.radioWhite.CheckedChanged += new System.EventHandler(this.radioWhite_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Location = new System.Drawing.Point(12, 472);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 1);
            this.panel2.TabIndex = 8;
            // 
            // groupBoxScale
            // 
            this.groupBoxScale.Controls.Add(this.numericUpDownScale);
            this.groupBoxScale.Location = new System.Drawing.Point(190, 356);
            this.groupBoxScale.Name = "groupBoxScale";
            this.groupBoxScale.Size = new System.Drawing.Size(82, 46);
            this.groupBoxScale.TabIndex = 9;
            this.groupBoxScale.TabStop = false;
            this.groupBoxScale.Text = "Scale";
            // 
            // numericUpDownScale
            // 
            this.numericUpDownScale.DecimalPlaces = 2;
            this.numericUpDownScale.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownScale.Location = new System.Drawing.Point(6, 19);
            this.numericUpDownScale.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownScale.Name = "numericUpDownScale";
            this.numericUpDownScale.Size = new System.Drawing.Size(60, 22);
            this.numericUpDownScale.TabIndex = 1;
            this.numericUpDownScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownScale.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // groupBoxStyle
            // 
            this.groupBoxStyle.Controls.Add(this.checkBoxLabels);
            this.groupBoxStyle.Controls.Add(this.checkBoxDonut);
            this.groupBoxStyle.Location = new System.Drawing.Point(190, 408);
            this.groupBoxStyle.Name = "groupBoxStyle";
            this.groupBoxStyle.Size = new System.Drawing.Size(82, 56);
            this.groupBoxStyle.TabIndex = 10;
            this.groupBoxStyle.TabStop = false;
            this.groupBoxStyle.Text = "Style";
            // 
            // checkBoxLabels
            // 
            this.checkBoxLabels.AutoSize = true;
            this.checkBoxLabels.Location = new System.Drawing.Point(6, 35);
            this.checkBoxLabels.Name = "checkBoxLabels";
            this.checkBoxLabels.Size = new System.Drawing.Size(58, 17);
            this.checkBoxLabels.TabIndex = 1;
            this.checkBoxLabels.Text = "Labels";
            this.checkBoxLabels.UseVisualStyleBackColor = true;
            this.checkBoxLabels.CheckedChanged += new System.EventHandler(this.checkBoxLabels_CheckedChanged);
            // 
            // checkBoxDonut
            // 
            this.checkBoxDonut.AutoSize = true;
            this.checkBoxDonut.Location = new System.Drawing.Point(6, 16);
            this.checkBoxDonut.Name = "checkBoxDonut";
            this.checkBoxDonut.Size = new System.Drawing.Size(59, 17);
            this.checkBoxDonut.TabIndex = 0;
            this.checkBoxDonut.Text = "Donut";
            this.checkBoxDonut.UseVisualStyleBackColor = true;
            this.checkBoxDonut.CheckedChanged += new System.EventHandler(this.checkBoxDonut_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "CSV (*.csv)|*.csv";
            // 
            // PieChartConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(284, 512);
            this.Controls.Add(this.groupBoxStyle);
            this.Controls.Add(this.groupBoxScale);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxAngle);
            this.Controls.Add(this.groupBoxOutline);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PieChartConfigDialog";
            this.Text = "Pie Chart";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.helpButtonClicked);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxAngle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle1)).EndInit();
            this.groupBoxOutline.ResumeLayout(false);
            this.groupBoxOutline.PerformLayout();
            this.groupBoxScale.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).EndInit();
            this.groupBoxStyle.ResumeLayout(false);
            this.groupBoxStyle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBoxAngle;
        private System.Windows.Forms.NumericUpDown txtAngle1;
        private AngleControl.AngleSelector angleSelector1;
        private System.Windows.Forms.GroupBox groupBoxOutline;
        private System.Windows.Forms.RadioButton radioBlack;
        private System.Windows.Forms.RadioButton radioGray;
        private System.Windows.Forms.RadioButton radioWhite;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxScale;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
        private System.Windows.Forms.RadioButton radioNone;
        private System.Windows.Forms.GroupBox groupBoxStyle;
        private System.Windows.Forms.CheckBox checkBoxDonut;
        private System.Windows.Forms.CheckBox checkBoxLabels;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewImageColumn ColumnIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnExploded;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}