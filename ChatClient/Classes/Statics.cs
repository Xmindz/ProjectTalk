using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Commons;
namespace ChatClient.Engine
{
    class Statics
    {
        static TcpClient currentClient;
        public static TcpClient CurrentClient
        {
            get
            {
                return currentClient;
            }
            set
            {
                currentClient = value;
            }
        }

        static Commons.UserInfo currentUser;
        public static Commons.UserInfo CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }

        static List<UserInfo> currentUsers = new List<UserInfo>();

        public static List<UserInfo> CurrentUsers
        {
            get
            {
                return currentUsers;
            }
            set
            {
                currentUsers = value;
            }
        }

        static List<Message> messageQueue = new List<Message>();
        public static List<Message> MessageQueue
        {
            get
            {
                return messageQueue;
            }
            set
            {
                
                messageQueue = value;
            }
        }

        static List<ChatClient.MessageWindow> messageWindows = new List<MessageWindow>();
        public static List<MessageWindow> MessageWindows
        {
            get
            {
                return messageWindows;
            }
            set
            {
                messageWindows = value;
            }
        }

    }


}
