using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static KeywordSearch.clsSearch;

namespace KeywordSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDirectory.Text = Application.StartupPath + "\\";
            txtKeyword.Focus();
            this.AcceptButton = btnSearch;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();

            if (string.IsNullOrEmpty(txtDirectory.Text.Trim()))
            {
                MessageBox.Show("Please enter a directory of text file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDirectory.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtKeyword.Text))
            {
                MessageBox.Show("Please enter a keyword to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKeyword.Focus();
                return;
            }

            List<sKeyword> lstKeyword = null;
            string strErrorMsg = string.Empty;
            
            if (SearchKeyword(txtDirectory.Text.Trim(), txtKeyword.Text, out lstKeyword, out strErrorMsg))
            {
                rtbOutput.AppendText("Keyword found as below: \n\n");

                for (int i = 0; i < lstKeyword.Count; i++)
                {
                    rtbOutput.AppendText("No." + (i + 1).ToString() + ":\n");
                    rtbOutput.AppendText("File = " + lstKeyword[i].strFileName + "\n");
                    rtbOutput.AppendText("Line = " + lstKeyword[i].LineNumber.ToString() + "\n");
                    rtbOutput.AppendText("Value = " + lstKeyword[i].strLineText + "\n");
                    rtbOutput.AppendText("\n");
                }
            }
            else
                rtbOutput.AppendText("Info: \n\n" + strErrorMsg);

            lstKeyword.Clear();
            txtKeyword.Focus();
        }
    }
}
