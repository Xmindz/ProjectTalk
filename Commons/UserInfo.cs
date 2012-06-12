using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Commons
{
    [Serializable]
    public class UserInfo
    {
        string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        string identifier;
        
        public string Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                identifier = value;
            }
        }

        public static string Serialize(List<UserInfo> userList)
        {
            MemoryStream memoryStream=new MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(memoryStream, userList);
            StreamReader sr = new StreamReader(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return sr.ReadToEnd();
        }

        public static List<UserInfo> Deserialize(string userString)
        {
            byte[] userBytes = ASCIIEncoding.ASCII.GetBytes(userString.ToCharArray());
            MemoryStream memoryStream = new MemoryStream(userBytes);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            memoryStream.Seek(0, SeekOrigin.Begin);
            return (List<UserInfo>)bf.Deserialize(memoryStream);
        }

    }
}
