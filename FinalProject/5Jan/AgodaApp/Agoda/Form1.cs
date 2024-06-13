using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Booking
{
    public partial class FormHome : KryptonForm
    {
        private string connectionString = GlobalVar.cn;
        string userid;
        public FormHome()
        {
            InitializeComponent();
            kryptonGroupSoNguoiPhong.Visible = false;
            SetNoActiveControl(); // Gọi hàm để đặt control không được chọn
            this.WindowState = FormWindowState.Maximized;
            labelTenUser.Text = GlobalVar.name;
            userid = GlobalVar.id;
        }

        private void SetNoActiveControl()
        {
            // Đặt ActiveControl về null để không có control nào được chọn ban đầu
            this.ActiveControl = null;
        }

        private void kryptonButtonTim_Click(object sender, EventArgs e)
        {
            if (kryptonTextBoxSearch.Text == string.Empty || kryptonTextBoxSearch.Text == "Nhập địa điểm du lịch hoặc tên khách sạn")
            {
                MessageBox.Show("Vui lòng chọn địa điểm");
            } 
            else if (where == 0)
            {
                this.Hide();
                FormInfor formInfor = new FormInfor(kryptonTextBoxSearch.Text, kryptonDateTimePickerDi.Value,kryptonDateTimePickerDen.Value,kryptonButtonSoNguoiPhong.Text);
                formInfor.ShowDialog();
                this.Close();
            }
            else if (where == 1)
            {
                this.Hide();
                FormListRoom formListRoom = new FormListRoom(kryptonTextBoxSearch.Text, kryptonDateTimePickerDi.Value, kryptonDateTimePickerDen.Value, kryptonButtonSoNguoiPhong.Text);
                formListRoom.ShowDialog();
                this.Close();
            }
        }

        private bool groupBoxVisible = false;


        private void kryptonButtonSoNguoiPhong_Click(object sender, EventArgs e)
        {
            // Nếu GroupBox đang ẩn, hiển thị nó; ngược lại, ẩn nó.
            kryptonGroupSoNguoiPhong.Visible = !groupBoxVisible;

            // Cập nhật trạng thái của biến cờ
            groupBoxVisible = !groupBoxVisible;

            UpdateLabelSoNguoiPhong();
        }

        private void kryptonTextBoxSearch_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxSearch.Text == "Nhập địa điểm du lịch hoặc tên khách sạn")
            {
                kryptonTextBoxSearch.Text = "";
                kryptonTextBoxSearch.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxSearch.Text))
            {
                kryptonTextBoxSearch.Text = "Nhập địa điểm du lịch hoặc tên khách sạn";
                kryptonTextBoxSearch.ForeColor = Color.Gray;
            }
        }

        private int counterSoPhong = 1;
        private void pictureBoxCongPhong_Click(object sender, EventArgs e)
        {
            // Tăng giá trị của biến đếm khi được nhấn
            counterSoPhong++;
            UpdateLabelSoPhong();
        }

        private void pictureBoxTruPhong_Click(object sender, EventArgs e)
        {
            // Giảm giá trị của biến đếm khi  được nhấn
            counterSoPhong--;
            UpdateLabelSoPhong();
        }

        private void UpdateLabelSoPhong()
        {
            // Hiển thị giá trị của biến đếm trong Label
            labelSoPhong.Text = counterSoPhong.ToString();
        }

        private int counterSoNguoi = 2;
        private void pictureBoxTruNguoi_Click(object sender, EventArgs e)
        {
            counterSoNguoi--;
            UpdateLabelSoNguoi();

        }

        private void UpdateLabelSoNguoi()
        {
            // Hiển thị giá trị của biến đếm trong Label
            labelSoNguoi.Text = counterSoNguoi.ToString();
        }

        private void pictureBoxCongNguoi_Click(object sender, EventArgs e)
        {
            counterSoNguoi++;
            UpdateLabelSoNguoi();
        }

        private void UpdateLabelSoNguoiPhong()
        {
            // Số người và số phòng từ các Label2 và Label3
            string soNguoi = labelSoNguoi.Text;
            string soPhong = labelSoPhong.Text;

            // Định dạng lại chuỗi cho Label1
            kryptonButtonSoNguoiPhong.Text = $"{soNguoi} người, {soPhong} phòng";
        }

        
        private void SearchData(string keyword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tìm kiếm dữ liệu trong SQL theo từ khóa
                string query = "SELECT MAX(TenKS) AS TenKS,ViTri FROM KhachSan WHERE TenKS LIKE @Keyword OR ViTri LIKE @Keyword GROUP BY ViTri";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    // Hiển thị kết quả tìm kiếm trong Panel
                    DisplayResults(command.ExecuteReader(), keyword);
                }
            }
        }
        int where = 10;
        private void DisplayResults(SqlDataReader reader, string keyword)
        {
            // Xóa các controls hiện tại trong Panel
            kryptonGroup1.Panel.Controls.Clear();
            int yOffset = 0;
            // Duyệt qua các kết quả và hiển thị thông tin
            while (reader.Read())
            {
                string viTri = reader["ViTri"].ToString();
                string tenKS = reader["TenKS"].ToString();
                string keywordCleaned = Regex.Replace(keyword, @"\p{M}", "");
                string tenKSCleaned = Regex.Replace(tenKS, @"\p{M}", "");
                if (tenKSCleaned.IndexOf(keywordCleaned, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    UserControlTimKiem resultLabel = new UserControlTimKiem(tenKS, "Khách sạn", 1);
                    resultLabel.AutoSize = true;
                    resultLabel.Cursor = Cursors.Hand;
                    resultLabel.ItemClicked += (s, e) =>
                    {
                        // Lấy nội dung của Label1 từ UserControl và đặt vào TextBoxSearch
                        kryptonTextBoxSearch.Text = resultLabel.GetSearchText();
                        where = resultLabel.GetInfo();
                        kryptonGroup1.Visible = false;
                    };
                    resultLabel.Location = new Point(5, yOffset);
                    kryptonGroup1.Panel.Controls.Add(resultLabel);
                    yOffset += resultLabel.Height + 7; // Cập nhật vị trí dọc cho các kết quả
                }
                else
                {
                    UserControlTimKiem resultLabel = new UserControlTimKiem(viTri, "Vị trí", 0);
                    resultLabel.AutoSize = true;
                    resultLabel.Cursor = Cursors.Hand;
                    resultLabel.ItemClicked += (s, e) =>
                    {
                        // Lấy nội dung của Label1 từ UserControl và đặt vào TextBoxSearch
                        kryptonTextBoxSearch.Text = resultLabel.GetSearchText();
                        where = resultLabel.GetInfo();
                        kryptonGroup1.Visible = false;
                    };
                    resultLabel.Location = new Point(5, yOffset);
                    kryptonGroup1.Panel.Controls.Add(resultLabel);
                    yOffset += resultLabel.Height + 7; // Cập nhật vị trí dọc cho các kết quả
                }
            }

            // Hiển thị Panel chỉ khi có kết quả
            kryptonGroup1.Visible = kryptonGroup1.Panel.Controls.Count > 0;
        }
        private void kryptonTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(kryptonTextBoxSearch.Text);
            if (kryptonTextBoxSearch.Text == string.Empty)
            {
                kryptonGroup1.Visible = false;
            }
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            kryptonDateTimePickerDi.Value = DateTime.Now;
            kryptonDateTimePickerDen.Value = DateTime.Today.AddDays(1);
        }

        private void buttonDangXuat_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormDangNhap sisteams = new FormDangNhap();
                sisteams.ShowDialog();
                this.Close();
            };
        }

        private void pictureBoxTK_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormThongTin sisteams = new FormThongTin(userid);
                sisteams.ShowDialog();
                this.Show();
            };
        }
    }
}
