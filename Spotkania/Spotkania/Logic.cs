using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotkania
{
	class Logic
	{
		public int liczbaFryzjerow, liczbaManikiurzystek, pojemnoscPoczekalni;

		public void TakeNumbers(out int liczbaFryzjerow, out int liczbaManikiurzystek, out int pojemnoscPoczekalni)
		{
			Console.WriteLine("Podaj liczbę fryzjerów: ");
			liczbaFryzjerow = int.Parse(Console.ReadLine());
			Console.WriteLine("Podaj liczbę manikiurzystek: ");
			liczbaManikiurzystek = int.Parse(Console.ReadLine());
			Console.WriteLine("Podaj pojemność poczekalni: ");
			pojemnoscPoczekalni = int.Parse(Console.ReadLine());
		}
	}
}
