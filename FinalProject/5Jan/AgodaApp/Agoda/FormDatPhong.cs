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
using System.Windows.Forms.VisualStyles;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Booking
{
    public partial class FormDatPhong : KryptonForm
    {
        private string connectionString = GlobalVar.cn;
        public FormDatPhong()
        {
            InitializeComponent();
        }
        string tenks;
        int idp;
        DateTime di;
        DateTime ve;
        string nguoi;
        int soNgay;
        public FormDatPhong(string a, int b, DateTime x, DateTime y, string ng)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            tenks = a;
            idp = b;
            di = x;
            ve = y;
            nguoi = ng;
            labelTenHotel.Text = tenks;
            labelNgay.Text = di.ToString("dd/MM/yyyy") + " - " + ve.ToString("dd/MM/yyyy");
            TimeSpan khoangCach = ve - di;
            soNgay = (int)khoangCach.TotalDays + 1;
            labelSoNgay.Text = soNgay.ToString() + "đêm";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Nguoidung WHERE SoDienThoai = @id", connection);
                command.Parameters.AddWithValue("@id", GlobalVar.id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    kryptonTextBoxHoTen.Text = reader.GetString(3) + " " + reader.GetString(4);
                    kryptonTextBoxSDT.Text = reader.GetString(2);
                    kryptonTextBoxEmail.Text = reader.GetString(1);
                    kryptonTextBoxNhapLaiEmail.Text = reader.GetString(1);
                }
            }
        }


        private void button30_Click(object sender, EventArgs e)
        {
            button30.BackColor = Color.DarkOrchid;
            button20.BackColor = Color.Thistle;

            double TienKM;
            if (gia * 0.3 > 200000)
            {
                TienKM = -200000;
                labelTruKM.Text = string.Format("{0:#,##0}₫", TienKM);
            }
            else
            {
                TienKM = -gia * 0.3;
                labelTruKM.Text = string.Format("{0:#,##0}₫", TienKM);
            }

            labelTienGiaDaGiam.Text = string.Format("{0:#,##0}₫", (gia*soNgay + TienKM));
            labelTienGiaSauCung.Text = string.Format("{0:#,##0}₫", ((gia*soNgay + TienKM) * 1.08));
        }

        private void button20_Click(object sender, EventArgs e)
        {
            button20.BackColor = Color.DarkOrchid;
            button30.BackColor = Color.Thistle;

            double TienKM;
            if (gia * 0.2 > 300000)
            {
                TienKM = -300000;
                labelTruKM.Text = string.Format("{0:#,##0}₫", TienKM);
            }
            else
            {
                TienKM = -gia * 0.2;
                labelTruKM.Text = string.Format("{0:#,##0}₫", TienKM);
            }

            labelTienGiaDaGiam.Text = string.Format("{0:#,##0}₫", (gia*soNgay + TienKM));
            labelTienGiaSauCung.Text =string.Format("{0:#,##0}₫", ((gia*soNgay + TienKM) * 1.08));
        }


        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormListRoom sistema = new FormListRoom(tenks, di, ve, nguoi);
                sistema.ShowDialog();
                this.Close();
            }
        }

        private void kryptonButtonDatPhong_Click(object sender, EventArgs e)
        {
            Guid newID = Guid.NewGuid();
            string idgiaodich = newID.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "INSERT INTO LichSuGiaoDich (TenKS, TenPhong, SoTien, NgayDi, NgayVe, SoDienThoai, IDGiaoDich) VALUES (@tenks, @tenp, @sotien, @di, @den, @sdt, @idgiaodich)";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@tenks", tenks);
            command.Parameters.AddWithValue("@tenp", labelTenPhong.Text);
            command.Parameters.AddWithValue("@sotien", labelTienGiaSauCung.Text);
            command.Parameters.AddWithValue("@di", di);
            command.Parameters.AddWithValue("@den", ve);
            command.Parameters.AddWithValue("@sdt", GlobalVar.id);
            command.Parameters.AddWithValue("@idgiaodich", idgiaodich);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lưu thông tin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            int hah = slp - 1;
            conn = new SqlConnection(connectionString);
            query = "UPDATE Phong SET SoLuong = @sl WHERE IDPhong = @id";
            command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@sl", hah);
            command.Parameters.AddWithValue("@id", idp);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    {
                        this.Hide();
                        FormThanhCong sistema = new FormThanhCong(tenks, labelTenPhong.Text, di.ToString("dd/MM/yyyy"), ve.ToString("dd/MM/yyyy"), labelTienGiaSauCung.Text);
                        sistema.ShowDialog();
                        FormListRoom sis = new FormListRoom(tenks, di, ve, nguoi);
                        sis.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lưu thông tin");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        int slp;
        int gia;
        int idks;
        private void FormDatPhong_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ks.SoSao, p.TenPhong, p.LoaiPhong, p.Huong, p.DienTich, p.SoLuong, p.Gia, ks.IDKhachSan, ks.DiaDiem "
                               + "FROM KhachSan ks INNER JOIN Phong p ON ks.IDKhachSan = p.IDKhachSan "
                               + "WHERE ks.TenKS LIKE @Keyword AND p.IDPhong = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", tenks);
                command.Parameters.AddWithValue("@id", idp);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    labelSao.Text = new string('★', reader.GetInt32(0));
                    labelTenPhong.Text = reader.GetString(1);
                    labelThongTinPhong.Text = reader.GetString(2) + " - " + reader.GetString(3) + "-" + reader.GetString(4);
                    slp = reader.GetInt32(5);
                    gia = reader.GetInt32(6);
                    labelTienGiaGoc.Text = string.Format("{0:#,##0}₫", gia*soNgay);
                    labelTienGiaDaGiam.Text = string.Format("{0:#,##0}₫", gia*soNgay);
                    labelTienGiaSauCung.Text = string.Format("{0:#,##0}₫", (gia*soNgay * 1.08));
                    idks = reader.GetInt32(7);
                    labelDiaChi.Text = reader.GetString(8);
                }
            }
            string folderPathks = Path.Combine(Application.StartupPath, "KhachSan", $"{idks}\\{idks}.jpg");
            pictureBoxHotel.Image = Image.FromFile(folderPathks);
            string folderPathp = Path.Combine(Application.StartupPath, "KhachSan", $"{idks}\\{idp}");
            string[] imagePaths = Directory.GetFiles(folderPathp, "*.png");
            pictureBoxPhong.Image = Image.FromFile(imagePaths[0]);
        }
    }
}