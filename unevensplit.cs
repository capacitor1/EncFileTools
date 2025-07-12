
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
            byte[] buf = new byte[1048576];
            foreach (string arg in args)
            {
                FileStream fs = new(arg,FileMode.Open, FileAccess.Read);
                
                long splitseq = 1;
                long maxsplit = 262144000;
                long minsplit = 10485760;
                while (fs.Position < fs.Length)
                {
                    while(fs.Length - fs.Position < maxsplit)
                    {
                        maxsplit = (fs.Length - fs.Position) / 2;
                        minsplit = maxsplit / 25;
                    }
                    long thissize = NextLong(minsplit,maxsplit);
                    if (thissize < 10485760) thissize = fs.Length - fs.Position;
                    long finishsize = fs.Position + thissize;
                    FileStream encfs = new(arg + $"~1.{splitseq.ToString().PadLeft(3, '0')}", FileMode.OpenOrCreate, FileAccess.Write);
                    while(fs.Position < finishsize)
                    {
                        int c = fs.Read(buf);
                        encfs.Write(buf,0,c);
                    }
                    encfs.Close();
                    splitseq++;
                }
                Console.WriteLine($"{arg} Done!");
                fs.Close();
            }
        }
        public static Random R = new Random();
        public static long NextLong(long A, long B)
        {
            long myResult = A;
            //-----
            long Max = B, Min = A;
            if (A > B)
            {
                Max = A;
                Min = B;
            }
            double Key = R.NextDouble();
            myResult = Min + (long)((Max - Min) * Key);
            //-----
            return myResult;
        }
    }
}