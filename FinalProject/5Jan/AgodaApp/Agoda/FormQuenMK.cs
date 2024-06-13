using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Data.SqlClient;

namespace Booking
{
    public partial class FormQuenMK : KryptonForm
    {
        private BackgroundWorker emailSenderWorker;
        private string connectionString = GlobalVar.cn;
        public FormQuenMK()
        {
            InitializeComponent();
            // Khởi tạo và cấu hình BackgroundWorker
            emailSenderWorker = new BackgroundWorker();
            emailSenderWorker.DoWork += EmailSenderWorker_DoWork;
            emailSenderWorker.RunWorkerCompleted += EmailSenderWorker_RunWorkerCompleted;
        }

        private void FormQuenMK_Load(object sender, EventArgs e)
        {
            panelXacNhanEmail.Visible = true;
            panelNhapMKMoi.Visible = false;

            pictureBoxAnMKMoi.Visible = false;
            pictureBoxAnNhapLaiMK.Visible = false;

            pictureBoxHienMKMoi.Visible = true;
            pictureBoxHienNhapLaiMK.Visible = true;
        }

        private void buttonTiepTheo_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string email = TextBoxNhapEmail.Text;
                string sql = "select *from NguoiDung where Email = '" + email + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read() == true)
                {
                    panelXacNhanEmail.Visible = false;
                    panelNhapMKMoi.Visible = true;
                }
                else
                {
                    MessageBox.Show("Không tồn tại");
                }
                data.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void pictureBoxHienMKMoi_Click(object sender, EventArgs e)
        {
            pictureBoxHienMKMoi.Visible = false;
            pictureBoxAnMKMoi.Visible = true;
            textBoxNhapMKMoi.PasswordChar = '\0';
        }

        private void pictureBoxAnMKMoi_Click(object sender, EventArgs e)
        {
            pictureBoxHienMKMoi.Visible = true;
            pictureBoxAnMKMoi.Visible = false;
            textBoxNhapMKMoi.PasswordChar = '*';
        }

        private void pictureBoxHienNhapLaiMK_Click(object sender, EventArgs e)
        {
            pictureBoxHienNhapLaiMK.Visible = false;
            pictureBoxAnNhapLaiMK.Visible = true;
            textBoxNhapLaiMK.PasswordChar = '\0';
        }

        private void pictureBoxAnNhapLaiMK_Click(object sender, EventArgs e)
        {
            pictureBoxHienNhapLaiMK.Visible = true;
            pictureBoxAnNhapLaiMK.Visible = false;
            textBoxNhapLaiMK.PasswordChar = '*';
        }

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SendNotificationEmail(string recipientEmail, string newPassword)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("21520853@gm.uit.edu.vn");
                mail.To.Add(recipientEmail);
                mail.Subject = "Thông báo: Mật khẩu đã được thay đổi";
                mail.Body = $"Mật khẩu của bạn đã được thay đổi thành: {newPassword}";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("21520853@gm.uit.edu.vn", "Hiu211203!!!!!");
                smtp.Send(mail);
            }
        }
        private void ButtonLuu_Click(object sender, EventArgs e)
        {
            // Hiển thị PictureBox trước khi bắt đầu gửi email
            pictureBox2.Visible = true;

            // Bắt đầu BackgroundWorker để thực hiện gửi email
            emailSenderWorker.RunWorkerAsync();
        }
        private void ClearTextBoxes()
        {
            TextBoxNhapEmail.Text = "";
            textBoxNhapMKMoi.Text = "";
            textBoxNhapLaiMK.Text = "";
            panelXacNhanEmail.Visible = true;
            panelNhapMKMoi.Visible = false;
        }
        private void EmailSenderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string password = textBoxNhapMKMoi.Text;
            string passworda = textBoxNhapLaiMK.Text;
            string email = TextBoxNhapEmail.Text;
            if (password == passworda)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                string query = "UPDATE NguoiDung SET MatKhau = @NewMatKhau WHERE Email = @Email";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@NewMatKhau", password);

                try
                {
                    conn.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        SendNotificationEmail(email, password);
                        e.Result = true;
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi gửi email.");
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            else
            {
                MessageBox.Show("Thông tin điền sai");
            }
        }

        private void EmailSenderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Ẩn PictureBox sau khi công việc đã hoàn thành (gửi email)
            pictureBox2.Visible = false;

            // Kiểm tra xem gửi email có thành công hay không
            if ((bool)e.Result)
            {
                ClearTextBoxes();
                MessageBox.Show("Email đã được gửi thành công!");
            }
            else
            {
                MessageBox.Show("Có lỗi khi gửi email.");
            }
        }
    }
}