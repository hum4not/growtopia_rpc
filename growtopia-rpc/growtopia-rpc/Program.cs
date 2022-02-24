using DiscordRPC;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace growtopiarpc {
    public partial class program {


        

        public static void Main()
        {
            DiscordRpcClient
            clientr = new DiscordRpcClient($"945760328166305792");
            string GetGrowID(string path)
            {
                string result;
                try
                {
                    string text = null;
                    File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                    Regex regex = new Regex("[^\\w0-9]");
                    string text2 = text.Replace("\0", " ");
                    string text3 = regex.Replace(text2.Substring(text2.IndexOf("tankid_name") + "tankid_name".Length).Split(new char[]
                    {
                            ' '
                    })[3], string.Empty);
                    if (text3 == null)
                    {
                        result = "Error [No GrowID]";
                    }
                    else
                    {
                        result = text3;
                    }
                }
                catch (Exception ex)
                {
                    result = "Error [" + ex.Message + "]";
                }
                return result;
            }

            string GetLastWorld(string path)
            {
                string result;
                try
                {
                    string text = null;
                    File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                    Regex regex = new Regex("[^\\w0-9]");
                    string text2 = text.Replace("\0", " ");
                    string text3 = regex.Replace(text2.Substring(text2.IndexOf("lastworld") + "lastworld".Length).Split(new char[]
                    {
                            ' '
                    })[3], string.Empty);
                    if (text3 == null)
                    {
                        result = "Error [No GrowID]";
                    }
                    else
                    {
                        result = text3;
                    }
                }
                catch (Exception ex)
                {
                    result = "Error [" + ex.Message + "]";
                }
                return result;
            }
            void update_rpc(string t1, string t2)
            {

                {
                    clientr.SetPresence(new DiscordRPC.RichPresence()
                    {
                        Details = $"GrowID: {t1}",
                        State = $"World: {t2}",
                        Timestamps = Timestamps.Now,

                        Assets = new Assets()
                        {
                            LargeImageKey = "gt",
                            LargeImageText = "Growtopia",

                        }

                    });
                }
            }

            clientr.Initialize();
            Thread launch_rpc = new Thread(() =>
            {
                while (true) {
                    Console.WriteLine("Sleep 15sec");
                    Thread.Sleep(15000);
                    string location = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%") + @"\Growtopia\save.dat";
                    string user_growid = GetGrowID(location);
                    string user_world = GetLastWorld(location);
                    update_rpc(user_growid, user_world);
                    Console.WriteLine($"Updated: {user_growid}, {user_world}");
                }
            });
            launch_rpc.Start();
            launch_rpc.Join(1);
        }

 
    }
   
}