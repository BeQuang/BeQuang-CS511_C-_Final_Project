using Booking;
using Booking.ChatItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Booking
{
    public partial class FormChat : Form
    {
        public string idks;
        public FormChat()
        {
            InitializeComponent();
        }

        private string connectionString = GlobalVar.cn;

        void Send()
        {
            if (txtChat.Text.Trim().Length == 0) return;
            AddOutGoing(txtChat.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                string query = "INSERT INTO TableChar VALUES (@Param1, @Param2, @Param3, @Param4)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Sử dụng SqlParameter để tránh SQL Injection và hỗ trợ Unicode
                    command.Parameters.AddWithValue("@Param1", idks);
                    command.Parameters.AddWithValue("@Param2", GlobalVar.id);
                    command.Parameters.AddWithValue("@Param3", txtChat.Text);
                    command.Parameters.AddWithValue("@Param4", currentTime);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        txtChat.Text = string.Empty;
                    }
                }
            }
        }

        void AddInComing(string message)
        {
            var bubble = new ChatItems.Incomming();
            panelKhungChat.Controls.Add(bubble);
            bubble.BringToFront();
            bubble.Dock = DockStyle.Top;
            bubble.Message = message;
        }

        void AddOutGoing(string message)
        {
            var bubble = new ChatItems.Outgoing();
            panelKhungChat.Controls.Add(bubble);
            bubble.BringToFront();
            bubble.Dock = DockStyle.Top;
            bubble.Message = message;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            panelKhungChat.Controls.Clear();
            loadmessage();
        }

        private void panelSend_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void txtChat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Send();
            }
        }

        private void FormChat_Shown(object sender, EventArgs e)
        {
            //AddInComing("Tôi có thể giúp gì ?");
        }


        private void panelSend_MouseEnter(object sender, EventArgs e)
        {
            panelSend.BackColor = Color.FromArgb(29, 207, 209);
        }

        private void panelSend_MouseLeave(object sender, EventArgs e)
        {
            panelSend.BackColor = Color.LightGoldenrodYellow;
        }

        private void loadmessage()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                string query = "select * from TableChar order by time ASC";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetString(1) == GlobalVar.id)
                    {
                        if (reader.GetString(0) == idks)
                        {
                            AddOutGoing(reader.GetString(2));
                        }

                    }
                    else if(reader.GetString(1) == idks)
                    {
                        if (reader.GetString(0) == GlobalVar.id)
                        {
                            AddInComing(reader.GetString(2));
                        }

                    }
                }
            }
            panelKhungChat.VerticalScroll.Value = panelKhungChat.VerticalScroll.Maximum;
        }
        private void FormChat_Load(object sender, EventArgs e)
        {
            labelAdmin.Text = "IDKhachSan: " + idks;
            labelAdmin.ForeColor = Color.White;
            loadmessage();
            timer1.Start();
        }
        private void PopulateEmoteMenu(List<string> emoteList)
        {
            emoteMenu.Items.Clear();

            foreach (string emoteText in emoteList)
            {
                ToolStripMenuItem emoteItem = new ToolStripMenuItem(emoteText);
                emoteItem.Click += EmoteItem_Click;

                emoteMenu.Items.Add(emoteItem);
            }
        }
        private void InsertEmoteIntoTextBox(string emoteName)
        {
            int currentPosition = txtChat.SelectionStart;

            txtChat.Text = txtChat.Text.Insert(currentPosition, emoteName);

            txtChat.SelectionStart = currentPosition + emoteName.Length;

            txtChat.Focus();
        }
        private void EmoteItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedEmoteItem = (ToolStripMenuItem)sender;
            string selectedEmoteText = selectedEmoteItem.Text;

            InsertEmoteIntoTextBox(selectedEmoteText);

            emoteMenu.Close();
        }

        private void ShowEmoteMenu()
        {
            List<string> emoteList = GetEmoteList();
            PopulateEmoteMenu(emoteList);

            emoteMenu.Show(emoteButton, new Point(0, emoteButton.Location.Y - emoteMenu.Height - emoteButton.Height));
        }

        private List<string> GetEmoteList()
        {
            List<string> emoteList = new List<string>
            {
                "😆","😂","🥹","🥲","🙂","😍","🧐","😎","😒","😞","😟","😫","😭","😢","😡","🤔","😴","🤤","😲"
            };

            return emoteList;
        }
        private void emoteButton_Click(object sender, EventArgs e)
        {
            ShowEmoteMenu();
        }
    }
}