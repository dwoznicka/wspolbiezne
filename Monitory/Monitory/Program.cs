using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Monitory
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = -1;
            Semaphore semaphore = new Semaphore(1,1);
            Logic logic = new Logic();
            logic.TakeNumbers(out logic.liczbaFryzjerow, out logic.liczbaManikiurzystek, out logic.pojemnoscPoczekalni);
            Console.WriteLine("Licbza fryzjerów: " + logic.liczbaFryzjerow);
            Console.WriteLine("Liczba manikiurzystek: " + logic.liczbaManikiurzystek);
            Console.WriteLine("Pojemność poczekalni: " + logic.pojemnoscPoczekalni);

            Boss boss = new Boss(logic.liczbaFryzjerow, logic.liczbaManikiurzystek, logic.pojemnoscPoczekalni);

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (sender, e) => { counter++; HandleTimerElapsed(boss, counter);  };
            timer.Interval = 1000;
            timer.Enabled = true;

            if (Console.Read() == 'q')
                timer.Enabled = false;

            Console.ReadKey();
        }

        static void HandleTimerElapsed(Boss boss, int counter)
        {
            CreateClient(boss, counter);
        }

        private static void CreateClient(Boss boss, int counter)
        {
            Client client = new Client(counter);
            Console.WriteLine("Przybył klient " + client.name);
            boss.TakeClient(client);
        }        
    }
}
