using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitory
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Semaphore semaphore = new Semaphore(1,1);
            Logic logic = new Logic();
            logic.TakeNumbers(out logic.m, out logic.n);
            Console.WriteLine("Licbza fryzjerów: " + logic.m);
            Console.WriteLine("Liczba manicurzystek: " + logic.n);

            Console.ReadKey();
        }

        
    }
}
