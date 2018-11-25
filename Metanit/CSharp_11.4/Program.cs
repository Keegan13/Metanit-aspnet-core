using System.Threading.Tasks;
using System;
using System.Threading;

namespace CSharp_11._4
{
    class Program
    {
        static int x = 0;
        static object locker = new Object();
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(Count);
                t.Name ="Thread "+i.ToString();
                t.Start();
                
            }

            Console.ReadKey();
        }
         static void Count()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 0; i < 9; i++)
                {

                    Console.WriteLine("{0}\tx={1}", Thread.CurrentThread.Name, x);
                    x++;
                }
            }
        }
    }
}
