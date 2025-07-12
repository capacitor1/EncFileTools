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
            Console.WriteLine("Input XOR byte (0 ~ 255):");
            byte xor = Convert.ToByte(Console.ReadLine());
            foreach (string arg in args)
            {
                FileStream fs = new(arg,FileMode.Open, FileAccess.Read);
                FileStream encfs = new(arg + ".xor",FileMode.OpenOrCreate, FileAccess.Write);
                while(fs.Position < fs.Length)
                {
                    encfs.WriteByte((byte)(fs.ReadByte() ^ xor));
                }
                encfs.SetLength(fs.Length);
                Console.WriteLine($"{arg} Done!");
                fs.Close();
                encfs.Close();
            }
        }
    }
}