using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Booking
{
    public partial class UserControlPhong : UserControl
    {
        public event EventHandler ItemClicked;
        private string connectionString = GlobalVar.cn;
        public UserControlPhong()
        {
            InitializeComponent();
        }
        int id;
        public void LoadDataFromSql(string ten)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TOP 1 ks.IDKhachSan, ks.TenKS, ks.DiaDiem, ks.SoSao, ks.DanhGia, ks.SoLuongDanhGia, p.Gia "
                               + "FROM KhachSan ks INNER JOIN Phong p ON ks.IDKhachSan = p.IDKhachSan "
                               + "WHERE TenKS LIKE @Keyword "
                               + "ORDER BY p.Gia ASC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", ten);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id = reader.GetInt32(0); 
                    labelTenHotel.Text = ten;
                    labelDiaChi.Text = reader.GetString(2);
                    int sao = reader.GetInt32(3);
                    labelSao.Text = new string('★', sao);
                    double danhgia = reader.GetDouble(4);
                    labelDiem.Text = danhgia.ToString();
                    if (danhgia > 9)
                    {
                        labelDanhGia.Text = "Trên cả tuyệt vời";
                    }
                    else if (danhgia > 8)
                    {
                        labelDanhGia.Text = "Tuyệt vời";
                    }
                    else if (danhgia > 7) 
                    {
                        labelDanhGia.Text = "Rất tốt";
                    }
                    else if (danhgia > 6)
                    {
                        labelDanhGia.Text = "Hài lòng";
                    }
                    else
                    {
                        labelDanhGia.Text = "Điểm nhận xét";
                    }
                    labelSoLuongDanhGia.Text = reader.GetInt32(5).ToString() + " nhận xét";
                    labelGia.Text = string.Format("{0:#,##0}₫", reader.GetInt32(6));
                }
                string filePath = Path.Combine(Application.StartupPath, "KhachSan", $"{id}\\{id}.jpg");
                Image img = Image.FromFile(filePath);
                pictureBox1.Image = img;
            }
        }

        private void UserControlPhong_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
        public string GetInfo()
        {
            return labelTenHotel.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
