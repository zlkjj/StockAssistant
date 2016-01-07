using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockAssistant
{
    public partial class DealDlg : Form
    {
        //private DataGridView m_GridView;
        public DealDlg()
        {
            InitializeComponent();
            //m_GridView = gridview;
            //this.textBoxName.Text = gridview.SelectedRows[0].Cells["Name"].Value.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public decimal sellprofit = 0;
    }
}
