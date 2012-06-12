using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ChatClient.Engine
{
    public class HTMLFormatter
    {
        static Hashtable smiliesTable = new Hashtable();
        public static Hashtable SmiliesTable
        {
            get
            {
                return smiliesTable;
            }
            set
            {
                smiliesTable = value;
            }
        }

        public static string DocumentText
        {
            get
            {
                
                return "<html><body style='font-family:trebuchet MS; font-size:12.5px'><span id='spnFooter'></span></body></html>";
            }
        }

        public static string MessageTemplate
        {
            get
            {
                return "<div style='width:100%;'><span style='float:left'>[MESSAGE]</span><span style='float:right;color:grey'>[TIME]</span></div>";
            }
        }

        public static string NewLine
        {
            get
            {
                return "<br/>";
            }
        }

        public static string FormatUserName(string userName)
        {

            return ("<b style='color:CornflowerBlue'>" + userName + "</b>");
        }

        public static string FormatMessage(string message)
        {
            IEnumerator smilieEnum = SmiliesTable.Keys.GetEnumerator();
            while (smilieEnum.MoveNext())
            {
                if (message.Contains(smilieEnum.Current.ToString()))
                {
                    message = message.Replace(smilieEnum.Current.ToString(), "<img src='" + SmiliesTable[smilieEnum.Current.ToString()].ToString() + "'/>");
                }
            }

            message = MessageTemplate.Replace("[MESSAGE]", message).Replace("[TIME]", DateTime.Now.ToString("hh:mm tt"));

            return message;
        }

    }
}
