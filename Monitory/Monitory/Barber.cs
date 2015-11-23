using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitory
{
    class Barber : BaseThread
    {
        public bool occupied = false;
        public int name;

        public Barber(int _name)
        {
            name = _name;
        }

        public override void RunThread()
        {
            Thread current = Thread.CurrentThread;
            Console.WriteLine("Fryzjer " + this.name + " w trakcie strzyżenia.");
            Random rand = new Random();
            int time = rand.Next(1000, 3001);
            Console.WriteLine("Strzyzenie bedzie trwac " + time / 100 + " minut");
            Thread.Sleep(time);
            Console.WriteLine("Fryzjer " + this.name + " koniec strzyżenia.");
        }
    }
}
