using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotkania
{
	class Program
	{
		static void Main(string[] args)
		{
			int counter = -1;

			Logic logic = new Logic();
			logic.TakeNumbers(out logic.liczbaFryzjerow, out logic.liczbaManikiurzystek, out logic.pojemnoscPoczekalni);
			Console.WriteLine("Licbza fryzjerów: " + logic.liczbaFryzjerow);
			Console.WriteLine("Liczba manikiurzystek: " + logic.liczbaManikiurzystek);
			Console.WriteLine("Pojemność poczekalni: " + logic.pojemnoscPoczekalni);

			Boss szefu = new Boss(logic.liczbaFryzjerow, logic.liczbaManikiurzystek, logic.pojemnoscPoczekalni);

			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += (sender, e) => { counter++; HandleTimerElapsed(szefu, counter); };
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
			Client client = new Client(counter, boss);
			if (client.manicure_wanted)
				Console.WriteLine("Przybył klient " + client.name + " na strzyżenie i manicure");
			else
				Console.WriteLine("Przybył klient " + client.name + " tylko na strzyżenie");

			client.Start();
		}
	}
}
