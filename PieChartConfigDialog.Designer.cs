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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxExploded = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCategoryValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCategoryName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnMoveUpCategory = new System.Windows.Forms.Button();
            this.btnMoveDownCategory = new System.Windows.Forms.Button();
            this.btnRemoveCategory = new System.Windows.Forms.Button();
            this.lbCategories = new System.Windows.Forms.ListBox();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBoxScale = new System.Windows.Forms.GroupBox();
            this.numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            this.groupBoxStyle = new System.Windows.Forms.GroupBox();
            this.checkBoxLabels = new System.Windows.Forms.CheckBox();
            this.checkBoxDonut = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxAngle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAngle1)).BeginInit();
            this.groupBoxOutline.SuspendLayout();
            this.groupBoxScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScale)).BeginInit();
            this.groupBoxStyle.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxExploded);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pnlColor);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbCategoryValue);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbCategoryName);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 154);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Pie Chart Slice";
            // 
            // checkBoxExploded
            // 
            this.checkBoxExploded.AutoSize = true;
            this.checkBoxExploded.Location = new System.Drawing.Point(74, 99);
            this.checkBoxExploded.Name = "checkBoxExploded";
            this.checkBoxExploded.Size = new System.Drawing.Size(15, 14);
            this.checkBoxExploded.TabIndex = 8;
            this.checkBoxExploded.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Exploded:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(6, 123);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(248, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add Slice";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Color:";
            // 
            // pnlColor
            // 
            this.pnlColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlColor.BackColor = System.Drawing.Color.Red;
            this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColor.Location = new System.Drawing.Point(74, 71);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(180, 20);
            this.pnlColor.TabIndex = 4;
            this.pnlColor.Click += new System.EventHandler(this.pnlColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Value:";
            // 
            // tbCategoryValue
            // 
            this.tbCategoryValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCategoryValue.Location = new System.Drawing.Point(74, 45);
            this.tbCategoryValue.Name = "tbCategoryValue";
            this.tbCategoryValue.Size = new System.Drawing.Size(180, 20);
            this.tbCategoryValue.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // tbCategoryName
            // 
            this.tbCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCategoryName.Location = new System.Drawing.Point(74, 19);
            this.tbCategoryName.Name = "tbCategoryName";
            this.tbCategoryName.Size = new System.Drawing.Size(180, 20);
            this.tbCategoryName.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnMoveUpCategory);
            this.groupBox3.Controls.Add(this.btnMoveDownCategory);
            this.groupBox3.Controls.Add(this.btnRemoveCategory);
            this.groupBox3.Controls.Add(this.lbCategories);
            this.groupBox3.Location = new System.Drawing.Point(12, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 134);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pie Chart Slices";
            // 
            // btnMoveUpCategory
            // 
            this.btnMoveUpCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveUpCategory.Location = new System.Drawing.Point(228, 19);
            this.btnMoveUpCategory.Name = "btnMoveUpCategory";
            this.btnMoveUpCategory.Size = new System.Drawing.Size(26, 23);
            this.btnMoveUpCategory.TabIndex = 1;
            this.btnMoveUpCategory.Text = "▲";
            this.btnMoveUpCategory.UseVisualStyleBackColor = true;
            this.btnMoveUpCategory.Click += new System.EventHandler(this.btnMoveUpCategory_Click);
            // 
            // btnMoveDownCategory
            // 
            this.btnMoveDownCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveDownCategory.Location = new System.Drawing.Point(228, 48);
            this.btnMoveDownCategory.Name = "btnMoveDownCategory";
            this.btnMoveDownCategory.Size = new System.Drawing.Size(26, 23);
            this.btnMoveDownCategory.TabIndex = 1;
            this.btnMoveDownCategory.Text = "▼";
            this.btnMoveDownCategory.UseVisualStyleBackColor = true;
            this.btnMoveDownCategory.Click += new System.EventHandler(this.btnMoveDownCategory_Click);
            // 
            // btnRemoveCategory
            // 
            this.btnRemoveCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveCategory.Location = new System.Drawing.Point(228, 104);
            this.btnRemoveCategory.Name = "btnRemoveCategory";
            this.btnRemoveCategory.Size = new System.Drawing.Size(26, 23);
            this.btnRemoveCategory.TabIndex = 1;
            this.btnRemoveCategory.Text = "X";
            this.btnRemoveCategory.UseVisualStyleBackColor = true;
            this.btnRemoveCategory.Click += new System.EventHandler(this.btnRemoveCategory_Click);
            // 
            // lbCategories
            // 
            this.lbCategories.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbCategories.FormattingEnabled = true;
            this.lbCategories.Location = new System.Drawing.Point(6, 19);
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Size = new System.Drawing.Size(216, 108);
            this.lbCategories.TabIndex = 0;
            this.lbCategories.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbCategories_DrawItem);
            this.lbCategories.SelectedIndexChanged += new System.EventHandler(this.lbCategories_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(116, 452);
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
            this.btnCancel.Location = new System.Drawing.Point(197, 452);
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
            this.groupBoxAngle.Location = new System.Drawing.Point(101, 326);
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
            this.txtAngle1.Size = new System.Drawing.Size(60, 20);
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
            this.groupBoxOutline.Location = new System.Drawing.Point(12, 326);
            this.groupBoxOutline.Name = "groupBoxOutline";
            this.groupBoxOutline.Size = new System.Drawing.Size(82, 108);
            this.groupBoxOutline.TabIndex = 3;
            this.groupBoxOutline.TabStop = false;
            this.groupBoxOutline.Text = "Outline Color";
            // 
            // radioNone
            // 
            this.radioNone.AutoSize = true;
            this.radioNone.Location = new System.Drawing.Point(6, 18);
            this.radioNone.Name = "radioNone";
            this.radioNone.Size = new System.Drawing.Size(51, 17);
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
            this.radioGray.Size = new System.Drawing.Size(47, 17);
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
            this.radioWhite.Size = new System.Drawing.Size(53, 17);
            this.radioWhite.TabIndex = 2;
            this.radioWhite.TabStop = true;
            this.radioWhite.Text = "White";
            this.radioWhite.UseVisualStyleBackColor = true;
            this.radioWhite.CheckedChanged += new System.EventHandler(this.radioWhite_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(12, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 1);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Location = new System.Drawing.Point(12, 442);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 1);
            this.panel2.TabIndex = 8;
            // 
            // groupBoxScale
            // 
            this.groupBoxScale.Controls.Add(this.numericUpDownScale);
            this.groupBoxScale.Location = new System.Drawing.Point(190, 326);
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
            this.numericUpDownScale.Size = new System.Drawing.Size(60, 20);
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
            this.groupBoxStyle.Location = new System.Drawing.Point(190, 378);
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
            this.checkBoxLabels.Size = new System.Drawing.Size(57, 17);
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
            this.checkBoxDonut.Size = new System.Drawing.Size(55, 17);
            this.checkBoxDonut.TabIndex = 0;
            this.checkBoxDonut.Text = "Donut";
            this.checkBoxDonut.UseVisualStyleBackColor = true;
            this.checkBoxDonut.CheckedChanged += new System.EventHandler(this.checkBoxDonut_CheckedChanged);
            // 
            // PieChartConfigDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 482);
            this.Controls.Add(this.groupBoxStyle);
            this.Controls.Add(this.groupBoxScale);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxAngle);
            this.Controls.Add(this.groupBoxOutline);
            this.HelpButton = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "PieChartConfigDialog";
            this.Text = "Pie Chart";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.helpButtonClicked);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
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

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCategoryValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCategoryName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMoveUpCategory;
        private System.Windows.Forms.Button btnMoveDownCategory;
        private System.Windows.Forms.Button btnRemoveCategory;
        private System.Windows.Forms.ListBox lbCategories;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxExploded;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBoxScale;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
        private System.Windows.Forms.RadioButton radioNone;
        private System.Windows.Forms.GroupBox groupBoxStyle;
        private System.Windows.Forms.CheckBox checkBoxDonut;
        private System.Windows.Forms.CheckBox checkBoxLabels;
    }
}