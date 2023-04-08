namespace ChartCsApp
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            formsPlot = new ScottPlot.FormsPlot();
            label2 = new Label();
            comboBoxY = new ComboBox();
            label1 = new Label();
            comboBoxX = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            comboBoxType = new ComboBox();
            comboBoxMfr = new ComboBox();
            SuspendLayout();
            // 
            // formsPlot
            // 
            formsPlot.Location = new Point(13, 12);
            formsPlot.Margin = new Padding(4, 3, 4, 3);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new Size(629, 438);
            formsPlot.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 478);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 2;
            label2.Text = "Y";
            // 
            // comboBoxY
            // 
            comboBoxY.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxY.FormattingEnabled = true;
            comboBoxY.Items.AddRange(new object[] { "calories", "protein", "fat", "sodium", "fiber", "carbo", "sugars", "potass", "vitamins", "shelf", "weight", "cups", "rating" });
            comboBoxY.Location = new Point(82, 475);
            comboBoxY.Name = "comboBoxY";
            comboBoxY.Size = new Size(120, 23);
            comboBoxY.TabIndex = 2;
            comboBoxY.SelectedIndexChanged += comboBoxY_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 449);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 2;
            label1.Text = "X";
            // 
            // comboBoxX
            // 
            comboBoxX.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxX.FormattingEnabled = true;
            comboBoxX.Items.AddRange(new object[] { "calories", "protein", "fat", "sodium", "fiber", "carbo", "sugars", "potass", "vitamins", "shelf", "weight", "cups", "rating" });
            comboBoxX.Location = new Point(82, 446);
            comboBoxX.Name = "comboBoxX";
            comboBoxX.Size = new Size(120, 23);
            comboBoxX.TabIndex = 1;
            comboBoxX.SelectedIndexChanged += comboBoxX_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(232, 478);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 5;
            label3.Text = "type";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(232, 449);
            label4.Name = "label4";
            label4.Size = new Size(25, 15);
            label4.TabIndex = 6;
            label4.Text = "mfr";
            // 
            // comboBoxType
            // 
            comboBoxType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxType.FormattingEnabled = true;
            comboBoxType.Location = new Point(263, 475);
            comboBoxType.Name = "comboBoxType";
            comboBoxType.Size = new Size(120, 23);
            comboBoxType.TabIndex = 4;
            comboBoxType.SelectedIndexChanged += comboBoxType_SelectedIndexChanged;
            // 
            // comboBoxMfr
            // 
            comboBoxMfr.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMfr.FormattingEnabled = true;
            comboBoxMfr.Location = new Point(263, 446);
            comboBoxMfr.Name = "comboBoxMfr";
            comboBoxMfr.Size = new Size(120, 23);
            comboBoxMfr.TabIndex = 3;
            comboBoxMfr.SelectedIndexChanged += comboBoxMfr_SelectedIndexChanged;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 516);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(comboBoxType);
            Controls.Add(comboBoxMfr);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBoxY);
            Controls.Add(comboBoxX);
            Controls.Add(formsPlot);
            Name = "Form";
            Text = "chart-cs-app";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.FormsPlot formsPlot;
        private Label label2;
        private ComboBox comboBoxY;
        private Label label1;
        private ComboBox comboBoxX;
        private Label label3;
        private Label label4;
        private ComboBox comboBoxType;
        private ComboBox comboBoxMfr;
    }
}