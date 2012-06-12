using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons;
using System.Net.Sockets;
namespace ChatServer.Engine
{ 
    class ServerProcessor
    {
        public static void ProcessMessage(Commons.Message msg,TcpClient tcpClient)
        {
            Message response = new Message();
            switch (msg.Command)
            {
                case Statics.Commands.ADD:
                    // List all the users.
                    Message ack = new Message();
                    ack.Sender = msg.Sender;
                    ack.Command = Statics.Commands.USERONLINE;
                    foreach(UserWrapper uw in Statics.ExtendedCurrentUsers)
                    {
                        ack.To = uw.User.UserName;
                        if (uw.TcpClient.Connected)
                            Message.Send(ack, uw.TcpClient.GetStream());
                    }
                    Statics.CurrentUsers.Add(msg.Sender);
                    Statics.ExtendedCurrentUsers.Add(new UserWrapper(msg.Sender, tcpClient));
                    response = new Message();
                    response.Command = Statics.Commands.LISTUSERS;
                    response.Text = UserInfo.Serialize(Statics.CurrentUsers);
                    Message.Send(response, tcpClient.GetStream());
                    break;
                case Statics.Commands.SENDMSG:
                    UserWrapper userWrapper = (from UserWrapper uw in Statics.ExtendedCurrentUsers where uw.User.UserName.Equals(msg.To) select uw).ToList<UserWrapper>().First();
                    Message.Send(msg, userWrapper.TcpClient.GetStream());
                    break;
                case Commons.Statics.Commands.USEROFFLINE:
                    if (Statics.CurrentUsers.Contains(msg.Sender))
                        Statics.CurrentUsers.Remove(msg.Sender);
                    break;
            }
        }

        public static void RemoveOfflineUsers()
        {
            for (int counter = 0; counter < Statics.ExtendedCurrentUsers.Count;counter++ )
            {
                UserWrapper uw = Statics.ExtendedCurrentUsers[counter];
                if (!uw.TcpClient.Connected)
                {
                    Statics.CurrentUsers.Remove(uw.User);
                    Statics.ExtendedCurrentUsers.Remove(uw);
                }
            }
        }
    }
}
