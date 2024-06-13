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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Booking
{
    public partial class FormThongTin : KryptonForm
    {
        private string connectionString = GlobalVar.cn;
        public FormThongTin()
        {
            InitializeComponent();
        }
        public FormThongTin(string id)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Nguoidung WHERE SoDienThoai = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    labelTextHoVaTen.Text = reader.GetString(3) + " " + reader.GetString(4);
                    labelTextSDT.Text = reader.GetString(2);
                    labelTextEmail.Text = reader.GetString(1);
                    textBoxMK.Text = reader.GetString(0);
                }
            }
        }
        private void labelChinhSuaHoVaTen_Click(object sender, EventArgs e)
        {
            if (panelSuaHoTen.Visible == false)
            {
                panelSuaHoTen.Visible = true;
            }
            else
            {
                panelSuaHoTen.Visible &= false;
            }

        }
        int yOffset = 0;
        private void load(SqlDataReader reader)
        {
            try
            {
                while (reader.Read())
                {
                    UserControlLichSu phong = new UserControlLichSu();
                    phong.LoadDataFromSql(reader.GetString(6));
                    phong.Location = new Point(5, yOffset);
                    panel2.Controls.Add(phong);
                    yOffset += phong.Height + 7;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void FormThongTin_Load(object sender, EventArgs e)
        {
            panelSuaHoTen.Visible = false;
            panelSuaEmail.Visible = false;
            pictureBoxAnMK.Visible = false;
            pictureBoxHienMK.Visible = true;
            panelSuaMK.Visible = false;
            pictureBoxAnMKMoi.Visible = false;
            pictureBoxAnXacNhanMK.Visible = false;
            pictureBoxHienMKMoi.Visible = true;
            pictureBoxHienXacNhanMK.Visible = true;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM LichSuGiaoDich WHERE SoDienThoai = @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", labelTextSDT.Text);
                load(command.ExecuteReader());
            }
        }

        private void buttonSaveHoTen_Click(object sender, EventArgs e)
        {
            panelSuaHoTen.Visible = false;
            labelTextHoVaTen.Text = textBoxSuaTen.Text + " " + textBoxSuaHo.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE NguoiDung SET Ho = @ho, Ten = @ten WHERE SoDienThoai = @id";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@ho", textBoxSuaHo.Text);
            command.Parameters.AddWithValue("@ten", textBoxSuaTen.Text);
            command.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thành công");
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

        private void labelChinhSuaEmail_Click(object sender, EventArgs e)
        {
            if (panelSuaEmail.Visible == false)
            {
                panelSuaEmail.Visible = true;
            }
            else
            {
                panelSuaEmail.Visible &= false;
            }
        }

        private void buttonSaveEmail_Click(object sender, EventArgs e)
        {
            panelSuaEmail.Visible &= false;
            labelTextEmail.Text = textBoxSuaEmail.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE NguoiDung SET Email = @Email WHERE SoDienThoai = @id";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Email", textBoxSuaEmail.Text);
            command.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thành công");
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

        private void pictureBoxHienMK_Click(object sender, EventArgs e)
        {
            pictureBoxHienMK.Visible = false;
            pictureBoxAnMK.Visible = true;
            textBoxMK.PasswordChar = '\0';
        }

        private void pictureBoxAnMK_Click(object sender, EventArgs e)
        {
            pictureBoxAnMK.Visible = false;
            pictureBoxHienMK.Visible = true;
            textBoxMK.PasswordChar = '*';
        }

        private void labelChinhSuaMK_Click(object sender, EventArgs e)
        {
            if (panelSuaMK.Visible == false)
            {
                panelSuaMK.Visible = true;
            }
            else
            {
                panelSuaMK.Visible = false;
            }

        }

        private void pictureBoxHienMKMoi_Click(object sender, EventArgs e)
        {
            pictureBoxHienMKMoi.Visible = false;
            pictureBoxAnMKMoi.Visible = true;
            textBoxMKMoi.PasswordChar = '\0';
        }

        private void pictureBoxAnMKMoi_Click(object sender, EventArgs e)
        {
            pictureBoxHienMKMoi.Visible = true;
            pictureBoxAnMKMoi.Visible = false;
            textBoxMKMoi.PasswordChar = '*';
        }

        private void pictureBoxHienXacNhanMK_Click(object sender, EventArgs e)
        {
            pictureBoxHienXacNhanMK.Visible = false;
            pictureBoxAnXacNhanMK.Visible = true;
            textBoxXacNhanMK.PasswordChar = '\0';
        }

        private void pictureBoxAnXacNhanMK_Click(object sender, EventArgs e)
        {
            pictureBoxHienXacNhanMK.Visible = true;
            pictureBoxAnXacNhanMK.Visible = false;
            textBoxXacNhanMK.PasswordChar = '\0';
        }

        private void buttonSaveMK_Click_1(object sender, EventArgs e)
        {
            textBoxMK.Text = textBoxMKMoi.Text;
            panelSuaMK.Visible = false;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE NguoiDung SET MatKhau = @mk WHERE SoDienThoai = @id";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@mk", textBoxMKMoi.Text);
            command.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thành công");
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

        private void pictureBoxBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelSuaEmail.Visible &= false;
            labelTextEmail.Text = textBoxSuaEmail.Text;

            SqlConnection conn = new SqlConnection(connectionString);
            string query = "UPDATE NguoiDung SET SoDienThoai = @sdt WHERE SoDienThoai = @id";

            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@sdt", textBox1.Text);
            command.Parameters.AddWithValue("@id", GlobalVar.id);
            try
            {
                conn.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Sửa thành công");
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

        private void label1_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }
    } 
}