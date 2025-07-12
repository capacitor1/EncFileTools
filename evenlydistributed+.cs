
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
            if (args.Length != 2) throw new Exception("Must: Enter only one pair of files (A and B) at a time");
            string A = string.Empty, B = string.Empty;
            if (Path.GetFileName(args[0]).EndsWith("A"))
            {
                A = args[0];
                B = args[1];
            }
            else
            {
                B = args[0];
                A = args[1];
            }
            FileStream fs = new(args[0].Substring(0, args[0].Length - 1), FileMode.OpenOrCreate, FileAccess.Write);
            FileStream encfsa = new(A, FileMode.Open, FileAccess.Read);
            FileStream encfsb = new(B, FileMode.Open, FileAccess.Read);
            bool is2 = false;
            if ((encfsa.Length + encfsb.Length) % 2 == 0) is2 = true;
            while (fs.Position < (encfsa.Length + encfsb.Length) - 2)
            {
                fs.WriteByte((byte)encfsa.ReadByte());
                fs.WriteByte((byte)encfsb.ReadByte());
            }
            //
            if (is2)
            {
                fs.WriteByte((byte)encfsa.ReadByte());
                fs.WriteByte((byte)encfsb.ReadByte());
            }
            else
            {
                fs.WriteByte((byte)encfsa.ReadByte());
            }
            //
            Console.WriteLine($"Done!");
            fs.Dispose();
            encfsa.Dispose();
            encfsb.Dispose();

        }
    }
}