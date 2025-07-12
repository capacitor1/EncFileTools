
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
            Console.WriteLine("Enter HEX string:");
            string? bytesstr = Console.ReadLine();
            if (bytesstr == null)
            {
                Console.WriteLine($"You must enter the HEX string you want to retrieve.");
                return;
            }
            byte[] se;
            if (!IsIllegalHexadecimal(bytesstr))
            {
                se = Encoding.Default.GetBytes(bytesstr);
            }
            else
            {
                se = strToToHexByte(bytesstr);
            }
            foreach (string arg in args)
            {
                FileStream fs = new(arg, FileMode.Open, FileAccess.Read);
                long pos = IndexOf(fs, se, 0);
                FileStream fsc = new(arg + $"~1.Pos={pos.ToString("X16")}", FileMode.OpenOrCreate, FileAccess.Write);
                fs.Position = pos;
                fs.CopyTo(fsc);
                fs.Close();
                fsc.Close();
                Console.WriteLine($"{arg} Done!");
            }
        }
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        public const string PATTERN = @"^[0-9A-Fa-f]+$";
        public static bool IsIllegalHexadecimal(string hex)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hex, PATTERN);
        }
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