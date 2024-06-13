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
using System.Web;
using System.Windows.Forms;

namespace Booking
{
    public partial class UserControlFormInfor : UserControl
    {
        private string connectionString = GlobalVar.cn;
        public UserControlFormInfor()
        {
            InitializeComponent();
        }

        int id;
        public int LoadDataFromSql(string ten)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM KhachSan WHERE TenKS LIKE @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", ten);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    id = reader.GetInt32(0);
                    labelTenChung.Text = ten;
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
                    labelVitri.Text = reader.GetString(7);
                }
            }

            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                connection1.Open();
                SqlCommand command1 = new SqlCommand("SELECT * FROM TienNghi WHERE IDKhachSan = @Keyword", connection1);
                command1.Parameters.AddWithValue("@Keyword", id);
                SqlDataReader reader1 = command1.ExecuteReader();

                if (reader1.Read())
                {
                    if (reader1.GetBoolean(1)) 
                    {
                        labelWF.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(2))
                    {
                        labelNH.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(3))
                    {
                        labelDX.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(4))
                    {
                        labelDVP.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(5))
                    {
                        labelHB.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(6))
                    {
                        labelDD.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(7))
                    {
                        labelV.ForeColor = Color.DodgerBlue;
                    }
                    if (reader1.GetBoolean(8))
                    {
                        labelPT.ForeColor = Color.DodgerBlue;
                    }    
                }
            }

            string folderPath = Path.Combine(Application.StartupPath, "KhachSan", $"{id}");

            List<string> imagePaths = new List<string>();
            Stack<string> foldersToCheck = new Stack<string>();
            foreach (string subDirectory in Directory.EnumerateDirectories(folderPath))
            {
                foldersToCheck.Push(subDirectory);
            }
            foldersToCheck.Push(folderPath);
            while (foldersToCheck.Count > 0)
            {
                string currentFolder = foldersToCheck.Pop();

                foreach (string filePath in Directory.EnumerateFiles(currentFolder, "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => s.EndsWith(".jpg") || s.EndsWith(".png") ))
                {
                    imagePaths.Add(filePath);
                }
            }

            // Gắn hình ảnh vào các PictureBox từ 1 đến 7
            for (int i = 0; i < Math.Min(imagePaths.Count, 7); i++)
            {
                PictureBox pictureBox = Controls.Find($"pictureBox{i + 1}", true)[0] as PictureBox;
                if (pictureBox != null)
                {
                    // Tải hình ảnh từ tệp và gắn vào PictureBox
                    pictureBox.Image = Image.FromFile(imagePaths[i]);
                }
            }
            return id;
        }

        private void pictureBox8_Click_1(object sender, EventArgs e)
        {
            FormMap map = new FormMap();
            map.ten = labelTenChung.Text;
            map.ShowDialog();
        }

        private void buttonLienHe_Click_1(object sender, EventArgs e)
        {
            FormChat formChat = new FormChat();
            formChat.idks = id.ToString();
            formChat.ShowDialog();
        }
    }
}
