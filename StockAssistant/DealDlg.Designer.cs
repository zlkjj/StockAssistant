namespace StockAssistant
{
    partial class DealDlg
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
            this.LableName = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.lablePrice = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelReason = new System.Windows.Forms.Label();
            this.labelPool = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxPool = new System.Windows.Forms.ComboBox();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.comboBoxAlgorithm = new System.Windows.Forms.ComboBox();
            this.labelNumber = new System.Windows.Forms.Label();
            this.textBoxNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LableName
            // 
            this.LableName.AutoSize = true;
            this.LableName.Location = new System.Drawing.Point(66, 20);
            this.LableName.Name = "LableName";
            this.LableName.Size = new System.Drawing.Size(35, 13);
            this.LableName.TabIndex = 0;
            this.LableName.Text = "Name";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(66, 100);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(31, 13);
            this.labelType.TabIndex = 1;
            this.labelType.Text = "Type";
            // 
            // lablePrice
            // 
            this.lablePrice.AutoSize = true;
            this.lablePrice.Location = new System.Drawing.Point(66, 142);
            this.lablePrice.Name = "lablePrice";
            this.lablePrice.Size = new System.Drawing.Size(31, 13);
            this.lablePrice.TabIndex = 2;
            this.lablePrice.Text = "Price";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(66, 180);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(35, 13);
            this.labelCount.TabIndex = 3;
            this.labelCount.Text = "Count";
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new System.Drawing.Point(66, 261);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(44, 13);
            this.labelReason.TabIndex = 4;
            this.labelReason.Text = "Reason";
            // 
            // labelPool
            // 
            this.labelPool.AutoSize = true;
            this.labelPool.Location = new System.Drawing.Point(66, 220);
            this.labelPool.Name = "labelPool";
            this.labelPool.Size = new System.Drawing.Size(28, 13);
            this.labelPool.TabIndex = 5;
            this.labelPool.Text = "Pool";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(126, 13);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 6;
            // 
            // textBoxReason
            // 
            this.textBoxReason.Location = new System.Drawing.Point(126, 254);
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(100, 20);
            this.textBoxReason.TabIndex = 7;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(126, 173);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxCount.TabIndex = 9;
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(126, 135);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(100, 20);
            this.textBoxPrice.TabIndex = 10;
            // 
            // textBoxType
            // 
            this.textBoxType.Location = new System.Drawing.Point(126, 93);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.ReadOnly = true;
            this.textBoxType.Size = new System.Drawing.Size(100, 20);
            this.textBoxType.TabIndex = 11;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(21, 373);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(235, 373);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxPool
            // 
            this.comboBoxPool.FormattingEnabled = true;
            this.comboBoxPool.Items.AddRange(new object[] {
            "LongTerm",
            "MidTerm",
            "ShortTerm"});
            this.comboBoxPool.Location = new System.Drawing.Point(126, 212);
            this.comboBoxPool.Name = "comboBoxPool";
            this.comboBoxPool.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPool.TabIndex = 14;
            // 
            // labelAlgorithm
            // 
            this.labelAlgorithm.AutoSize = true;
            this.labelAlgorithm.Location = new System.Drawing.Point(66, 331);
            this.labelAlgorithm.Name = "labelAlgorithm";
            this.labelAlgorithm.Size = new System.Drawing.Size(50, 13);
            this.labelAlgorithm.TabIndex = 15;
            this.labelAlgorithm.Text = "Algorithm";
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Location = new System.Drawing.Point(66, 294);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(30, 13);
            this.labelNote.TabIndex = 16;
            this.labelNote.Text = "Note";
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(126, 291);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(100, 20);
            this.textBoxNote.TabIndex = 17;
            // 
            // comboBoxAlgorithm
            // 
            this.comboBoxAlgorithm.FormattingEnabled = true;
            this.comboBoxAlgorithm.Items.AddRange(new object[] {
            "DefinedPrice",
            "FiveDayCost"});
            this.comboBoxAlgorithm.Location = new System.Drawing.Point(126, 323);
            this.comboBoxAlgorithm.Name = "comboBoxAlgorithm";
            this.comboBoxAlgorithm.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAlgorithm.TabIndex = 18;
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(66, 58);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(44, 13);
            this.labelNumber.TabIndex = 19;
            this.labelNumber.Text = "Number";
            // 
            // textBoxNumber
            // 
            this.textBoxNumber.Location = new System.Drawing.Point(126, 55);
            this.textBoxNumber.Name = "textBoxNumber";
            this.textBoxNumber.Size = new System.Drawing.Size(100, 20);
            this.textBoxNumber.TabIndex = 20;
            // 
            // DealDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 419);
            this.Controls.Add(this.textBoxNumber);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.comboBoxAlgorithm);
            this.Controls.Add(this.textBoxNote);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.comboBoxPool);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxType);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.textBoxReason);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelPool);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.lablePrice);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.LableName);
            this.Name = "DealDlg";
            this.Text = "DealDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LableName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label lablePrice;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.Label labelPool;
        internal System.Windows.Forms.TextBox textBoxName;
        internal System.Windows.Forms.TextBox textBoxReason;
        internal System.Windows.Forms.TextBox textBoxCount;
        internal System.Windows.Forms.TextBox textBoxPrice;
        internal System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        internal System.Windows.Forms.ComboBox comboBoxPool;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelNote;
        internal System.Windows.Forms.TextBox textBoxNote;
        internal System.Windows.Forms.ComboBox comboBoxAlgorithm;
        private System.Windows.Forms.Label labelNumber;
        internal System.Windows.Forms.TextBox textBoxNumber;
    }
}