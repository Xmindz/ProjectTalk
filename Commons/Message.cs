using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters;
using System.Net.Sockets;
using System.IO;
namespace Commons
{
    [Serializable]
    public class Message
    {
        Statics.Commands command;
        public Statics.Commands Command
        {
            get
            {
                return command;
            }

            set
            {
                command = value;
            }
        }

        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        UserInfo sender;
        public UserInfo Sender
        {
            get
            {
                return sender;
            }
            set
            {
                sender = value;
            }
        }

        string to;
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
            }
        }

        public static byte[] Serialize(Message msg,NetworkStream ns)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //System.IO.Stream serializedStream=System.IO.Stream.Null;
            bf.Serialize(ns, msg);
            byte[] buffer = new byte[20];
            //ns.Read(buffer, 0, (int)ns.Length);
            return buffer;
        }

        public static Message Deserialize(NetworkStream ns)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            object obj = bf.Deserialize(ns);
            return (Message)obj;
        }

        public static bool Send(Message msg, NetworkStream ns)
        {
            byte[] buffer = Message.Serialize(msg,ns);
            //ns.Write(buffer, 0, buffer.Length);
            return true;
        }
    }
}
