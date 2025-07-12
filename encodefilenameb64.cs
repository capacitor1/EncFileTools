
using System.Text;

namespace EncFileCodec
{
    internal class EncFileCodec
    {
        static void Main(string[] args)
        {
            //parseargs
            List<string> argsnew = new List<string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (!args[i].Contains("\"")) argsnew.Add(args[i]);
                else
                {
                    string arg = args[i] + " ";
                    int j = 1;
                    while (true)
                    {
                        if (!args[i + j].Contains("\""))
                        {
                            arg += args[i + j] + " ";
                        }
                        else
                        {
                            arg += args[i + j];
                            break;
                        }
                        j++;
                    }
                    argsnew.Add(arg);
                    i += j;
                }
            }
            args = argsnew.ToArray();
            foreach (string arg in args)
            {
                string path = (Path.GetDirectoryName(arg) == null) ? "" : Path.GetDirectoryName(arg);
                File.Move(arg,Path.Combine(path,Base64Encode(Path.GetFileName(arg))));
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes).Replace("/","_");
        }
    }
}