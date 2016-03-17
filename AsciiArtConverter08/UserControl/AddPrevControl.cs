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
    public partial class AddPrevControl : System.Windows.Forms.UserControl
    {
        public AddPrevControl()
        {
            InitializeComponent();
        }

        private string filename;

        public void ReleaseImage()
        {
            if (panel1.BackgroundImage != null)
            {
                panel1.BackgroundImage.Dispose();
            }
        }

        public void SetImage(string filename)
        {
            if (this.filename == filename)
            {
                return;
            }

            long st = DateTime.Now.Ticks;

            this.filename = filename;

            Image oldImage = null;

            if (panel1.BackgroundImage != null)
            {
                oldImage = panel1.BackgroundImage;
            }

            if (File.Exists(filename))
            {
                using (Image bmp = Bitmap.FromFile(filename))
                {
                    panel1.BackgroundImage = new Bitmap(bmp);
                }

                if (oldImage != null)
                {
                    oldImage.Dispose();
                }
            }
            else
            {
                if (panel1.BackgroundImage != null)
                {
                    panel1.BackgroundImage.Dispose();
                }

                panel1.BackgroundImage = null;

            }

            //Debug.WriteLine(DateTime.Now.Ticks - st);

            this.panel1.Refresh();

        }
    }
}
