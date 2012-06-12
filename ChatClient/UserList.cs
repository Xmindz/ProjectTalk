using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.DirectoryServices;
using ChatClient.Engine;
using System.Net.Sockets;
using System.Threading;

namespace ChatClient
{
    public partial class UserListWindow : Form
    {
        public UserListWindow()
        {
            InitializeComponent();
        }

        private void UserList_Load(object sender, EventArgs e)
        {
            Statics.CurrentClient = new TcpClient();
            Statics.CurrentClient.Connect("node7", 1269);
            if (Statics.CurrentClient.Connected)
            {
                Commons.Message msg = new Commons.Message();
                msg.Command = Commons.Statics.Commands.ADD;
                Commons.UserInfo user = new Commons.UserInfo();
                user.UserName = Environment.UserName;
                user.Identifier = Environment.UserName;
                lblUser.Text = Environment.UserName;
                msg.Sender = user;
                Statics.CurrentUser = user;
                Commons.Message.Send(msg, Statics.CurrentClient.GetStream());

                this.Text = "ProjectTalk™ - " + Statics.CurrentUser.UserName;

                ThreadStart tsReadMessage;
                Thread thReadMessage;
                tsReadMessage = delegate { ReadMessage(); };
                thReadMessage = new Thread(tsReadMessage);
                thReadMessage.SetApartmentState(ApartmentState.STA);
                thReadMessage.Start();

                refreshTimer.Start();
            }
        }

        void refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        void RefreshList()
        {
            if (lvUserList.Items.Count != Statics.CurrentUsers.Count)
            {
                lvUserList.Items.Clear();
                foreach (Commons.UserInfo user in Statics.CurrentUsers)
                {
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = user.UserName;
                    listItem.Tag = user.UserName;

                    lvUserList.Items.Add(listItem);
                }
            }
        }

        void ReadMessage()
        {
            TcpClient tcpClient = Statics.CurrentClient;
            while (true)
            {
                byte[] buffer = new byte[255];
                NetworkStream ns = tcpClient.GetStream();
                Commons.Message msg = Commons.Message.Deserialize(ns);
                ProcessMessage(msg, tcpClient.GetStream());
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            RefreshList();
            if (Statics.MessageQueue.Count > 0)
            {
                if (Statics.MessageWindows.Count > 0)
                {
                    List<string> tags = (from MessageWindow mw in Statics.MessageWindows select mw.Tag.ToString()).ToList<string>();
                    foreach (Commons.Message msg in Statics.MessageQueue)
                    {
                        if (!tags.Contains(msg.Sender.UserName))
                        {
                            MessageWindow msgWindow = new MessageWindow(msg.Sender.UserName);
                            msgWindow.Show();
                            Statics.MessageWindows.Add(msgWindow);
                        }
                    }
                }
            }
        }

        private void lvUserList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvUserList_ItemActivate(object sender, EventArgs e)
        {
            bool windowOpened = false;
            string identifier = lvUserList.SelectedItems[0].Tag.ToString();
            foreach (MessageWindow mw in Statics.MessageWindows)
            {
                if (mw.Tag.ToString().Equals(identifier))
                {
                    // A hidden window.
                    ShowWindow(mw);
                    windowOpened = true;
                }
            }
            if (!windowOpened)
            {
                MessageWindow msgWindow = new MessageWindow(identifier);
                ShowWindow(msgWindow);
                Statics.MessageWindows.Add(msgWindow);
            }
        }

        delegate void ShowWindowCallback(MessageWindow window);
        private void ShowWindow(MessageWindow window)
        {
            if (window.InvokeRequired)
            {
                window.Invoke(new ShowWindowCallback(ShowWindow), window);
            }
            else
            {
                window.Show();
            }
        }

        delegate void HideWindowCallback(MessageWindow window);
        private void HideWindow(MessageWindow window)
        {
            if (this.InvokeRequired)
            {
                window.Invoke(new HideWindowCallback(HideWindow), window);
            }
            else
            {
                window.Hide();
            }
        }

        private void UserListWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

            //ClientProcessor.RegisterLogout();
            Application.ExitThread();

        }

        void ProcessMessage(Commons.Message msg, NetworkStream ns)
        {
            Commons.Message response = new Commons.Message();


            switch (msg.Command)
            {
                case Commons.Statics.Commands.LISTUSERS:
                    // List all the users.
                    Statics.CurrentUsers = Commons.UserInfo.Deserialize(msg.Text);
                    break;
                case Commons.Statics.Commands.SENDMSG:
                    //Statics.MessageQueue.Add(msg);
                    if (Statics.MessageWindows.Count > 0)
                    {
                        List<string> tags = (from MessageWindow mw in Statics.MessageWindows select mw.Tag.ToString()).ToList<string>();
                        if (!tags.Contains(msg.Sender.UserName))
                        {
                            MessageWindow msgWindow = new MessageWindow(msg.Sender.UserName);
                            msgWindow.Show();
                            Statics.MessageWindows.Add(msgWindow);
                        }
                        else
                        {
                            MessageWindow msw = (MessageWindow)(from MessageWindow mw in Statics.MessageWindows where mw.Tag.ToString().Equals(msg.Sender.UserName) select mw).ToList<MessageWindow>().First<MessageWindow>();
                            SetMessage(msw, msg);
                        }
                    }
                    else
                    {
                        OpenNewWindow(msg);
                    }
                    break;
                case Commons.Statics.Commands.USERONLINE:
                    if (!Statics.CurrentUsers.Contains(msg.Sender))
                        Statics.CurrentUsers.Add(msg.Sender);
                    break;
                case Commons.Statics.Commands.USEROFFLINE:
                    if (Statics.CurrentUsers.Contains(msg.Sender))
                        Statics.CurrentUsers.Remove(msg.Sender);
                    break;
            }
        }

        public void OpenNewWindow(Commons.Message msg)
        {
            MessageWindow msgWindow = new MessageWindow(msg.Sender.UserName);
            Statics.MessageWindows.Add(msgWindow);
            SetMessage(msgWindow, msg);
            System.Windows.Forms.Application.Run(msgWindow);
        }

        public void RegisterLogout()
        {
            Commons.Message msg = new Commons.Message();
            msg.Command = Commons.Statics.Commands.USEROFFLINE;
            msg.Sender = Statics.CurrentUser;
        }

        delegate void SetMessageCallback(string msg, string user);
        public void SetMessage(MessageWindow messageWindow, Commons.Message message)
        {
            if (messageWindow.InvokeRequired)
            {
                SetMessageCallback callback = new SetMessageCallback(messageWindow.RecievedNewMessage);
                messageWindow.Invoke(callback, message.Text, message.Sender.UserName);
            }
            else
            {
                messageWindow.RecievedNewMessage(message.Text, message.Sender.UserName);
            }
        }
    }
}
