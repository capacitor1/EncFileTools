
using System;
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
                FileStream fs = new(arg, FileMode.Open, FileAccess.Read);
                FileStream encfsa = new(arg + "~1A", FileMode.OpenOrCreate, FileAccess.Write);
                FileStream encfsb = new(arg + "~1B", FileMode.OpenOrCreate, FileAccess.Write);
                bool is2 = false;
                if (fs.Length % 2 == 0) is2 = true;
                while (fs.Position < fs.Length - 2)
                {
                    encfsa.WriteByte((byte)fs.ReadByte());
                    encfsb.WriteByte((byte)fs.ReadByte());
                }
                //
                if (is2)
                {
                    encfsa.WriteByte((byte)fs.ReadByte());
                    encfsb.WriteByte((byte)fs.ReadByte());
                }
                else
                {
                    encfsa.WriteByte((byte)fs.ReadByte());
                }
                //
                Console.WriteLine($"{arg} Done!");
                fs.Dispose();
                encfsa.Dispose();
                encfsb.Dispose();
            }
        }
    }
}