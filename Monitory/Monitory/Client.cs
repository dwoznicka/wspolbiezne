using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitory
{
    class Client
    {
        public bool manicure_wanted = false;
        public bool haircut_in_progress = false;
        public bool manicure_in_progress = false;
        public bool haircut_done = false;
        public bool manicure_done = false;
        public int name;
        
        //Thread haircut;
        //Thread manicure;

        public Client(int _name)
        {
            name = _name;
            Random rand = new Random();
            int temp = rand.Next(0, 3);
            if (temp == 0)
                manicure_wanted = true;
            else
                manicure_wanted = false;
        }

        public void Haircut(Barber barber)
        {
             Console.WriteLine("Fryzjer " + barber.name + " rozpoczyna strzyzenie klienta " + this.name);
             //haircut = new Thread(new ThreadStart(barber.RunThread));
            //haircut = new Thread(() => barber.RunThread());

            //haircut.Start();

            Random rand = new Random();
            int time = rand.Next(1000, 3001);
            Console.WriteLine("Strzyzenie bedzie trwac " + time / 100 + " minut");
            haircut_in_progress = true;
            Thread.Sleep(time);
            Console.WriteLine("Fryzjer " + this.name + " koniec strzyżenia.");
            haircut_done = true;
        }

        public void Manicure(Manicurist manicurist)
        {
            Console.WriteLine("Manikurzystka " + manicurist.name + " rozpoczyna manicure klienta " + this.name);
           // manicure = new Thread(() => manicurist.RunThread());

           // manicure = new Thread(new ThreadStart(manicurist.RunThread));
           // manicure.Start();

            Console.WriteLine("Manicure trwa 15 minut.");
            manicure_in_progress = true;
            Thread.Sleep(1500);
            Console.WriteLine("Manikurzystka " + this.name + "koniec manicure.");
            manicure_done = true;
        }

        public bool WaitForRoom(Boss boss)
        {
            Random rand = new Random();
            int patience = rand.Next(0, 41);
            bool entered = false;

            Console.WriteLine(this.name + "  Poczekam " + patience/2 + " minut na wolne miejsce w poczekalni");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (sender, e) => {
                patience--;
                if(patience > 0)
                {
                    entered = HandleRoom(boss);
                    if(entered == true)
                    {
                        Console.WriteLine(this.name + "  Zanim wyszedłem pojawiło się miejsce w poczekalni. Wchodzę do poczekalni.");
                        timer.Stop();
                    }
                }
                else
                {
                    Console.WriteLine(this.name + "  Skończyła mi się cierpliwość. Wychodzę.");
                    entered = false;
                    timer.Stop();
                }
            };
            timer.Interval = 500;
            timer.Enabled = true;

            return entered;
        }

        bool HandleRoom(Boss boss)
        {
            if(boss.waiting_room.Count < boss.waiting_room.Capacity - 1)
            {
                boss.waiting_room.Add(this);
                return true;
            }
            else
            {
                return false;
            }
        }       
    }
}
