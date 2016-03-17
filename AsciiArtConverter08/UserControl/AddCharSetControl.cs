using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AsciiArtConverter08.UserControl
{
    public partial class AddCharSetControl : System.Windows.Forms.UserControl
    {
        public AddCharSetControl()
        {
            InitializeComponent();

            this.comboBox1.SelectedIndex = 0;
        }

        public string CharSet
        {
            get
            {
                return this.comboBox1.Text;
            }
        }
    }
}
