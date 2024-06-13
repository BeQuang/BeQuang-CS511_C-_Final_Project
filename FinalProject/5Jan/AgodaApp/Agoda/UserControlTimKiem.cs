using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking
{
    public partial class UserControlTimKiem : UserControl
    {
        public event EventHandler ItemClicked;
        public UserControlTimKiem()
        {
            InitializeComponent();
        }
        int y;
        public UserControlTimKiem(string m1, string m2, int x)
        {
            InitializeComponent();
            label1.Text = m1;
            label2.Text = m2;
            y = x;
            pictureBox1.Image = imageList1.Images[x];
        }

        private void UserControlTimKiem_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
        public string GetSearchText()
        {
            return label1.Text;
        }
        public int GetInfo() 
        {
            return y;
        }
    }
}
