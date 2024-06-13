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
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Booking
{
    public partial class FormDangNhap : KryptonForm
    {
        private string connectionString = GlobalVar.cn;
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void kryptonTextBoxEmail_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxEmail.Text == "Email")
            {
                kryptonTextBoxEmail.Text = "";
                kryptonTextBoxEmail.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxEmail.Text))
            {
                kryptonTextBoxEmail.Text = "Email";
                kryptonTextBoxEmail.ForeColor = Color.Gray;
            }
        }

        private void kryptonTextBoxMatKhau_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxMatKhau.Text == "Mật khẩu")
            {
                kryptonTextBoxMatKhau.PasswordChar = '*';
                kryptonTextBoxMatKhau.Text = "";
                kryptonTextBoxMatKhau.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxMatKhau_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxMatKhau.Text))
            {
                kryptonTextBoxMatKhau.PasswordChar = '\0';
                kryptonTextBoxMatKhau.Text = "Mật khẩu";
                kryptonTextBoxMatKhau.ForeColor = Color.Gray;
            }
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            kryptonTextBoxMatKhau.Text = "Mật khẩu";
            kryptonTextBoxMatKhau.ForeColor = Color.Gray;

            kryptonTextBoxEmail.Text = "Email";
            kryptonTextBoxEmail.ForeColor = Color.Gray;

            pictureBoxHien.Visible = true;
            pictureBoxAn.Visible = false;
        }

        private void pictureBoxHien_Click(object sender, EventArgs e)
        {
            kryptonTextBoxMatKhau.PasswordChar = '\0';
            pictureBoxHien.Visible = false;
            pictureBoxAn.Visible = true;
        }

        private void pictureBoxAn_Click(object sender, EventArgs e)
        {
            kryptonTextBoxMatKhau.PasswordChar = '*';
            pictureBoxAn.Visible = false;
            pictureBoxHien.Visible = true;
        }

        private void labelTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormDangKi sistema = new FormDangKi();
                sistema.ShowDialog();
                this.Show();
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Nguoidung WHERE Email = @tk AND MatKhau = @mk", connection);
                command.Parameters.AddWithValue("@tk", kryptonTextBoxEmail.Text);
                command.Parameters.AddWithValue("@mk", kryptonTextBoxMatKhau.Text);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GlobalVar.id = reader.GetString(2);
                    GlobalVar.email = reader.GetString(1);
                    GlobalVar.name = reader.GetString(3) +" "+ reader.GetString(4);
                    {
                        this.Hide();
                        FormHome sistema = new FormHome();
                        sistema.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không hợp lệ");
                }
            }
        }


        private void labelQuenMK_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                FormQuenMK sistema = new FormQuenMK();
                sistema.ShowDialog();
                this.Show();
            }
        }
    }
}
