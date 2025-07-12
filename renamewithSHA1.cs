
using System.Security.Cryptography;
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
                string S1 = GetSHA1(arg);
                File.Move(arg,Path.Combine(path,S1) + Path.GetExtension(arg));
            }
        }
        static string GetSHA1(string s)
        {
            FileStream file = new FileStream(s, FileMode.Open);
            SHA1 sha1 = SHA1.Create();
            byte[] retval = sha1.ComputeHash(file);
            file.Close();

            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < retval.Length; i++)
            {
                sc.Append(retval[i].ToString("x2"));
            }
            return sc.ToString();
        }
    }
}