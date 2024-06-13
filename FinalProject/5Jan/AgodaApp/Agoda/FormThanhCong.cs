using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Drawing.Imaging;

namespace Booking
{
    public partial class FormThanhCong : KryptonForm
    {
        private BackgroundWorker emailSenderWorker;
        public FormThanhCong()
        {
            InitializeComponent();
            
        }
        string tenkhachsan;
        string tenp;
        public FormThanhCong(string tenks, string tenphong, string di, string den, string tien)
        {
            InitializeComponent();
            // Khởi tạo và cấu hình BackgroundWorker
            emailSenderWorker = new BackgroundWorker();
            emailSenderWorker.DoWork += EmailSenderWorker_DoWork;
            emailSenderWorker.RunWorkerCompleted += EmailSenderWorker_RunWorkerCompleted;
            tenkhachsan = tenks;
            tenp = tenphong;
            // Draw text on the image
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                // Set font and brush for drawing text
                Font font = new Font("Arial", 10);
                SolidBrush brush = new SolidBrush(Color.Black);

                // Set the position for drawing text
                PointF point = new PointF(60, 120);

                // Your booking information
                string bookingInfo = $"\t\t\tBooking Successful!\n\n\nName: {GlobalVar.name}\n\nHotel: {tenkhachsan}\n\nRoom: {tenphong}\n\nArrival Date: {di}\n\nDeparture Date: {den}\n\nPrice: {tien}";

                // Draw the text on the image
                g.DrawString(bookingInfo, font, brush, point);
            }
        }
        private Bitmap originalImage;

        private void button1_Click(object sender, EventArgs e)
        {
            originalImage = new Bitmap(pictureBox1.Image);

            // Tạo một SaveFileDialog để người dùng chọn vị trí để lưu
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files (*.png)|*.png|All files (*.*)|*.*";
            saveFileDialog.Title = "Chọn nơi lưu hình ảnh";

            // Mặc định tên file khi hiển thị dialog
            saveFileDialog.FileName = $"{GlobalVar.name}{tenkhachsan}{tenp}.png";

            // Nếu người dùng chọn vị trí và nhấn OK
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn được chọn bởi người dùng
                string outputPath = saveFileDialog.FileName;

                // Lưu hình ảnh với vị trí được chọn
                originalImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);

                // Hiển thị hình ảnh với vị trí được chọn
                pictureBox1.Image = originalImage;
                MessageBox.Show("Lưu thành công");
            }
        }
        private void EmailSenderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("lmhoang1008@gmail.com");
                mail.To.Add(GlobalVar.email);
                mail.Subject = "Thông báo: Mua vé thành công";
                mail.Body = $" ";
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Image image = pictureBox1.Image;
                    image.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0;

                    // Đính kèm ảnh từ mảng byte
                    Attachment imageAttachment = new Attachment(memoryStream, "image.png");
                    mail.Attachments.Add(imageAttachment);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential("lmhoang1008@gmail.com", "nfjt mvjj bife simi");
                    try
                    {
                        smtp.Send(mail);
                        e.Result = true;
                    }
                    catch (Exception ex)
                    {
                        e.Result = false;
                    }
                }
            }
        }
        private void EmailSenderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Ẩn PictureBox sau khi công việc đã hoàn thành (gửi email)
            pictureBox2.Visible = false;

            // Kiểm tra xem gửi email có thành công hay không
            if ((bool)e.Result)
            {
                MessageBox.Show("Email đã được gửi thành công!");
            }
            else
            {
                MessageBox.Show("Có lỗi khi gửi email.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Hiển thị PictureBox trước khi bắt đầu gửi email
            pictureBox2.Visible = true;

            // Bắt đầu BackgroundWorker để thực hiện gửi email
            emailSenderWorker.RunWorkerAsync();
            
        }
    }
}
