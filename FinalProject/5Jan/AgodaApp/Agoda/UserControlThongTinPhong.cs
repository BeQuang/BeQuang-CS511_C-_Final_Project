using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking
{
    public partial class UserControlThongTinPhong : UserControl
    {
        public event EventHandler ItemClicked;
        private string connectionString = GlobalVar.cn;
        public UserControlThongTinPhong()
        {
            InitializeComponent();
        }
        public UserControlThongTinPhong(DateTime s)
        {
            InitializeComponent();
            labelTextHuyBo.Text = "Miễn phí hủy trước " + s.ToString("dd-MM-yyyy");
        }
        int idks;
        int idp;
        public void LoadDataFromSql(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Phong WHERE IDPhong = @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", id);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    idks = reader.GetInt32(0);
                    idp = id;
                    labelSoGiuong.Text = reader.GetString(2);
                    labelHuong.Text = reader.GetString(3);
                    labelDienTich.Text = reader.GetString(4);
                    if (reader.GetInt32(5) == 0)
                    {
                        labelCon.Text = "Hết phòng";
                        labelCon.ForeColor = Color.Red;
                        buttonDat.Enabled = false;
                    }
                    else
                    {
                        labelCon.Text = "Còn " + reader.GetInt32(5) + " phòng";
                    }
                    labelGia.Text = string.Format("{0:#,##0}₫", reader.GetInt32(6));
                    labelTenPhong.Text = reader.GetString(7);
                    labelSoNguoi.Text = "x"+reader.GetInt32(8);
                }
            }
            string folderPath = Path.Combine(Application.StartupPath, "KhachSan", $"{idks}\\{idp}");
            string[] imagePaths = Directory.GetFiles(folderPath, "*.png");
            // Gắn hình ảnh vào các PictureBox từ 1 đến 7
            for (int i = 0; i < Math.Min(imagePaths.Length, 3); i++)
            {
                PictureBox pictureBox = Controls.Find($"pictureBox{i + 1}", true)[0] as PictureBox;
                if (pictureBox != null)
                {
                    // Tải hình ảnh từ tệp và gắn vào PictureBox
                    pictureBox.Image = Image.FromFile(imagePaths[i]);
                }
            }
        }
        public int GetINT()
        {
            return idp;
        }
        private void buttonDat_Click(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonDat_Click_1(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
