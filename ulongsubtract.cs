using System;

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
            byte[] buffer = new byte[8];
            foreach (string arg in args)
            {
                FileStream fs = new(arg,FileMode.Open, FileAccess.Read);
                FileStream encfs = new(arg + "~1",FileMode.OpenOrCreate, FileAccess.Write);
                while (fs.Position < fs.Length)
                {
                    fs.Read(buffer);
                    encfs.Write(BitConverter.GetBytes(0xFFFFFFFFFFFFFFFFL - BitConverter.ToUInt64(buffer)));
                }
                encfs.SetLength(fs.Length);
                Console.WriteLine($"{arg} Done!");
                fs.Close();
                encfs.Close();
            }
        }
    }
}