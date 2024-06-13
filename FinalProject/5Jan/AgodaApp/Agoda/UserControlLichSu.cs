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
    public partial class UserControlLichSu : UserControl
    {
        private string connectionString = GlobalVar.cn;
        public UserControlLichSu()
        {
            InitializeComponent();
        }
        public void LoadDataFromSql(string sdt)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM LichSuGiaoDich WHERE IDGiaoDich = @Keyword", connection);
                command.Parameters.AddWithValue("@Keyword", sdt);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    labelLSTenHotel.Text = reader.GetString(0);
                    labelLSTenPhong.Text = reader.GetString(1);
                    labelLSGiaTien.Text = reader.GetString(2);
                    labelNgayDi.Text = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    labelLSNgayDen.Text = reader.GetDateTime(4).ToString("dd/MM/yyyy");
                }
            } 
        }
    }
}
