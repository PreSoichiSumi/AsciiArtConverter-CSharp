using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsciiArtConverter08.UserControl
{
    public class PanelDBuffer:Panel
    {
        public PanelDBuffer()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PanelDBuffer
            // 
            this.Name = "PanelDBuffer";
            this.ResumeLayout(false);

        }
    }
}
