using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
namespace Commons
{
    public class Statics
    {
        public enum Commands
        {
            ADD,
            LISTUSERS,
            SENDMSG,
            USERONLINE,
            USEROFFLINE,
            USERTYPING
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

        static List<UserWrapper> extendedCurrentUsers = new List<UserWrapper>();

        public static List<UserWrapper> ExtendedCurrentUsers
        {
            get
            {
                return extendedCurrentUsers;
            }
            set
            {
                extendedCurrentUsers = value;
            }
        }
        

        
    }

    public class UserWrapper
    {
        public UserWrapper()
        {
        }

        public UserWrapper(UserInfo usr, TcpClient tcp)
        {
            user = usr;
            tcpClient = tcp;
        }

        UserInfo user;
        public UserInfo User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
            }
        }

        TcpClient tcpClient;
        public TcpClient TcpClient
        {
            get
            {
                return tcpClient;
            }

            set
            {
                tcpClient = value;
            }
        }
    }

   
}
