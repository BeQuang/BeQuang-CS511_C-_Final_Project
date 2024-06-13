using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Booking
{
    public partial class FormInfor : KryptonForm
    {
        string vitri;
        string userid;
        private string connectionString = GlobalVar.cn;
        public FormInfor()
        {
            InitializeComponent();
        }
        public FormInfor(string vitri, DateTime di, DateTime den, string nguoi)
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            this.vitri = vitri;
            kryptonTextBox1.Text = vitri;
            kryptonDateTimePicker2.Value = di;
            kryptonDateTimePicker1.Value = den;
            kryptonButton3.Text = nguoi;
            labelTenUser.Text = GlobalVar.name;
            userid = GlobalVar.id;
            // Gắn sự kiện cho CheckBox
            checkBox1Star.CheckedChanged += (s, e) => LoadFilteredData();
            checkBox2Star.CheckedChanged += (s, e) => LoadFilteredData();
            checkBox3Star.CheckedChanged += (s, e) => LoadFilteredData();
            checkBox4Star.CheckedChanged += (s, e) => LoadFilteredData();
            checkBox5Star.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxWifi.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxDoxe.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxNhaHang.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxDichVuPhong.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxHoiBoi.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxDuaDon.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxVuong.CheckedChanged += (s, e) => LoadFilteredData();
            checkBoxPhongTap.CheckedChanged += (s, e) => LoadFilteredData();
        }
        private void load(SqlDataReader reader)
        {
            yOffset = 0;
            panel3.Controls.Clear();
            while (reader.Read())
            {
                UserControlPhong phong = new UserControlPhong();
                phong.LoadDataFromSql(reader.GetString(1));
                phong.Cursor = Cursors.Hand;
                phong.ItemClicked += (s, e) =>
                {
                    this.Hide();
                    FormListRoom formListRoom = new FormListRoom(phong.GetInfo(), kryptonDateTimePicker2.Value, kryptonDateTimePicker1.Value, kryptonButton3.Text);
                    formListRoom.ShowDialog();
                    this.Close();
                };
                phong.Location = new Point(5, yOffset);
                panel3.Controls.Add(phong);
                yOffset += phong.Height + 7;
            }

        }
        int yOffset = 0;
        private void FormInfor_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM KhachSan WHERE ViTri LIKE @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", vitri);
                load(command.ExecuteReader());
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            FormMap map = new FormMap();
            map.ten = vitri;
            map.ShowDialog();
        }
        private void LoadFilteredData()
        {
            bool[] starRatings = new bool[] { checkBox1Star.Checked, checkBox2Star.Checked, checkBox3Star.Checked, checkBox4Star.Checked, checkBox5Star.Checked };
            bool showWifi = checkBoxWifi.Checked;
            bool showDoxe = checkBoxDoxe.Checked;
            bool showNhaHang = checkBoxNhaHang.Checked;
            bool showDichVuPhong = checkBoxDichVuPhong.Checked;
            bool showHoiBoi = checkBoxHoiBoi.Checked;
            bool showDuaDon = checkBoxDuaDon.Checked;
            bool showVuong = checkBoxVuong.Checked;
            bool showPhongTap = checkBoxPhongTap.Checked;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Xây dựng câu truy vấn dựa trên bộ lọc
                string query = "SELECT ks.SoSao, ks.TenKS, tn.Wifi, tn.NhaHang, tn.DoXe, tn.DichVuPhong, tn.HoBoi, tn.DuaDon, tn.Vuon, tn.PhongTap " +
                               "FROM KhachSan ks " +
                               "INNER JOIN TienNghi tn ON ks.IDKhachSan = tn.IDKhachSan " +
                               "WHERE (@show1Star = 0 OR (@show1Star = 1 AND ks.SoSao = 1)) " +
                               "AND (@show2Star = 0 OR (@show2Star = 1 AND ks.SoSao = 2)) " +
                               "AND (@show3Star = 0 OR (@show3Star = 1 AND ks.SoSao = 3)) " +
                               "AND (@show4Star = 0 OR (@show4Star = 1 AND ks.SoSao = 4)) " +
                               "AND (@show5Star = 0 OR (@show5Star = 1 AND ks.SoSao = 5)) " +
                               "AND (@showWifi = 0 OR (@showWifi = 1 AND tn.Wifi = 1)) " +
                               "AND (@showDoxe = 0 OR (@showDoxe = 1 AND tn.DoXe = 1)) " +
                               "AND (@showNhaHang = 0 OR (@showNhaHang = 1 AND tn.NhaHang = 1)) " +
                               "AND (@showDichVuPhong = 0 OR (@showDichVuPhong = 1 AND tn.DichVuPhong = 1)) " +
                               "AND (@showHoiBoi = 0 OR (@showHoiBoi = 1 AND tn.HoBoi = 1)) " +
                               "AND (@showDuaDon = 0 OR (@showDuaDon = 1 AND tn.DuaDon = 1)) " +
                               "AND (@showVuong = 0 OR (@showVuong = 1 AND tn.Vuon = 1)) " +
                               "AND (@showPhongTap = 0 OR (@showPhongTap = 1 AND tn.PhongTap = 1))" +
                               "AND ViTri LIKE @Keyword";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@show1Star", starRatings[0] ? 1 : 0);
                    command.Parameters.AddWithValue("@show2Star", starRatings[1] ? 1 : 0);
                    command.Parameters.AddWithValue("@show3Star", starRatings[2] ? 1 : 0);
                    command.Parameters.AddWithValue("@show4Star", starRatings[3] ? 1 : 0);
                    command.Parameters.AddWithValue("@show5Star", starRatings[4] ? 1 : 0);
                    command.Parameters.AddWithValue("@showWifi", showWifi ? 1 : 0);
                    command.Parameters.AddWithValue("@showDoxe", showDoxe ? 1 : 0);
                    command.Parameters.AddWithValue("@showNhaHang", showNhaHang ? 1 : 0);
                    command.Parameters.AddWithValue("@showDichVuPhong", showDichVuPhong ? 1 : 0);
                    command.Parameters.AddWithValue("@showHoiBoi", showHoiBoi ? 1 : 0);
                    command.Parameters.AddWithValue("@showDuaDon", showDuaDon ? 1 : 0);
                    command.Parameters.AddWithValue("@showVuong", showVuong ? 1 : 0);
                    command.Parameters.AddWithValue("@showPhongTap", showPhongTap ? 1 : 0);
                    command.Parameters.AddWithValue("@Keyword", vitri);
                    load(command.ExecuteReader());
                }
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormHome sisteams = new FormHome();
                sisteams.ShowDialog();
                this.Close();
            }
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
