using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsciiArtConverter08.Form.Dialog
{
    public partial class FrmChkAAInfo : System.Windows.Forms.Form
    {
        private bool isOK = false;

        private bool isConfigChange = false;

        int start1 = 0;
        int start2 = 0;

        public FrmChkAAInfo()
        {
            InitializeComponent();

            this.splitContainer1.SplitterDistance = this.splitContainer1.Width / 2;

            this.btnNew.Left = this.ClientSize.Width / 2 - this.btnNew.Width / 2;
        }

        public FrmChkAAInfo(string oldConf, string newConf):this()
        {
            string[] oc = oldConf.Replace("\r\n","\n").Split('\n');
            string[] nc = newConf.Replace("\r\n", "\n").Split('\n');

            this.richTextBox1.Text = oldConf;
            this.richTextBox2.Text = newConf;

            int index1 = 0;
            int index2 = 0;
            

            for (int i = 0; i < oc.Length; i++)
            {
                string s1 = oc[i];
                string s2 = nc[i];

                if (s1 != s2)
                {
                    this.richTextBox1.Select(index1, s1.Length);
                    this.richTextBox1.SelectionColor = Color.Red;
                    this.richTextBox2.Select(index2, s2.Length);
                    this.richTextBox2.SelectionColor = Color.Red;

                    if (this.start1 == 0)
                    {
                        this.start1 = index1;
                        this.start2 = index2;
                    }
                }

                index1+=s1.Length+1;
                index2+=s2.Length+1;
            }

            this.richTextBox1.SelectionStart = start1;
            this.richTextBox1.SelectionLength = 0;
            this.richTextBox2.SelectionStart = start2;
            this.richTextBox2.SelectionLength = 0;

            this.richTextBox1.ScrollToCaret();
            this.richTextBox2.ScrollToCaret();
        }

        public bool IsOK
        {
            get
            {
                return this.isOK;
            }
        }

        public bool IsConfigOpen
        {
            get
            {
                return this.isConfigChange;
            }
        }

        private void FrmChkAAInfo_Resize(object sender, EventArgs e)
        {
            this.btnNew.Left = this.ClientSize.Width / 2 - this.btnNew.Width / 2;
        }

        private void btnOld_Click(object sender, EventArgs e)
        {
            this.isConfigChange = true;

            this.isOK = true;

            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.isConfigChange = false;

            this.isOK = true;

            this.Close();
        }

        private void btnCansel_Click(object sender, EventArgs e)
        {
            this.isConfigChange = false;

            this.isOK = false;

            this.Close();
        }
    }
}
