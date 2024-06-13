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
    public partial class FormMap : Form
    {
        public string ten;
        private string mapsUrl;
        public FormMap()
        {
            InitializeComponent();
        }

        private async void FormMap_Load(object sender, EventArgs e)
        {
            mapsUrl = $"https://www.google.com/maps/dir/?api=1&destination={ten}";

            // Hiển thị màn hình chờ
            ShowLoadingScreen();
            // Đăng ký sự kiện NavigationCompleted để ẩn màn hình chờ sau khi trang web được tải
            webView21.NavigationCompleted += webView21_NavigationCompleted;

            // Tải trang web Google Maps trong WebView2
            await webView21.EnsureCoreWebView2Async();

            webView21.Source = new Uri(mapsUrl);
        }
        private void ShowLoadingScreen()
        {
            pictureBox1.Visible = true;
        }

        private void HideLoadingScreen()
        {
            pictureBox1.Visible = false;
        }

        private void webView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            HideLoadingScreen();
        }
    }
}
