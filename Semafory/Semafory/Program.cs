using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semafory
{
    public class Program
    {
        public Logic log = new Logic();

        static Semaphore sem = new Semaphore(1, 1);

        void semaphoring()
        {
            while (log.stop == false)
            {
                Console.WriteLine("{0} is waiting in line...", Thread.CurrentThread.Name);
                sem.WaitOne();
                Console.WriteLine("{0} enters the semaphore!", Thread.CurrentThread.Name);
                if (Thread.CurrentThread.Name == "thread_x")
                {
                    if (log.indexX == -1)
                        log.temp = log.FindEvenNumberInX();
                }
                else if(Thread.CurrentThread.Name == "thread_y")
                {
                    if (log.indexY == -1)
                        log.temp = log.FindOddNumberInY();
                }
                Thread.Sleep(300);
                Console.WriteLine("{0} is leaving the semaphore", Thread.CurrentThread.Name);
                sem.Release();
            }
        }


        static void Main(string[] args)
        {
            Program pro = new Program();

            pro.log.InsertSize();
            pro.log.ShowLists();
            
            Thread threadX = new Thread(pro.semaphoring);
            threadX.Name = "thread_x";
            threadX.Start();

            Thread threadY = new Thread(pro.semaphoring);
            threadY.Name = "thread_y";
            threadY.Start();

            pro.log.ShowLists();

            Console.Read();
        }
    }
}
