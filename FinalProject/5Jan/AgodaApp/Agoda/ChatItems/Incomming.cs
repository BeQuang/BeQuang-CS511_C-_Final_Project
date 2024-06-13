using Booking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking.ChatItems
{
    public partial class Incomming : UserControl
    {
        public Incomming()
        {
            InitializeComponent();
        }

        public string Message
        {
            get
            {

                return labelTextChat.Text;
            }

            set
            {
                labelTextChat.Text = value;
            }
        }

        /// <summary>
        /// public Image Avatar { get => panelKhung.BackgroundImage; set => panelKhung.BackgroundImage = value; }
        /// </summary>

        void AdjustHeight()
        {
            panelUser2.Location = new Point(4, 3);
            labelTextChat.Height = Utils.GetTextHeight(labelTextChat) + 10;

            //panelKhung.Height = labelTextChat.Top + panelKhung.Top + labelTextChat.Height;
            //this.Height = panelKhung.Bottom + 10;
        }

        private void Incomming_Resize(object sender, EventArgs e)
        {
            AdjustHeight();
        }
    }
}