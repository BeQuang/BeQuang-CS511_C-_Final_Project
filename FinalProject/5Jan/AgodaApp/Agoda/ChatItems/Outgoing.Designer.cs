namespace Booking.ChatItems
{
    partial class Outgoing
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTextChat = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTextChat
            // 
            this.labelTextChat.AutoSize = true;
            this.labelTextChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelTextChat.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelTextChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(207)))), ((int)(((byte)(209)))));
            this.labelTextChat.Location = new System.Drawing.Point(166, 19);
            this.labelTextChat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextChat.MaximumSize = new System.Drawing.Size(400, 12308);
            this.labelTextChat.MinimumSize = new System.Drawing.Size(400, 49);
            this.labelTextChat.Name = "labelTextChat";
            this.labelTextChat.Size = new System.Drawing.Size(400, 49);
            this.labelTextChat.TabIndex = 0;
            // 
            // Outgoing
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelTextChat);
            this.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Outgoing";
            this.Size = new System.Drawing.Size(570, 95);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTextChat;
    }
}