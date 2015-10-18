using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semafory
{
    class Program
    {
        static Thread[] threads = new Thread[2];
        static Semaphore sem = new Semaphore(1, 1);
        static void semaphoring()
        {
            Console.WriteLine("{0} is waiting in line...", Thread.CurrentThread.Name);
            sem.WaitOne();
            Console.WriteLine("{0} enters the semaphore!", Thread.CurrentThread.Name);
            Thread.Sleep(300);
            Console.WriteLine("{0} is leaving the semaphore", Thread.CurrentThread.Name);
            sem.Release();
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 2; i++)
            {
                threads[i] = new Thread(semaphoring);
                threads[i].Name = "thread_" + i;
                threads[i].Start();
            }
            Console.Read();
        }
    }
}
