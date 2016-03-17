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
    public partial class FrmTone : System.Windows.Forms.Form
    {
        public FrmTone()
        {
            InitializeComponent();
        }

        public string[] ToneText
        {
            set
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("濃い←－－－－－－－－－－－－－－－－－－→薄い\r\n");
                sb.Append("\r\n");

                for (int i = 0; i < 5; i++)
                {
                    foreach (string t in value)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            sb.Append(t);
                        }
                    }
                    sb.Append("\r\n");
                }

                this.textBox1.Text = sb.ToString();

                this.textBox1.SelectionStart = 0;
                this.textBox1.SelectionLength = 0;
            }
        }

        private void FrmTone_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
