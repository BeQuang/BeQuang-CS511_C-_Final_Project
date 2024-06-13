using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chatbot
{
    public partial class FormChat : Form
    {
        public string idks;
        public string idnd;
        public FormChat()
        {
            InitializeComponent();
        }

        SqlConnection sqlCond = null;
        string strCond = @"Data Source=LAPTOP-B8F38DK3\SQLEXPRESS;Initial Catalog=Agoda;Integrated Security=True";

        void Send()
        {
            if (txtChat.Text.Trim().Length == 0) return;
            AddOutGoing(txtChat.Text);

            using (SqlConnection sqlCond = new SqlConnection(strCond))
            {
                sqlCond.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;

                    // Sử dụng SqlParameter để tránh SQL Injection và hỗ trợ Unicode
                    cmd.CommandText = "INSERT INTO TableChar VALUES (@Param1, @Param2, @Param3, @Param4)";
                    cmd.Parameters.AddWithValue("@Param1", idnd);
                    cmd.Parameters.AddWithValue("@Param2", idks);
                    cmd.Parameters.AddWithValue("@Param3", txtChat.Text);
                    cmd.Parameters.AddWithValue("@Param4", currentTime);

                    cmd.Connection = sqlCond;
                    int rowsAffected = cmd.ExecuteNonQuery();

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
            using (SqlConnection sqlCond = new SqlConnection(strCond))
            {
                sqlCond.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from TableChar order by time ASC";
                    cmd.Connection = sqlCond;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.GetString(1) == idks)
                        {
                            if (reader.GetString(0) == idnd)
                            {
                                AddOutGoing(reader.GetString(2));
                            }

                        }
                        else if(reader.GetString(1) == idnd)
                        {
                            if (reader.GetString(0) == idks)
                            {
                                AddInComing(reader.GetString(2));
                            }

                        }
                    }
                    panelKhungChat.VerticalScroll.Value = panelKhungChat.VerticalScroll.Maximum;
                }
            }

            
        }
        private void FormChat_Load(object sender, EventArgs e)
        {
            labelAdmin.Text = idnd;
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
