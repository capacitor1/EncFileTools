
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
            //
            foreach (string arg in args)
            {
                FileStream fs = new(arg, FileMode.Open, FileAccess.Read);
                long pos = -1;
                string real = string.Empty;
                pos = IndexOf(fs, zip, 0);
                if(pos == 0)
                {
                    real = ".zip";
                    goto Here;
                }
                pos = IndexOf(fs, rar, 0);
                if (pos == 0)
                {
                    real = ".rar";
                    goto Here;
                }
                pos = IndexOf(fs, sevenz, 0);
                if (pos == 0)
                {
                    real = ".7z";
                    goto Here;
                }
                //
                if (real == string.Empty)
                {
                    Console.WriteLine($"{arg} Failed. Compressed files not correct or not supported.");
                    continue;
                }
                Here:
                FileStream fsc = new(arg + "~1", FileMode.OpenOrCreate, FileAccess.Write);
                var random = new Random();
                byte[] returnBytes = new byte[random.Next((int)Math.Ceiling((double)(fs.Length * 0.001)), (int)Math.Ceiling((double)(fs.Length * 0.1)))];
                random.NextBytes(returnBytes);
                fsc.Write(returnBytes);
                fsc.Flush();
                fs.Position = 0;
                fs.CopyTo(fsc);
                fs.Close();
                fsc.Close();
                Console.WriteLine($"{arg} Done!");
            }
        }
        public static byte[] zip = { 0x50,0x4b,0x03,0x04};
        public static byte[] rar = { 0x52 ,0x61 ,0x72 ,0x21 ,0x1A ,0x07 ,0x00 };
        public static byte[] sevenz = { 0x37 ,0x7A, 0xBC ,0xAF ,0x27 ,0x1C };
        public static long IndexOf(FileStream srcBytes, byte[] searchBytes, long offset = 0)
        {
            if (offset == -1) { return -1; }
            if (srcBytes == null) { return -1; }
            if (searchBytes == null) { return -1; }
            if (srcBytes.Length == 0) { return -1; }
            if (searchBytes.Length == 0) { return -1; }
            if (srcBytes.Length < searchBytes.Length) { return -1; }
            for (long i = offset; i < srcBytes.Length - searchBytes.Length; i++)
            {
                if (GetByte(srcBytes,i) != searchBytes[0]) continue;
                if (searchBytes.Length == 1) { return i; }
                var flag = true;
                for (long j = 1; j < searchBytes.Length; j++)
                {
                    if (GetByte(srcBytes, i + j) != searchBytes[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) { return i; }
            }
            return -1;
        }
        public static byte GetByte(FileStream fs,long pos)
        {
            fs.Position = pos;
            byte b = (byte)fs.ReadByte();
            fs.Position--;
            return b;
        }
    }
}