using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Booking
{
    public partial class FormDangKi : KryptonForm
    {
        private string connectionString = GlobalVar.cn;
        public FormDangKi()
        {
            InitializeComponent();
        }

        private void kryptonTextBoxTen_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxTen.Text == "Tên")
            {
                kryptonTextBoxTen.Text = "";
                kryptonTextBoxTen.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxTen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxTen.Text))
            {
                kryptonTextBoxTen.Text = "Tên";
                kryptonTextBoxTen.ForeColor = Color.Gray;
            }
        }

        private void kryptonTextBoxHo_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxHo.Text == "Họ")
            {
                kryptonTextBoxHo.Text = "";
                kryptonTextBoxHo.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxHo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxHo.Text))
            {
                kryptonTextBoxHo.Text = "Họ";
                kryptonTextBoxHo.ForeColor = Color.Gray;
            }
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

        private void kryptonTextBoxSDT_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxSDT.Text == "Số điện thoại")
            {
                kryptonTextBoxSDT.Text = "";
                kryptonTextBoxSDT.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxSDT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxSDT.Text))
            {
                kryptonTextBoxSDT.Text = "Số điện thoại";
                kryptonTextBoxSDT.ForeColor = Color.Gray;
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

        private void kryptonTextBoxNhapLaiMK_Enter(object sender, EventArgs e)
        {
            if (kryptonTextBoxNhapLaiMK.Text == "Nhập lại mật khẩu")
            {
                kryptonTextBoxNhapLaiMK.PasswordChar = '*';
                kryptonTextBoxNhapLaiMK.Text = "";
                kryptonTextBoxNhapLaiMK.ForeColor = Color.Black;
            }
        }

        private void kryptonTextBoxNhapLaiMK_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(kryptonTextBoxNhapLaiMK.Text))
            {
                kryptonTextBoxNhapLaiMK.PasswordChar = '\0';
                kryptonTextBoxNhapLaiMK.Text = "Nhập lại mật khẩu";
                kryptonTextBoxNhapLaiMK.ForeColor = Color.Gray;
            }
        }

        private void pictureBoxAnMK_Click(object sender, EventArgs e)
        {
            kryptonTextBoxMatKhau.PasswordChar = '*';
            pictureBoxAnMK.Visible = false;
            pictureBoxHienMK.Visible = true;
        }

        private void FormDangKi_Load(object sender, EventArgs e)
        {
            pictureBoxHienMK.Visible = true;
            pictureBoxAnMK.Visible = false;

            pictureBoxHienNhapLaiMK.Visible = true;
            pictureBoxAnNhapLaiMK.Visible = false;
        }

        private void pictureBoxHienMK_Click(object sender, EventArgs e)
        {
            kryptonTextBoxMatKhau.PasswordChar = '\0';
            pictureBoxHienMK.Visible = false;
            pictureBoxAnMK.Visible = true;
        }

        private void pictureBoxHienNhapLaiMK_Click(object sender, EventArgs e)
        {
            kryptonTextBoxNhapLaiMK.PasswordChar = '\0';
            pictureBoxHienNhapLaiMK.Visible = false;
            pictureBoxAnNhapLaiMK.Visible = true;
        }

        private void pictureBoxAnNhapLaiMK_Click(object sender, EventArgs e)
        {
            kryptonTextBoxNhapLaiMK.PasswordChar = '*';
            pictureBoxHienNhapLaiMK.Visible = false;
            pictureBoxHienNhapLaiMK.Visible = true;
        }

        private void kryptonButtonDangKi_Click(object sender, EventArgs e)
        {
            if (kryptonTextBoxTen.Text == null || kryptonTextBoxHo.Text == null || kryptonTextBoxEmail.Text == null || kryptonTextBoxSDT.Text == null || kryptonTextBoxMatKhau.Text == null || kryptonTextBoxNhapLaiMK.Text == null)
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin");
                
            }
            else if(kryptonTextBoxMatKhau.Text != kryptonTextBoxNhapLaiMK.Text)
            {
                MessageBox.Show("Nhập lại mật khẩu không đúng");
            }
            else
            {
                string email = kryptonTextBoxEmail.Text;
                string phone = kryptonTextBoxSDT.Text;
                string ten = kryptonTextBoxTen.Text;
                string password = kryptonTextBoxMatKhau.Text;
                string ho = kryptonTextBoxHo.Text;

                SqlConnection conn = new SqlConnection(connectionString);
                string query = "INSERT INTO NguoiDung (Email, SoDienThoai, MatKhau, Ho, Ten) VALUES (@Email, @SoDienThoai, @MatKhau, @Ho, @Ten)";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@SoDienThoai", phone);
                command.Parameters.AddWithValue("@MatKhau", password);
                command.Parameters.AddWithValue("@Ho", ho);
                command.Parameters.AddWithValue("@Ten", ten);

                try
                {
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đăng kí thành công");
                        ClearTextBoxes();
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
        }
        private void ClearTextBoxes()
        {
            kryptonTextBoxTen.Text = "";
            kryptonTextBoxHo.Text = "";
            kryptonTextBoxEmail.Text = "";
            kryptonTextBoxSDT.Text = "";
            kryptonTextBoxMatKhau.Text = "";
            kryptonTextBoxNhapLaiMK.Text = "";
        }
        private void labelBackDangNhap_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
