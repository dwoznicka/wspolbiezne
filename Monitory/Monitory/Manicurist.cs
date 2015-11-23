using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitory
{
    class Manicurist : BaseThread
    {
        public int name;

        public Manicurist(int _name)
        {
            name = _name;
        }

        public override void RunThread()
        {
            Thread current = Thread.CurrentThread;
            Console.WriteLine("Manikurzystka " + this.name + " w trakcie manicure.");
            Console.WriteLine("Manicure trwa 15 minut.");
            Thread.Sleep(1500);
            Console.WriteLine("Manikurzystka " + this.name + "koniec manicure.");
        }
    }
}
