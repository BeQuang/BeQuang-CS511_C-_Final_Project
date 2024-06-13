﻿namespace Booking
{
    partial class FormChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChat));
            this.panelHeading = new System.Windows.Forms.Panel();
            this.labelAdmin = new System.Windows.Forms.Label();
            this.panelAdmin = new System.Windows.Forms.Panel();
            this.panelKhungChat = new System.Windows.Forms.Panel();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.emoteButton = new System.Windows.Forms.Panel();
            this.panelSend = new System.Windows.Forms.Panel();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.emoteMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelHeading.SuspendLayout();
            this.panelMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeading
            // 
            this.panelHeading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(149)))));
            this.panelHeading.Controls.Add(this.labelAdmin);
            this.panelHeading.Controls.Add(this.panelAdmin);
            this.panelHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeading.Location = new System.Drawing.Point(0, 0);
            this.panelHeading.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeading.Name = "panelHeading";
            this.panelHeading.Size = new System.Drawing.Size(579, 89);
            this.panelHeading.TabIndex = 0;
            // 
            // labelAdmin
            // 
            this.labelAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAdmin.ForeColor = System.Drawing.Color.White;
            this.labelAdmin.Location = new System.Drawing.Point(151, 29);
            this.labelAdmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAdmin.Name = "labelAdmin";
            this.labelAdmin.Size = new System.Drawing.Size(312, 31);
            this.labelAdmin.TabIndex = 2;
            this.labelAdmin.Text = "ADMIN";
            // 
            // panelAdmin
            // 
            this.panelAdmin.BackColor = System.Drawing.Color.White;
            this.panelAdmin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelAdmin.BackgroundImage")));
            this.panelAdmin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelAdmin.Location = new System.Drawing.Point(18, 7);
            this.panelAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.panelAdmin.Name = "panelAdmin";
            this.panelAdmin.Size = new System.Drawing.Size(125, 74);
            this.panelAdmin.TabIndex = 1;
            // 
            // panelKhungChat
            // 
            this.panelKhungChat.AutoScroll = true;
            this.panelKhungChat.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKhungChat.Location = new System.Drawing.Point(0, 89);
            this.panelKhungChat.Margin = new System.Windows.Forms.Padding(4);
            this.panelKhungChat.Name = "panelKhungChat";
            this.panelKhungChat.Size = new System.Drawing.Size(579, 580);
            this.panelKhungChat.TabIndex = 1;
            // 
            // panelMessage
            // 
            this.panelMessage.BackColor = System.Drawing.Color.White;
            this.panelMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMessage.Controls.Add(this.emoteButton);
            this.panelMessage.Controls.Add(this.panelSend);
            this.panelMessage.Controls.Add(this.txtChat);
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMessage.Location = new System.Drawing.Point(0, 669);
            this.panelMessage.Margin = new System.Windows.Forms.Padding(4);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(579, 83);
            this.panelMessage.TabIndex = 2;
            // 
            // emoteButton
            // 
            this.emoteButton.BackColor = System.Drawing.Color.White;
            this.emoteButton.BackgroundImage = global::Booking.Properties.Resources.smile;
            this.emoteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.emoteButton.Location = new System.Drawing.Point(432, 15);
            this.emoteButton.Margin = new System.Windows.Forms.Padding(4);
            this.emoteButton.Name = "emoteButton";
            this.emoteButton.Size = new System.Drawing.Size(50, 50);
            this.emoteButton.TabIndex = 3;
            this.emoteButton.Click += new System.EventHandler(this.emoteButton_Click);
            // 
            // panelSend
            // 
            this.panelSend.BackgroundImage = global::Booking.Properties.Resources.tải_xuống__3_;
            this.panelSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSend.Location = new System.Drawing.Point(490, 15);
            this.panelSend.Margin = new System.Windows.Forms.Padding(4);
            this.panelSend.Name = "panelSend";
            this.panelSend.Size = new System.Drawing.Size(50, 50);
            this.panelSend.TabIndex = 1;
            this.panelSend.Click += new System.EventHandler(this.panelSend_Click);
            this.panelSend.MouseEnter += new System.EventHandler(this.panelSend_MouseEnter);
            this.panelSend.MouseLeave += new System.EventHandler(this.panelSend_MouseLeave);
            // 
            // txtChat
            // 
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChat.Location = new System.Drawing.Point(31, 10);
            this.txtChat.Margin = new System.Windows.Forms.Padding(4);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(393, 59);
            this.txtChat.TabIndex = 0;
            this.txtChat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtChat_KeyUp);
            // 
            // emoteMenu
            // 
            this.emoteMenu.DropShadowEnabled = false;
            this.emoteMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.emoteMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.emoteMenu.Name = "emotemenu";
            this.emoteMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.emoteMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormChat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 752);
            this.Controls.Add(this.panelMessage);
            this.Controls.Add(this.panelKhungChat);
            this.Controls.Add(this.panelHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChat";
            this.Load += new System.EventHandler(this.FormChat_Load);
            this.Shown += new System.EventHandler(this.FormChat_Shown);
            this.panelHeading.ResumeLayout(false);
            this.panelMessage.ResumeLayout(false);
            this.panelMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeading;
        private System.Windows.Forms.Panel panelKhungChat;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.Panel panelAdmin;
        private System.Windows.Forms.Label labelAdmin;
        private System.Windows.Forms.Panel panelSend;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.ContextMenuStrip emoteMenu;
        private System.Windows.Forms.Panel emoteButton;
        private System.Windows.Forms.Timer timer1;
    }
}