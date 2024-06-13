using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Booking
{
    public partial class FormListRoom : KryptonForm
    {
        public string ten;
        string userid;
        private string connectionString = GlobalVar.cn;
        public FormListRoom()
        {
            InitializeComponent();
        }
        public FormListRoom(string ten, DateTime di, DateTime den, string nguoi)
        {
            InitializeComponent();
            this.ten = ten;
            kryptonTextBox1.Text = ten;
            kryptonDateTimePicker2.Value = di;
            kryptonDateTimePicker1.Value = den;
            kryptonButton3.Text = nguoi;
            this.WindowState = FormWindowState.Maximized;
            labelTenUser.Text = GlobalVar.name;
            userid = GlobalVar.id;
        }
        private void load(SqlDataReader reader)
        {
            while (reader.Read())
            {
                idp = reader.GetInt32(1);
                UserControlThongTinPhong phong = new UserControlThongTinPhong(kryptonDateTimePicker2.Value);
                phong.LoadDataFromSql(idp);
                phong.ItemClicked += (s, e) =>
                {
                    this.Hide();
                    FormDatPhong formDatPhong = new FormDatPhong(ten, phong.GetINT(), kryptonDateTimePicker2.Value, kryptonDateTimePicker1.Value, kryptonButton3.Text);
                    formDatPhong.ShowDialog();
                    this.Close();
                };
                phong.Location = new Point(5, yOffset);
                panel2.Controls.Add(phong);
                yOffset += phong.Height + 7;
            }
        }
        int idks;
        int idp;
        int yOffset = 0;
        private void FormListRoom_Load(object sender, EventArgs e)
        {
            //Thông tin khách sạn
            UserControlFormInfor resultLabel = new UserControlFormInfor();
            idks = resultLabel.LoadDataFromSql(ten); 
            resultLabel.Location = new Point(5, yOffset);
            panel2.Controls.Add(resultLabel);
            yOffset += resultLabel.Height + 7;

            //Phòng
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Phong WHERE IDKhachSan = @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", idks);
                load(command.ExecuteReader());
            }
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
