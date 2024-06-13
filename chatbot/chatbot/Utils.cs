using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Housework2
{
    public static class Utils
    {
        public static int GetTextHeight(Label lbl)
        {
            using (Graphics q = lbl.CreateGraphics())
            {
                SizeF size = q.MeasureString(lbl.Text, lbl.Font, 495);
                return (int)Math.Ceiling(size.Height);
            }
        }
    }
}
