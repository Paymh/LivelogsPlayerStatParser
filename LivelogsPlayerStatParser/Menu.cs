using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace LivelogsPlayerStatParser
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            lstLogsTracked.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExcelHandler.CreateExcel();
            ExcelHandler.ShowExcel();
            List<Log> logsToOutput = new List<Log>();
            foreach(ListViewItem item in lstLogsTracked.Items)
            {
                if (item.Checked)
                    logsToOutput.Add(LogHandler.logsTracked[item.Index]);
            }
            ExcelHandler.OutputExcel();
            
        }

        private void btnParseLog_Click(object sender, EventArgs e)
        {
            bool result = LogHandler.ParseLog(txtLogUri.Text);
            if (result)
            {
                lstLogsTracked.Items.Clear();
                foreach (Log log in LogHandler.logsTracked)
                {
                    ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = log.URL.Replace(@"http://livelogs.ozfortress.com/view/", "");
                    item.SubItems.Add(log.dateTime.ToString());
                    item.SubItems.Add(log.Map);
                    lstLogsTracked.Items.Add(item);
                }
                lstLogsTracked.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            LogHandler.logsTracked.Clear();
            lstLogsTracked.Clear();
        }

        private void btnRemoveLogs_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in lstLogsTracked.Items)
            {
                if(item.Checked)
                {
                    LogHandler.logsTracked.RemoveAt(item.Index);
                    item.Remove();
                }
            }
        }
    }
}
