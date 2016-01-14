using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace StockAssistant
{
    enum StockPoolType
    {
        ObservedPool = 0,
        LongTermPool,
        TacticalPool,
        ShortTermPool,
    }
    public partial class Form1 : Form
    {
        public class StatisticData
        {
            public StatisticData() 
            {
                CostCash = 500000;
                LongTermLeftCash = LongTermAvailableCash = 450000;
                ShortTermLeftCash = 50000;
                curNetCount = 500000;
            }
            /*
             * please note that the profit which is saved in xml file only include the selled stock's profit.
             * but the total profit include not only each pool profit, but also real-time profit of each hold stock
             * */
            public decimal LongTermValue;
            public decimal LongTermProfit;
            public decimal LongTermLeftCash; //real cash
            public decimal LongTermAvailableCash; //available cash for new long term stock
            public decimal TacticalValue;
            public decimal TacticalProfit;
            public decimal ShortTermValue;
            public decimal ShortTermProfit;
            public decimal ShortTermLeftCash;
            public decimal LeftTotalCash; //LongTermLeftCash + ShortTermLeftCash            
            public decimal TotalAsset; //LongTerm value + Tactical value + ShortTerm value + left total Cash
            public decimal TotalProfit; // LongTerm profit + Tactical profit + ShortTerm profit + real-time profit
            public decimal CostCash; //original cash
            public UInt32 curNetCount; //Original, 1 yuan equal 1 share
            public decimal curNetValue; //Total Asset / curNetCount
        }

        public class DealRecordData
        {
            public DealRecordData() { }
            public string Name;
            public string Type;
            public string Price;
            public string Count;
            public string Profit;
            public string Pool;
        }

        private StatisticData m_statData;
        private StockPool m_dt;

        public Form1()
        {
            InitializeComponent();
            m_dt = new StockPool();
        }

        //private void CreateComboxColumn(DataGridView gridview)
        //{
        //    if (gridview.Columns.Contains("AlgorithmType"))
        //        return;

        //    DataGridViewColumn col = gridview.Columns["Algorithm"];
        //    col.Visible = false;

        //    DataGridViewComboBoxColumn cmbox = new DataGridViewComboBoxColumn();

        //    cmbox.HeaderText = "AlgorithmType";
        //    cmbox.Name = "AlgorithmType";   
            
        //    cmbox.Items.AddRange(Enum.GetNames(typeof(AlgorithmType)));
        //    gridview.Columns.Add(cmbox);
        //}

       

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
                        SaveStockPoolFile(dataGridViewTactical,"TacticalPool.xml");
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
            switch(FileName)
            {
                case "ObservedPool.xml":
                    gridview.DataSource = m_dt.ObservedTable;
                    break;
                case "LongTermPool.xml":
                    gridview.DataSource = m_dt.LongTermTable;
                    break;
                case "TacticalPool.xml":
                    gridview.DataSource = m_dt.TacticalTable;
                    break;
                case "ShortTermPool.xml":
                    gridview.DataSource = m_dt.ShortTermTable;
                    break;
                default:
                    return;
            }
            
            DataTable dt = (DataTable)gridview.DataSource;
            if (File.Exists(FileName))
                dt.ReadXml(FileName);

            //CreateComboxColumn(gridview);
            
            //foreach (DataGridViewRow row in gridview.Rows)
            //{
            //    //row.Cells["Algorithm"].Value = (row.Cells["AlgorithmType"] as DataGridViewComboBoxCell).Value;
            //    row.Cells["AlgorithmType"].Value = row.Cells["Algorithm"].Value;
            //}
        }

        private void SaveStockPoolFile(DataGridView gridview, string FileName)
        {
            DataTable dt = (DataTable)gridview.DataSource;
            if (dt == null)
                return;

            //foreach (DataGridViewRow row in gridview.Rows)
            //{
            //    row.Cells["Algorithm"].Value = (row.Cells["AlgorithmType"] as DataGridViewComboBoxCell).Value;
            //}

            dt.WriteXml(FileName);
        }

        private void LoadDealRecordPool()
        {
            //clear old data
            this.listView1.Items.Clear();

            if (!File.Exists("DealRecordPool.xml"))
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load("DealRecordPool.xml");

            XmlNode firstchild = doc.DocumentElement;
            
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

        

        private void SaveDealRecord()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("DealRecordPool"));
            XmlElement root = doc.DocumentElement;            

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
            SaveStockPoolFile(dataGridViewTactical, "TacticalPool.xml");
            SaveStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
            SaveDealRecord();
            NetAssetdataset.WriteXml("NetAsset.xml");
        }

        private void BuyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealDlg dlg = new DealDlg();

            if (this.tabControl1.SelectedIndex == 3)
            {//ShortTermPool
                if (dataGridViewShortTerm.SelectedRows.Count == 1)
                {
                    dlg.textBoxName.Text = dataGridViewShortTerm.SelectedRows[0].Cells["Name"].Value.ToString();
                    dlg.textBoxNumber.Text = dataGridViewShortTerm.SelectedRows[0].Cells["Code"].Value.ToString();
                }
                dlg.textBoxType.Text = "Buy";                   
                        
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    InsertShortTermPool(dlg, dataGridViewShortTerm);
                    DealRecordData data = new DealRecordData();
                    data.Name = dlg.textBoxName.Text;
                    data.Type = dlg.textBoxType.Text;
                    data.Price = dlg.textBoxPrice.Text;
                    data.Count = dlg.textBoxCount.Text;
                    data.Pool = "ShortTermPool";
                    data.Profit = dlg.sellprofit.ToString();
                    InsertDealRecord(data);

                    //update Stastistic data
                    decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                    int count = Convert.ToInt32(dlg.textBoxCount.Text);
                    decimal cash = price * count;
                    m_statData.ShortTermLeftCash -= cash; 
                }                   
            }           
            else
                return;
        } 

        private void InsertShortTermPool(DealDlg dlg, DataGridView gridview)
        {
            StockPool.ShortTermTableDataTable dt = (StockPool.ShortTermTableDataTable)gridview.DataSource;

            StockPool.ShortTermTableRow row = dt.NewShortTermTableRow();           
            row.Name = dlg.textBoxName.Text;
            row.Code = dlg.textBoxNumber.Text;
            row.CostPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
            row.Amount = Convert.ToUInt32(dlg.textBoxCount.Text);            

            dt.Rows.Add(row);
        }

        private void InsertLongTermPool(PositionDlg dlg)
        {
            StockPool.LongTermTableDataTable dt = (StockPool.LongTermTableDataTable)dataGridViewLongTerm.DataSource;
            
            StockPool.LongTermTableRow row = dt.NewLongTermTableRow();
            row.Name = dlg.textBoxName.Text;
            row.Code = dlg.textBoxNumber.Text;
            row.CostPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
            row.Amount = Convert.ToUInt32(dlg.textBoxCount.Text);
            row.ClassType = dlg.comboBoxClassType.SelectedText;
            if (dlg.comboBoxClassType.SelectedText == "High potential")
                row.AvailableCash = row.CostPrice * row.Amount / 2; //for high potential stock, long term position is 50%
            else
                row.AvailableCash = row.CostPrice * row.Amount / 3; //for blue chip stock, long term position is 60%
           
            dt.Rows.Add(row);
        }

        private void InsertTacticalPool(DealDlg dlg, int index)
        {
            StockPool.TacticalTableDataTable dt = (StockPool.TacticalTableDataTable)dataGridViewTactical.DataSource;

            StockPool.TacticalTableRow row = dt.NewTacticalTableRow();
            row.Name = dlg.textBoxName.Text;
            row.Code = dlg.textBoxNumber.Text;
            row.CostPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
            row.DealCount = Convert.ToUInt32(dlg.textBoxCount.Text);

            if (index != -1)
                dt.Rows.InsertAt(row, index);
            else
                dt.Rows.Add(row);            
        }

        private void DeleteFromStockPool(DataGridView gridview, int index)
        {
            DataTable dt = (DataTable)gridview.DataSource;
            dt.Rows.RemoveAt(index);
        }

        private void InsertDealRecord(DealRecordData data)
        {
            ListViewItem item = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd"));
            item.SubItems.Add(data.Name);
            item.SubItems.Add(data.Type);
            item.SubItems.Add(data.Price);
            item.SubItems.Add(data.Count);
            item.SubItems.Add(data.Pool);
            item.SubItems.Add(data.Profit);
            
            listView1.Items.Add(item);           
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
            LoadStockPoolFile(dataGridViewTactical, "TacticalPool.xml");
            LoadStockPoolFile(dataGridViewShortTerm, "ShortTermPool.xml");
            LoadDealRecordPool();

            //Deserialize StatisticData
            if (File.Exists("StatisticData.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(StatisticData));
                m_statData = (StatisticData)xs.Deserialize(new XmlTextReader("StatisticData.xml"));
            }
            else
                m_statData = new StatisticData();

            if (File.Exists("NetAsset.xml"))
                NetAssetdataset.ReadXml("NetAsset.xml");
            else
                NetAssetdataset = new AssetDS();

            this.tabControl1.SelectedIndex = 0;
        }

        private void sellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DealDlg dlg = new DealDlg();

            if (this.tabControl1.SelectedIndex == 3)
            {//ShortTermPool
                if (dataGridViewShortTerm.SelectedRows.Count != 1)
                    return;

                dlg.textBoxName.Text = dataGridViewShortTerm.SelectedRows[0].Cells["Name"].Value.ToString();
                dlg.textBoxNumber.Text = dataGridViewShortTerm.SelectedRows[0].Cells["Code"].Value.ToString();
                dlg.textBoxType.Text = "Sell";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //decrese count from specific stockpool, if left count is 0, then delete item
                    decimal oriPrice = Convert.ToDecimal(dataGridViewShortTerm.SelectedRows[0].Cells["CostPrice"].Value.ToString());
                    decimal sellPrice = Convert.ToDecimal(dlg.textBoxPrice.Text);
                    dlg.sellprofit = (sellPrice - oriPrice) / oriPrice;
                    int count = Convert.ToInt32(dlg.textBoxCount.Text);
                    int current = Convert.ToInt32(dataGridViewShortTerm.SelectedRows[0].Cells["Amount"].Value.ToString());
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
                        DeleteFromStockPool(dataGridViewShortTerm, dataGridViewShortTerm.SelectedRows[0].Index);
                    }
                    else
                    {
                        dataGridViewShortTerm.SelectedRows[0].Cells["Amount"].Value = left;
                    }

                    //insert DealRecord
                    DealRecordData data = new DealRecordData();
                    data.Name = dlg.textBoxName.Text;
                    data.Type = dlg.textBoxType.Text;
                    data.Price = dlg.textBoxPrice.Text;
                    data.Count = dlg.textBoxCount.Text;
                    data.Pool = "ShortTermPool";
                    data.Profit = dlg.sellprofit.ToString();
                    InsertDealRecord(data);

                    //update Stastistic data      
                    m_statData.ShortTermProfit += dlg.sellprofit;
                    decimal cash = sellPrice * count;
                    m_statData.ShortTermLeftCash += cash;
                }                   
            }           
            else
                return;
        }       

        private void RefreshObservedPool()
        {
            StockPool.ObservedTableDataTable dt = (StockPool.ObservedTableDataTable)dataGridViewObserved.DataSource;
            foreach(StockPool.ObservedTableRow row in dt.Rows)
            {    
                string stocknumber = row.Code;
                decimal price = WebUtil.GetCurrentPrice(stocknumber);
                if (price != 0)
                    row.CurrentPrice = price;
            }            
        }
        private void RefreshTacticalPool()
        {
            StockPool.TacticalTableDataTable dt = (StockPool.TacticalTableDataTable)dataGridViewObserved.DataSource;
            foreach (StockPool.TacticalTableRow row in dt.Rows)
            {
                string stocknumber = row.Code;
                decimal price = WebUtil.GetCurrentPrice(stocknumber);
                if (price != 0)
                    row.CurrentPrice = price;
            }
        }
        private void RefreshLongTermPool()
        {
            StockPool.LongTermTableDataTable dt = (StockPool.LongTermTableDataTable)dataGridViewObserved.DataSource;
            foreach (StockPool.LongTermTableRow row in dt.Rows)
            {
                string stocknumber = row.Code;
                decimal price = WebUtil.GetCurrentPrice(stocknumber);
                if (price != 0)
                    row.CurrentPrice = price;
            }
        }
        private void RefreshShortTermPool()
        {
            StockPool.ShortTermTableDataTable dt = (StockPool.ShortTermTableDataTable)dataGridViewObserved.DataSource;
            foreach (StockPool.ShortTermTableRow row in dt.Rows)
            {
                string stocknumber = row.Code;
                decimal price = WebUtil.GetCurrentPrice(stocknumber);
                if (price != 0)
                    row.CurrentPrice = price;
            }
        }

        private void DeleteDealRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in this.listView1.SelectedItems)
            {
                this.listView1.Items.RemoveAt(item.Index);
            }
            
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
                    return this.dataGridViewTactical;
                case 3:
                    return this.dataGridViewShortTerm;
                default:
                    return null;
            }
        }

        //For ObservedPool and LongTerm pool
        private void openPosittionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PositionDlg dlg = new PositionDlg();

            switch (this.tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        if (dataGridViewObserved.SelectedRows.Count != 1)
                            return;

                        dlg.textBoxName.Text = dataGridViewObserved.SelectedRows[0].Cells["Name"].Value.ToString();
                        dlg.textBoxNumber.Text = dataGridViewObserved.SelectedRows[0].Cells["Code"].Value.ToString();
                        dlg.textBoxType.Text = "OpenPosition";
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            InsertLongTermPool(dlg);
                            //InsertTacticalPool(dlg);
                            DealRecordData data = new DealRecordData();
                            data.Name = dlg.textBoxName.Text;
                            data.Type = dlg.textBoxType.Text;
                            data.Price = dlg.textBoxPrice.Text;
                            data.Count = dlg.textBoxCount.Text;
                            data.Pool = "LongTermPool";
                            data.Profit = dlg.sellprofit.ToString();
                            InsertDealRecord(data);
                            DeleteFromStockPool(dataGridViewObserved, dataGridViewObserved.SelectedRows[0].Index);
                            //update statistic data
                            decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                            uint count = Convert.ToUInt32(dlg.textBoxCount.Text);
                            if (dlg.comboBoxClassType.SelectedText == "High potential")
                                m_statData.LongTermAvailableCash -= price*count*2;
                            else
                                m_statData.LongTermAvailableCash -= price*count*5/3;
                            m_statData.LongTermLeftCash -= price * count;
                        }
                    }
                    break;
                case 1:
                    {
                        if (dataGridViewObserved.SelectedRows.Count != 0)
                            return;
                       
                        dlg.textBoxType.Text = "OpenPosition";
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            InsertLongTermPool(dlg);
                            //InsertTacticalPool(dlg);
                            DealRecordData data = new DealRecordData();
                            data.Name = dlg.textBoxName.Text;
                            data.Type = dlg.textBoxType.Text;
                            data.Price = dlg.textBoxPrice.Text;
                            data.Count = dlg.textBoxCount.Text;
                            data.Pool = "LongTermPool";
                            data.Profit = dlg.sellprofit.ToString();
                            InsertDealRecord(data);
                            
                            //update statistic data
                            decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                            uint count = Convert.ToUInt32(dlg.textBoxCount.Text);
                            if (dlg.comboBoxClassType.SelectedText == "High potential")
                                m_statData.LongTermAvailableCash -= price * count * 2;
                            else
                                m_statData.LongTermAvailableCash -= price * count * 5 / 3;
                            m_statData.LongTermLeftCash -= price * count;
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        private void closePositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 1)
                return;

            PositionDlg dlg = new PositionDlg();

            string name = dataGridViewLongTerm.SelectedRows[0].Cells["Name"].Value.ToString();
            StockPool.TacticalTableDataTable dt = (StockPool.TacticalTableDataTable)dataGridViewTactical.DataSource;
            if (dt.Select("Name=" + name).Length != 0)
            {
                MessageBox.Show("please underweight stock in tactical pool first");
                return;
            }           

            dlg.textBoxName.Text = dataGridViewLongTerm.SelectedRows[0].Cells["Name"].Value.ToString();
            dlg.textBoxNumber.Text = dataGridViewLongTerm.SelectedRows[0].Cells["Code"].Value.ToString();
            dlg.textBoxType.Text = "ClosePosition";
            dlg.textBoxCount.Text = dataGridViewLongTerm.SelectedRows[0].Cells["Amount"].Value.ToString();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //update statistic data
                StockPool.LongTermTableDataTable longtermdt = (StockPool.LongTermTableDataTable)dataGridViewLongTerm.DataSource;
                StockPool.LongTermTableRow row = (StockPool.LongTermTableRow)longtermdt.Rows[dataGridViewLongTerm.SelectedRows[0].Index];
                decimal sellprice = Convert.ToDecimal(dlg.textBoxPrice.Text);
                decimal profit = (sellprice - row.CostPrice) * row.Amount;
                m_statData.LongTermProfit += profit;
                m_statData.LongTermLeftCash += sellprice * row.Amount;
                m_statData.LongTermAvailableCash += sellprice * row.Amount + row.AvailableCash;
                //insertDealRecord
                DealRecordData data = new DealRecordData();
                data.Name = dlg.textBoxName.Text;
                data.Type = dlg.textBoxType.Text;
                data.Price = dlg.textBoxPrice.Text;
                data.Count = dlg.textBoxCount.Text;
                data.Pool = "LongTermPool";
                data.Profit = profit.ToString() ;
                InsertDealRecord(data);
                //delete longterm record
                DeleteFromStockPool(dataGridViewLongTerm, dataGridViewLongTerm.SelectedRows[0].Index);
            }
            
        }

        private void overweightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 2)
                return;

            DealDlg dlg = new DealDlg();
            dlg.textBoxName.Text = dataGridViewTactical.SelectedRows[0].Cells["Name"].Value.ToString();
            dlg.textBoxNumber.Text = dataGridViewTactical.SelectedRows[0].Cells["Code"].Value.ToString();
            dlg.textBoxType.Text = "OverWeight";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //update stock available cash in long term pool
                decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                uint count = Convert.ToUInt16(dlg.textBoxCount.Text);
                StockPool.LongTermTableDataTable dt = (StockPool.LongTermTableDataTable)dataGridViewLongTerm.DataSource;
                StockPool.LongTermTableRow row = (StockPool.LongTermTableRow)dt.Select("Name=" + dlg.textBoxName.Text)[0];
                row.AvailableCash -= price * count;
                m_statData.LongTermLeftCash -= price * count;
                 
                //insert tactical record
                InsertTacticalPool(dlg,dataGridViewTactical.SelectedRows[0].Index+1);

                //insert deal record
                DealRecordData data = new DealRecordData();
                data.Name = dlg.textBoxName.Text;
                data.Type = dlg.textBoxType.Text;
                data.Price = dlg.textBoxPrice.Text;
                data.Count = dlg.textBoxCount.Text;
                data.Pool = "TacticalPool";
                data.Profit = dlg.sellprofit.ToString();
                InsertDealRecord(data);
            }
        }

        private void underwweightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 2)
                return;

            DealDlg dlg = new DealDlg();
            dlg.textBoxName.Text = dataGridViewTactical.SelectedRows[0].Cells["Name"].Value.ToString();
            dlg.textBoxNumber.Text = dataGridViewTactical.SelectedRows[0].Cells["Code"].Value.ToString();
            dlg.textBoxCount.Text = dataGridViewTactical.SelectedRows[0].Cells["DealCount"].Value.ToString();
            dlg.textBoxType.Text = "UnderWeight";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                uint count = Convert.ToUInt16(dlg.textBoxCount.Text);
                StockPool.LongTermTableDataTable dt = (StockPool.LongTermTableDataTable)dataGridViewLongTerm.DataSource;
                StockPool.LongTermTableRow row = (StockPool.LongTermTableRow)dt.Select("Name=" + dlg.textBoxName.Text)[0];
                decimal profit = (price - row.CostPrice) * count;
                row.AvailableCash += price * count;
                m_statData.TacticalProfit += profit;                
                m_statData.LongTermLeftCash += price * count; //real left cash

                //delete tactical record
                DeleteFromStockPool(dataGridViewTactical, dataGridViewTactical.SelectedRows[0].Index);

                //insert deal record
                DealRecordData data = new DealRecordData();
                data.Name = dlg.textBoxName.Text;
                data.Type = dlg.textBoxType.Text;
                data.Price = dlg.textBoxPrice.Text;
                data.Count = dlg.textBoxCount.Text;
                data.Pool = "TacticalPool";
                data.Profit = dlg.sellprofit.ToString();
                InsertDealRecord(data);
            }
        }

        private void underPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 1)
                return;


        }

        private void overweightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 1)
                return;

            DealDlg dlg = new DealDlg();
            dlg.textBoxName.Text = dataGridViewLongTerm.SelectedRows[0].Cells["Name"].Value.ToString();
            dlg.textBoxNumber.Text = dataGridViewLongTerm.SelectedRows[0].Cells["Code"].Value.ToString();
            dlg.textBoxType.Text = "OverWeight";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //update stock available cash in long term pool
                decimal price = Convert.ToDecimal(dlg.textBoxPrice.Text);
                uint count = Convert.ToUInt16(dlg.textBoxCount.Text);
                StockPool.LongTermTableDataTable dt = (StockPool.LongTermTableDataTable)dataGridViewLongTerm.DataSource;
                StockPool.LongTermTableRow row = (StockPool.LongTermTableRow)dt.Rows[dataGridViewLongTerm.SelectedRows[0].Index];
                row.AvailableCash -= price * count;
                m_statData.LongTermLeftCash -= price * count;

                //insert tactical record
                InsertTacticalPool(dlg,-1);

                //insert deal record
                DealRecordData data = new DealRecordData();
                data.Name = dlg.textBoxName.Text;
                data.Type = dlg.textBoxType.Text;
                data.Price = dlg.textBoxPrice.Text;
                data.Count = dlg.textBoxCount.Text;
                data.Pool = "TacticalPool";
                data.Profit = dlg.sellprofit.ToString();
                InsertDealRecord(data);
            }
        }

        private void currentStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticInfo dlg = new StatisticInfo();
            //refresh current data
            RefreshObservedPool();
            RefreshLongTermPool();
            RefreshShortTermPool();
            RefreshTacticalPool();
            //calculate statistic data            
            decimal longtermprofit = m_statData.LongTermProfit;
            decimal shorttermprofit = m_statData.ShortTermProfit;
            decimal tacticalprofit = m_statData.TacticalProfit;

            StockPool.LongTermTableDataTable longdt = (StockPool.LongTermTableDataTable)dataGridViewObserved.DataSource;
            foreach (StockPool.LongTermTableRow row in longdt.Rows)
            {
                m_statData.LongTermValue += row.CurrentPrice * row.Amount;
                longtermprofit += (row.CurrentPrice - row.CostPrice) * row.Amount;                
            }
            StockPool.TacticalTableDataTable tacticaldt = (StockPool.TacticalTableDataTable)dataGridViewTactical.DataSource;
            foreach (StockPool.TacticalTableRow row in tacticaldt.Rows)
            {
                m_statData.TacticalValue += row.CurrentPrice * row.DealCount;
                tacticalprofit += (row.CurrentPrice - row.CostPrice) * row.DealCount;
            }
            StockPool.ShortTermTableDataTable shortdt = (StockPool.ShortTermTableDataTable)dataGridViewTactical.DataSource;
            foreach (StockPool.ShortTermTableRow row in shortdt.Rows)
            {
                m_statData.ShortTermValue += row.CurrentPrice * row.Amount;
                shorttermprofit += (row.CurrentPrice - row.CostPrice) * row.Amount;
            }

            //display
            dlg.textBoxLongTermValue.Text = m_statData.LongTermValue.ToString();
            dlg.textBoxLongProfit.Text = longtermprofit.ToString();
            dlg.textBoxLongLeftCash.Text = m_statData.LongTermLeftCash.ToString();
            dlg.textBoxLongAvailable.Text = m_statData.LongTermAvailableCash.ToString();
            dlg.textBoxTacticalValue.Text = m_statData.TacticalValue.ToString();
            dlg.textBoxTacticalProfit.Text = tacticalprofit.ToString();
            dlg.textBoxShortValue.Text = m_statData.ShortTermValue.ToString();
            dlg.textBoxShortProfit.Text = shorttermprofit.ToString();
            dlg.textBoxShortLeft.Text = m_statData.ShortTermLeftCash.ToString();
            dlg.textBoxTotalLeft.Text = (m_statData.LongTermLeftCash + m_statData.ShortTermLeftCash).ToString();
            m_statData.TotalAsset = m_statData.LongTermLeftCash + m_statData.ShortTermLeftCash + m_statData.LongTermValue + m_statData.TacticalValue + m_statData.ShortTermValue;
            dlg.textBoxTotalAsset.Text = m_statData.TotalAsset.ToString();
            m_statData.TotalProfit = longtermprofit + tacticalprofit + shorttermprofit;
            dlg.textBoxTotalProfit.Text = m_statData.TotalProfit.ToString();
            dlg.textBoxCostCash.Text = m_statData.CostCash.ToString();
            dlg.textBoxNetCount.Text = m_statData.curNetCount.ToString();
            m_statData.curNetValue = m_statData.TotalAsset / m_statData.curNetCount;
            dlg.textBoxNetValue.Text = m_statData.curNetValue.ToString();
            dlg.ShowDialog();
        }

        private void historyNetValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //add record if this is of new day
            AssetDS.NetValueDTDataTable dt = NetAssetdataset.NetValueDT;
            AssetDS.NetValueDTRow row = dt.NewNetValueDTRow();
            row.Date = DateTime.Now.ToString("yyyy-MM-dd");
            row.NetAsset = m_statData.TotalAsset;
            row.NetCount = m_statData.curNetCount;
            row.NetValue = m_statData.curNetValue;

            DataRow findrow = dt.Rows.Find(row.Date);
            if (findrow == null)
                dt.Rows.Add(row);

            NetValue chartform = new NetValue();
            chartform.chart1.DataSource = NetAssetdataset.NetValueDT;
            chartform.chart1.Series[0].XValueMember = "Date";
            chartform.chart1.Series[0].YValueMembers = "NetValue";
            chartform.chart1.DataBind();
            chartform.Show();
        }
       

        
    }
}
