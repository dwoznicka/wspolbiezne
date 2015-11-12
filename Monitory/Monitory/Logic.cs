using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitory
{
    class Logic
    {
        public int m, n;

        public void TakeNumbers(out int m, out int n)
        {
            Console.WriteLine("Podaj liczbę fryzjerów: ");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine("Podaj liczbę manicurzystek: ");
            n = int.Parse(Console.ReadLine());
        }
    }
}
