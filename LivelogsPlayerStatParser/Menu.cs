using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            foreach (ListViewItem item in lstLogsTracked.Items)
            {
                if (item.Checked)
                    logsToOutput.Add(LogHandler.logsTracked[item.Index]);
            }
            if (chkAverageOutput.Checked)
                ExcelHandler.OutputExcel(false);
            else
                ExcelHandler.OutputExcel(true);
        }

        private void btnParseLog_Click(object sender, EventArgs e)
        {
            if (txtLogUri.Text != "")
            {
                if (txtLogWeighting.Text == "")
                    txtLogWeighting.Text = "0.5";
                bool result = LogHandler.ParseLog(txtLogUri.Text, Convert.ToDouble(txtLogWeighting.Text));
                if (result)
                {
                    fillListView();
                }
            }
        }

        private void fillListView()
        {
            lstLogsTracked.Items.Clear();
            foreach (Log log in LogHandler.logsTracked)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = log.URL.Replace(@"http://livelogs.ozfortress.com/view/", "");
                item.SubItems.Add(log.dateTime.ToString());
                item.SubItems.Add(log.Map);
                item.SubItems.Add(log.Weighting.ToString());
                lstLogsTracked.Items.Add(item);
            }
            lstLogsTracked.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            LogHandler.logsTracked.Clear();
            lstLogsTracked.Items.Clear();
        }

        private void btnRemoveLogs_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstLogsTracked.Items)
            {
                if (item.Checked)
                {
                    LogHandler.logsTracked.RemoveAt(item.Index);
                    item.Remove();
                }
            }
        }

        private void btnOpenHistory_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            dialog.Filter = "Config files | *.cfg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LogHandler.logsTracked.Clear();
                lstLogsTracked.Items.Clear();
                foreach (String line in System.IO.File.ReadAllLines(dialog.FileName))
                {
                    string[] temp = line.Split(' ');
                    LogHandler.ParseLog(temp[0], Convert.ToDouble(temp[1]));
                }
                fillListView();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Config files | *.cfg";
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String output = "";
                foreach (Log log in LogHandler.logsTracked)
                {
                    output += log.URL + " " + log.Weighting + "\r\n";
                }
                System.IO.File.WriteAllText(dialog.FileName, output);
            }
        }

        private void chkFullOutput_CheckedChanged(object sender, EventArgs e)
        {
            chkAverageOutput.Checked = !chkFullOutput.Checked;
        }

        private void chkAverageOutput_CheckedChanged(object sender, EventArgs e)
        {
            chkFullOutput.Checked = !chkAverageOutput.Checked;
        }
    }
}
