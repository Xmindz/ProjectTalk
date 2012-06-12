using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChatClient.Engine;
namespace ChatClient
{
    public partial class MessageWindow : Form
    {
        string CURRENT_HISTORY = string.Empty;
        string IDENTIFIER;
        public MessageWindow()
        {
            InitializeComponent();
            this.Text = "Chat with " + this.Tag.ToString();
            InitiateHistoryBox();
        }

        public MessageWindow(object tag)
        {
            InitializeComponent();
            this.Tag =  tag;
            this.Text = "Chat with " + this.Tag.ToString();
            InitiateHistoryBox();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Trim().Length == 0)
                return;
            byte[] buffer = Encoding.ASCII.GetBytes(txtMessage.Text);
            //Statics.CurrentClient.GetStream().Write(buffer, 0, buffer.Length);

            Commons.Message msg = new Commons.Message();
            msg.Command = Commons.Statics.Commands.SENDMSG;
            msg.To = this.Tag.ToString();
            msg.Sender = Statics.CurrentUser;
            msg.Text = txtMessage.Text.Trim();
            Commons.Message.Send(msg, Statics.CurrentClient.GetStream());
            AppendToHistory(msg.Text, Statics.CurrentUser.UserName);
            txtMessage.Text = string.Empty;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Enter))
            {
                if (txtMessage.Text.Trim() != string.Empty)
                    btnSend_Click(null, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }

        void InitiateHistoryBox()
        {
            
            wbHistory.DocumentText =CURRENT_HISTORY= HTMLFormatter.DocumentText;
            wbHistory.Update();
        }

        public void AppendToHistory(string message, string user)
        {
            if (!wbHistory.IsBusy)
            {
                CURRENT_HISTORY = CURRENT_HISTORY.Insert(CURRENT_HISTORY.IndexOf("<span id='spnFooter'>"), HTMLFormatter.NewLine + HTMLFormatter.FormatUserName(user) + HTMLFormatter.NewLine + HTMLFormatter.FormatMessage(message));
                wbHistory.DocumentText = CURRENT_HISTORY;// wbHistory.DocumentText.Insert(wbHistory.DocumentText.IndexOf("<span id='spnFooter'>"), HTMLFormatter.NewLine + HTMLFormatter.FormatUserName(user) + HTMLFormatter.NewLine + HTMLFormatter.FormatMessage(message));
            }
            txtMessage.Focus();
        }

        public void RecievedNewMessage(string message, string user)
        {
            if (!this.Visible) this.Show();
            
            AppendToHistory(message, user);
            
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
        }

        private void MessageWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void MessageWindow_Load(object sender, EventArgs e)
        {
           
        }

        private void wbHistory_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (CURRENT_HISTORY != wbHistory.DocumentText)
                wbHistory.DocumentText = CURRENT_HISTORY;
            wbHistory.Document.Body.Document.GetElementById("spnFooter").ScrollIntoView(false);
        }
    }
}
