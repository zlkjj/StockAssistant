namespace StockAssistant
{
    partial class PositionDlg
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
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.labelNumber = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.lablePrice = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.LableName = new System.Windows.Forms.Label();
            this.labelClassType = new System.Windows.Forms.Label();
            this.comboBoxClassType = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(114, 67);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(108, 20);
            this.textBoxNumber.TabIndex = 30;
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(54, 70);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(44, 13);
            this.labelNumber.TabIndex = 29;
            this.labelNumber.Text = "Number";
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(114, 105);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(108, 20);
            this.textBoxType.TabIndex = 28;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(114, 147);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(108, 20);
            this.textBoxPrice.TabIndex = 27;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(114, 185);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(108, 20);
            this.textBoxCount.TabIndex = 26;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(114, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(108, 20);
            this.textBoxName.TabIndex = 25;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(54, 192);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(35, 13);
            this.labelCount.TabIndex = 24;
            this.labelCount.Text = "Count";
            // 
            // lablePrice
            // 
            this.lablePrice.AutoSize = true;
            this.lablePrice.Location = new System.Drawing.Point(54, 154);
            this.lablePrice.Name = "lablePrice";
            this.lablePrice.Size = new System.Drawing.Size(31, 13);
            this.lablePrice.TabIndex = 23;
            this.lablePrice.Text = "Price";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(54, 112);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(31, 13);
            this.labelType.TabIndex = 22;
            this.labelType.Text = "Type";
            // 
            // LableName
            // 
            this.LableName.AutoSize = true;
            this.LableName.Location = new System.Drawing.Point(54, 32);
            this.LableName.Name = "LableName";
            this.LableName.Size = new System.Drawing.Size(35, 13);
            this.LableName.TabIndex = 21;
            this.LableName.Text = "Name";
            // 
            // labelClassType
            // 
            this.labelClassType.AutoSize = true;
            this.labelClassType.Location = new System.Drawing.Point(54, 237);
            this.labelClassType.Name = "labelClassType";
            this.labelClassType.Size = new System.Drawing.Size(56, 13);
            this.labelClassType.TabIndex = 33;
            this.labelClassType.Text = "ClassType";
            // 
            // comboBoxClassType
            // 
            this.comboBoxClassType.FormattingEnabled = true;
            this.comboBoxClassType.Items.AddRange(new object[] {
            "High potential",
            "Blue chip"});
            this.comboBoxClassType.Location = new System.Drawing.Point(114, 229);
            this.comboBoxClassType.Name = "comboBoxClassType";
            this.comboBoxClassType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxClassType.TabIndex = 35;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(177, 287);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 37;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(35, 287);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 36;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // PositionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 322);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxClassType);
            this.Controls.Add(this.labelClassType);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.lablePrice);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.LableName);
            this.Name = "PositionDlg";
            this.Text = "PositionDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBoxNumber;
        private System.Windows.Forms.Label labelNumber;
        internal System.Windows.Forms.TextBox textBoxType;
        internal System.Windows.Forms.TextBox textBoxPrice;
        internal System.Windows.Forms.TextBox textBoxCount;
        internal System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label lablePrice;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label LableName;
        private System.Windows.Forms.Label labelClassType;
        internal System.Windows.Forms.ComboBox comboBoxClassType;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
    }
}