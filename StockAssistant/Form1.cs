using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace StockAssistant
{
    enum StockPoolType
    {
        ObservedPool = 0,
        LongTermPool,
        MidTermPool,
        ShortTermPool,
    }
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void CreateComboxColumn(DataGridView gridview)
        {
            if (gridview.Columns.Contains("AlgorithmType"))
                return;

            DataGridViewColumn col = gridview.Columns["Algorithm"];
            col.Visible = false;

            DataGridViewComboBoxColumn cmbox = new DataGridViewComboBoxColumn();

            cmbox.HeaderText = "AlgorithmType";
            cmbox.Name = "AlgorithmType";   
            
            cmbox.Items.AddRange(Enum.GetNames(typeof(AlgorithmType)));
            gridview.Columns.Add(cmbox);
        }

        private void observedPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            LoadStockPoolFile(dataGridViewObserved, "ObservedPool.xml");

            this.tabControl1.SelectedIndex = 0;
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex < 0)
            {
                System.Diagnostics.Trace.WriteLine("tabcontrol not active when save is clicked");
                return;
            }

            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        SaveStockPoolFile(dataGridViewObserved,"ObservedPool.xml");
                    }
                    break;
                case 1:
                    {
                        SaveStockPoolFile(dataGridViewLongTerm,"LongTermPool.xml");
                    }
                    break;
                case 2:
                    {
                        SaveStockPoolFile(dataGridViewMidTerm,"MidTermPool.xml");
                    }
                    break;
                case 3:
                    {
                        SaveStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
                    }
                    break;
                case 4:
                    {
                        SaveDealRecord();
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void LoadStockPoolFile(DataGridView gridview, string FileName)
        {
            StockPool dataset = new StockPool();
            gridview.DataSource = dataset.ObservedStock;
            CreateComboxColumn(gridview);
            DataTable dt = (DataTable)gridview.DataSource;
            //dt.Clear();
            dt.ReadXml(FileName);
            foreach (DataGridViewRow row in gridview.Rows)
            {
                //row.Cells["Algorithm"].Value = (row.Cells["AlgorithmType"] as DataGridViewComboBoxCell).Value;
                row.Cells["AlgorithmType"].Value = row.Cells["Algorithm"].Value;
            }
        }

        private void SaveStockPoolFile(DataGridView gridview, string FileName)
        {
            DataTable dt = (DataTable)gridview.DataSource;
            if (dt == null)
                return;

            foreach (DataGridViewRow row in gridview.Rows)
            {
                row.Cells["Algorithm"].Value = (row.Cells["AlgorithmType"] as DataGridViewComboBoxCell).Value;
            }

            dt.WriteXml(FileName);
        }

        private void longTermPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStockPoolFile(dataGridViewLongTerm, "LongTermPool.xml");
            this.tabControl1.SelectedIndex = 1;
        }

        private void midTermPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStockPoolFile(dataGridViewMidTerm, "MidTermPool.xml");
            this.tabControl1.SelectedIndex = 2;
        }

        private void shortTermPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
            this.tabControl1.SelectedIndex = 3;
        }

        private void LoadDealRecordPool()
        {
            //clear old data
            this.listView1.Items.Clear();
            
            XmlDocument doc = new XmlDocument();
            doc.Load("DealRecordPool.xml");

            XmlNode firstchild = doc.DocumentElement;
            this.textBoxCash.Text = firstchild.Attributes["cash"].Value;

            foreach (XmlNode node in firstchild.ChildNodes)
            {

                ListViewItem item = null;
                foreach (XmlNode element in node.ChildNodes)
                {
                    if (item == null)
                    {
                        item = new ListViewItem(element.InnerText);
                    }
                    else
                        item.SubItems.Add(element.InnerText);
                }
                this.listView1.Items.Add(item);
            }           
        }

        private void dealRecordPoolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDealRecordPool();
            this.tabControl1.SelectedIndex = 4;
        }

        private void SaveDealRecord()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("DealRecordPool"));
            XmlElement root = doc.DocumentElement;
            root.SetAttribute("cash", this.textBoxCash.Text);

            foreach (ListViewItem item in this.listView1.Items)
            {
                XmlElement child= doc.CreateElement("DealRecord");
                
                for (int j = 0; j < item.SubItems.Count; j++)
                {
                    XmlElement childelement = doc.CreateElement(listView1.Columns[j].Text);
                    childelement.InnerText = item.SubItems[j].Text;
                    child.AppendChild(childelement);                    
                }

                root.AppendChild(child);
            }

            XmlTextWriter writer = new XmlTextWriter("DealRecordPool.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            doc.WriteContentTo(writer);
            writer.Flush();
            writer.Close();
        }

        private void toolStripSaveAll_Click(object sender, EventArgs e)
        {
            SaveStockPoolFile(dataGridViewObserved, "ObservedPool.xml");
            SaveStockPoolFile(dataGridViewLongTerm, "LongTermPool.xml");
            SaveStockPoolFile(dataGridViewMidTerm, "MidTermPool.xml");
            SaveStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
            SaveDealRecord();
            NetAssetdataset.WriteXml("NetAsset.xml");
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealDlg dlg = new DealDlg();

            if (this.tabControl1.SelectedIndex == 0)
            {//ObservedPool
                if (dataGridViewObserved.SelectedRows.Count != 1)
                    return;
                else
                {
                    dlg.textBoxName.Text = dataGridViewObserved.SelectedRows[0].Cells["Name"].Value.ToString();
                    dlg.textBoxType.Text = "Buy";
                    dlg.textBoxNote.Text = dataGridViewObserved.SelectedRows[0].Cells["Note"].Value.ToString();
                    dlg.comboBoxAlgorithm.Text = "FiveDayCost";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        //insert record into dealrecordpool
                        InsertDealRecord(dlg);
                        //delete this item from observedpool
                        DeleteFromStockPool(dataGridViewObserved, dataGridViewObserved.SelectedRows[0].Index);
                        
                        //insert item into specific stockpool
                        if (dlg.comboBoxPool.Text == "LongTerm")
                        {
                            InsertStockPool(dlg, dataGridViewLongTerm);
                        }
                        else if (dlg.comboBoxPool.Text == "MidTerm")
                        {
                            InsertStockPool(dlg, dataGridViewMidTerm);
                        }
                        else
                        {
                            InsertStockPool(dlg, dataGridViewShortTerm);
                        }
                    }
                }
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                ProcessDealBuyForHoldPool(dlg, dataGridViewLongTerm);
            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                ProcessDealBuyForHoldPool(dlg, dataGridViewMidTerm);
            }
            else if (this.tabControl1.SelectedIndex == 3)
            {
                ProcessDealBuyForHoldPool(dlg, dataGridViewShortTerm);
            }
            else
                return;
        }

        private void ProcessDealBuyForHoldPool(DealDlg dlg, DataGridView gridview)
        {
            //LongTerm/MidTerm/ShortTerm stock pool
            if (gridview.SelectedRows.Count != 1)
            {
                if (gridview.SelectedRows.Count == 0)
                {
                    dlg.textBoxType.Text = "Buy";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        InsertStockPool(dlg, gridview);
                        InsertDealRecord(dlg);
                    }
                }
            }               
            else
            {
                dlg.textBoxName.Text = gridview.SelectedRows[0].Cells["Name"].Value.ToString();
                dlg.textBoxNumber.Text = gridview.SelectedRows[0].Cells["Number"].Value.ToString();
                dlg.textBoxType.Text = "Buy";
                //dlg.textBoxNote.Text = gridview.SelectedRows[0].Cells["Note"].Value.ToString();
                // dlg.comboBoxAlgorithm.Text = "FiveDayCost";
                if (dlg.ShowDialog() == DialogResult.OK)
                {                   
                    //increase new item in datagridview
                    InsertStockPool(dlg, gridview);
                    //insert record into dealrecordpool
                    InsertDealRecord(dlg);
                }
            }
        }

        private void ProcessDealSellForHoldPool(DealDlg dlg, DataGridView gridview)
        {
            //LongTerm/MidTerm/ShortTerm stock pool
            if (gridview.SelectedRows.Count != 1)
                return;
            else
            {
                dlg.textBoxName.Text = gridview.SelectedRows[0].Cells["Name"].Value.ToString();
                dlg.textBoxNumber.Text = gridview.SelectedRows[0].Cells["Number"].Value.ToString();
                dlg.textBoxType.Text = "Sell";
                //dlg.textBoxNote.Text = gridview.SelectedRows[0].Cells["Note"].Value.ToString();
                // dlg.comboBoxAlgorithm.Text = "FiveDayCost";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //decrese count from specific stockpool, if left count is 0, then delete item
                    decimal oriPrice = Convert.ToDecimal(gridview.SelectedRows[0].Cells["OriginPrice"].Value.ToString());
                    decimal sellPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
                    dlg.sellprofit = (sellPrice - oriPrice) / oriPrice;
                    int count = Convert.ToInt32(dlg.textBoxCount.Text);
                    int current = Convert.ToInt32(gridview.SelectedRows[0].Cells["Count"].Value.ToString());
                    int left = current - count;
                    if (left < 0)
                    {
                        MessageBox.Show("Count is more than the holding number.");
                        return;
                    }
                    else if (left == 0)
                    {
                        //delete item from gridview
                        //DataRowView view = gridview.SelectedRows[0].DataBoundItem as DataRowView;
                        //int index = view.Row.
                        DeleteFromStockPool(gridview, gridview.SelectedRows[0].Index);
                    }
                    else
                    {
                        gridview.SelectedRows[0].Cells["Count"].Value = left;
                    }
                    //insert record into dealrecordpool
                    InsertDealRecord(dlg);
                }
            }
        }

        private void InsertStockPool(DealDlg dlg, DataGridView gridview)
        {
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)gridview.DataSource;
            
            StockPool.ObservedStockRow row = dt.NewObservedStockRow();           
            row.Name = dlg.textBoxName.Text;
            row.Number = dlg.textBoxNumber.Text;
            row.OriginPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
            row.Count = Convert.ToUInt32(dlg.textBoxCount.Text);
            row.Note = dlg.textBoxNote.Text;
            row.Algorithm = dlg.comboBoxAlgorithm.Text;

            dt.Rows.Add(row);
        }

        private void DeleteFromStockPool(DataGridView gridview, int index)
        {
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)gridview.DataSource;
            dt.Rows.RemoveAt(index);
        }

        private void InsertDealRecord(DealDlg dlg)
        {
            ListViewItem item = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd"));
            item.SubItems.Add(dlg.textBoxName.Text);
            item.SubItems.Add(dlg.textBoxType.Text);
            item.SubItems.Add(dlg.textBoxPrice.Text);
            item.SubItems.Add(dlg.textBoxCount.Text);
            item.SubItems.Add(dlg.comboBoxPool.Text);
            item.SubItems.Add(dlg.sellprofit.ToString());
            item.SubItems.Add(dlg.textBoxReason.Text);

            listView1.Items.Add(item);

            //update cash textbox
            decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
            int count = Convert.ToInt32(dlg.textBoxCount.Text);
            decimal cash = price*count;
            decimal oldCash = Convert.ToDecimal(this.textBoxCash.Text);
            decimal newCash;
            if (dlg.textBoxType.Text == "Buy")
            {
                newCash = oldCash - cash;
            }
            else
            {
                newCash = oldCash + cash;
            }
            this.textBoxCash.Text = newCash.ToString();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButtonLoad_Click(object sender, EventArgs e)
        {
            
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadStockPoolFile(dataGridViewObserved, "ObservedPool.xml");
            LoadStockPoolFile(dataGridViewLongTerm, "LongTermPool.xml");
            LoadStockPoolFile(dataGridViewMidTerm, "MidTermPool.xml");
            LoadStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
            LoadDealRecordPool();
            NetAssetdataset.ReadXml("NetAsset.xml");
            this.tabControl1.SelectedIndex = 0;
        }

        private void BtnAsset_Click(object sender, EventArgs e)
        {
            decimal asset = 0;
            asset += CalculateAsset(dataGridViewLongTerm);
            asset += CalculateAsset(dataGridViewMidTerm);
            asset += CalculateAsset(dataGridViewShortTerm);
            decimal cash = Convert.ToDecimal(this.textBoxCash.Text);
            asset += cash;

            this.textBox1.Text = asset.ToString();

            //add record if this is of new day
            AssetDS.NetValueDTDataTable dt = NetAssetdataset.NetValueDT;
            AssetDS.NetValueDTRow row = dt.NewNetValueDTRow();
            row.Date = DateTime.Now.ToString("yyyy-MM-dd");
            row.NetAsset = asset;
            row.NetCount = 700000;

            DataRow findrow = dt.Rows.Find(row.Date);
            if (findrow == null)
                dt.Rows.Add(row);

        }

        private decimal CalculateAsset(DataGridView gridview)
        {
            decimal asset = 0;
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)gridview.DataSource;

            foreach (StockPool.ObservedStockRow row in dt.Rows)
            {
                asset += row.CurrentPrice * row.Count;
            }

            return asset;
        }

        private void sellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealDlg dlg = new DealDlg();

            if (this.tabControl1.SelectedIndex == 0)
            {//ObservedPool
                return;
            }
            else if (this.tabControl1.SelectedIndex == 1)
            {
                ProcessDealSellForHoldPool(dlg, dataGridViewLongTerm);
            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                ProcessDealSellForHoldPool(dlg, dataGridViewMidTerm);
            }
            else if (this.tabControl1.SelectedIndex == 3)
            {
                ProcessDealSellForHoldPool(dlg, dataGridViewShortTerm);
            }
            else
                return;
        }

        private void toolStripCalculate_Click(object sender, EventArgs e)
        {
            RefreshPrice(dataGridViewObserved);
            RefreshPrice(dataGridViewLongTerm);
            RefreshPrice(dataGridViewMidTerm);
            RefreshPrice(dataGridViewShortTerm);
        }

        private void RefreshPrice(DataGridView gridview)
        {
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)gridview.DataSource;

            foreach (StockPool.ObservedStockRow row in dt.Rows)
            {
                string stocknumber = row.Number;
                decimal price = WebUtil.GetCurrentPrice(stocknumber);
                if (price != 0)
                    row.CurrentPrice = price;
            }
        }

        private void dataGridViewLongTerm_Sorted(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Sorted");
            
            string colName = this.dataGridViewLongTerm.SortedColumn.Name;
            SortOrder order = this.dataGridViewLongTerm.SortOrder;
            if (order == SortOrder.Ascending)
            {
                colName += " ASC";
            }
            else
            {
                colName += " DESC";
            }
            StockPool ds = new StockPool();
           
            //dataGridViewLongTerm.
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)this.dataGridViewLongTerm.DataSource;

            //StockPool ds = (StockPool)dt.DataSet;

            ds.Merge(dt.Select("", colName));

            this.dataGridViewLongTerm.DataSource = ds.ObservedStock;
        }
        private void dataGridViewMidTerm_Sorted(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Sorted");

            string colName = this.dataGridViewMidTerm.SortedColumn.Name;
            SortOrder order = this.dataGridViewMidTerm.SortOrder;
            if (order == SortOrder.Ascending)
            {
                colName += " ASC";
            }
            else
            {
                colName += " DESC";
            }
            StockPool ds = new StockPool();

            //dataGridViewMidTerm.
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)this.dataGridViewMidTerm.DataSource;

            //StockPool ds = (StockPool)dt.DataSet;

            ds.Merge(dt.Select("", colName));

            this.dataGridViewMidTerm.DataSource = ds.ObservedStock;
        }
        private void dataGridViewShortTerm_Sorted(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Sorted");

            string colName = this.dataGridViewShortTerm.SortedColumn.Name;
            SortOrder order = this.dataGridViewShortTerm.SortOrder;
            if (order == SortOrder.Ascending)
            {
                colName += " ASC";
            }
            else
            {
                colName += " DESC";
            }
            StockPool ds = new StockPool();

            //dataGridViewShortTerm.
            StockPool.ObservedStockDataTable dt = (StockPool.ObservedStockDataTable)this.dataGridViewShortTerm.DataSource;

            //StockPool ds = (StockPool)dt.DataSet;

            ds.Merge(dt.Select("", colName));

            this.dataGridViewShortTerm.DataSource = ds.ObservedStock;
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in this.listView1.SelectedItems)
            {
                this.listView1.Items.RemoveAt(item.Index);
            }
            
        }

        private void toolStripNetValue_Click(object sender, EventArgs e)
        {
            NetValue chartform = new NetValue();
            chartform.chart1.DataSource = NetAssetdataset.NetValueDT;
            chartform.chart1.Series[0].XValueMember = "Date";
            chartform.chart1.Series[0].YValueMembers = "NetValue";
            chartform.chart1.DataBind();
            chartform.Show();
        }

        private DataGridView GetCurrentGridView()
        {
            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    return this.dataGridViewObserved;
                case 1:
                    return this.dataGridViewLongTerm;
                case 2:
                    return this.dataGridViewMidTerm;
                case 3:
                    return this.dataGridViewShortTerm;
                default:
                    return null;
            }
        }

        private void MoveRow(DataGridView targetView)
        {
            DataGridView gridview = GetCurrentGridView();
            if (gridview == null || gridview.SelectedRows.Count == 0)
                return;

            foreach (DataGridViewRow row in gridview.SelectedRows)
            {
                StockPool.ObservedStockDataTable targetdt = (StockPool.ObservedStockDataTable)targetView.DataSource;
                StockPool.ObservedStockDataTable sourcedt = (StockPool.ObservedStockDataTable)gridview.DataSource;

                targetdt.ImportRow(sourcedt.Rows[row.Index]);
                sourcedt.Rows.RemoveAt(row.Index);
            }
        }

        private void longTermPoolToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1 || this.tabControl1.SelectedIndex == 0)
                return;

            MoveRow(dataGridViewLongTerm);
        }

        private void midTermPoolToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 2 || this.tabControl1.SelectedIndex == 0)
                return;

            MoveRow(dataGridViewMidTerm);
        }

        private void shortTermPoolToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 3 || this.tabControl1.SelectedIndex == 0)
                return;

            MoveRow(dataGridViewShortTerm);
        }

        
    }
}
