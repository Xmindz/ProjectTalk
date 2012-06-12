using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Reflection;
using ChatClient.Engine;
namespace ChatClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PropertyInfo[] properties = typeof(Smilies).GetProperties(BindingFlags.Static | BindingFlags.NonPublic);
            string smiliePath = string.Empty;

            if (!Directory.Exists("smilies"))
            {
                Directory.CreateDirectory("smilies");
                foreach (PropertyInfo property in properties)
                {
                    object img = (property.GetValue(null, null));//.Save(Path.Combine("smilies", property.Name));
                    if (img != null)
                        if (img.GetType() == typeof(Bitmap))
                        {
                            smiliePath = Path.Combine(Application.StartupPath, "smilies");
                            ((Bitmap)(property.GetValue(null, null))).Save(Path.Combine(smiliePath, property.Name + ".gif"));
                            HTMLFormatter.SmiliesTable.Add("(" + property.Name + ")", smiliePath);
                        }
                }
            }
            else
            {

                foreach (PropertyInfo property in properties)
                {
                    object img = (property.GetValue(null, null));//.Save(Path.Combine("smilies", property.Name));
                    if (img != null)
                        if (img.GetType() == typeof(Bitmap))
                        {
                            smiliePath = Path.Combine(Application.StartupPath, "smilies");
                            //((Bitmap)(property.GetValue(null, null))).Save(Path.Combine(smiliePath, property.Name + ".gif"));
                            HTMLFormatter.SmiliesTable.Add("(" + property.Name + ")", Path.Combine(smiliePath, property.Name + ".gif"));
                        }
                }

            }

            HTMLFormatter.SmiliesTable.Add(":)", Path.Combine(smiliePath, "smile.gif"));
            HTMLFormatter.SmiliesTable.Add(":(", Path.Combine(smiliePath, "sadsmile.gif"));
            HTMLFormatter.SmiliesTable.Add(":D", Path.Combine(smiliePath, "bigsmile.gif"));
            HTMLFormatter.SmiliesTable.Add(":d", Path.Combine(smiliePath, "bigsmile.gif"));

            HTMLFormatter.SmiliesTable.Add("8)", Path.Combine(smiliePath, "cool.gif"));
            HTMLFormatter.SmiliesTable.Add(":O", Path.Combine(smiliePath, "cool.gif"));
            HTMLFormatter.SmiliesTable.Add(";)", Path.Combine(smiliePath, "wink.gif"));
            HTMLFormatter.SmiliesTable.Add(";(", Path.Combine(smiliePath, "crying.gif"));
            HTMLFormatter.SmiliesTable.Add(":|", Path.Combine(smiliePath, "speechless.gif"));
            HTMLFormatter.SmiliesTable.Add(":*", Path.Combine(smiliePath, "kiss.gif"));
            HTMLFormatter.SmiliesTable.Add(":p", Path.Combine(smiliePath, "tongueout.gif"));
            HTMLFormatter.SmiliesTable.Add(":P", Path.Combine(smiliePath, "tongueout.gif"));
            HTMLFormatter.SmiliesTable.Add("|)", Path.Combine(smiliePath, "sleepy.gif"));
            HTMLFormatter.SmiliesTable.Add("|(", Path.Combine(smiliePath, "dull.gif"));
            HTMLFormatter.SmiliesTable.Add("(y)", Path.Combine(smiliePath, "yes.gif"));
            HTMLFormatter.SmiliesTable.Add("(n)", Path.Combine(smiliePath, "no.gif"));

            Application.Run(new UserListWindow());
        }
    }
}
