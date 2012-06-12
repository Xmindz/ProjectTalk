using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Threading;
using Commons;
using ChatServer.Engine;
namespace ChatServer
{
    class Program
    {
        static TcpListener tcpListener;
        static void Main(string[] args)
        {
            ThreadStart tsWaitClient = new ThreadStart(Program.WaitClient);
            Thread thWaitClient = new Thread(tsWaitClient);

            tcpListener = new TcpListener(1269);
            tcpListener.Start();
            InitiateLogging();
            LogMessage("Server started at port:1269 ");
            thWaitClient.Start();

        }

        static void WaitClient()
        {
            ThreadStart tsReadMessage;
            Thread thReadMessage;

            TcpClient tcpClient;
            LogMessage("Entering listening mode...");
            while (true)
            {
                tcpClient = tcpListener.AcceptTcpClient();
                LogMessage("Client Connected.");
                
                
                //Commons.Statics.CurrentUsers.Add
                tsReadMessage = delegate { Program.ReadMessage(tcpClient); };
                thReadMessage = new Thread(tsReadMessage);
                thReadMessage.Start();
            }
        }

        static void ReadMessage(TcpClient tcpClient)
        {
            while (true)
            {
                byte[] buffer = new byte[255];
                Message msg = new Message();
                try
                {
                    msg = Message.Deserialize(tcpClient.GetStream());
                    LogMessage("Message from client: " + msg.Sender.UserName+ "->" + msg.To+ ", command:" + msg.Command.ToString());
                    ServerProcessor.ProcessMessage(msg, tcpClient);
                }
                catch (Exception ex)
                {
                    if (!tcpClient.Connected)
                    {
                        // Client gone offline.
                        LogMessage("Client went offline.");
                        ServerProcessor.RemoveOfflineUsers();
                        break;
                    }
                }
                
            }
        }

        static void LogMessage(string text)
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "\t" + text);
        }
        static void InitiateLogging()
        {
            Console.WriteLine("ProjectTalk(TM) Chat Server.");
            Console.WriteLine("Copyright (c) www.xmindz.com\n");
        }
    }
}
