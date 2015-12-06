using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palacze
{
	class Program
	{
		static void Main(string[] args)
		{
			Provider providerTP = new Provider(true, true, false, "TP");
			Provider providerTM = new Provider(true, false, true, "TM");
			Provider providerPM = new Provider(false, true, true, "PM");

			List<Provider> providers = new List<Provider>();
			providers.Add(providerTP);
			providers.Add(providerTM);
			providers.Add(providerPM);

			providerTP.Start();
			providerTM.Start();
			providerPM.Start();

			int counter = -1;

			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += (sender, e) => { counter++; HandleTimerElapsed(counter, providers); };
			timer.Interval = 1000;
			timer.Enabled = true;

			if (Console.Read() == 'q')
				timer.Enabled = false;

			Console.ReadKey();
		}

		static void HandleTimerElapsed(int counter, List<Provider> providers)
		{
			CreateClient(counter, providers);
		}

		private static void CreateClient(int counter, List<Provider> providers)
		{
			Smoker smoker = new Smoker(counter, providers);
			if (smoker.tobacco)
				Console.WriteLine("Przybył palacz " + smoker.name + " i posiada tytoń");
			else if(smoker.papers)
				Console.WriteLine("Przybył palacz " + smoker.name + " i posiada bibułki");
			else
				Console.WriteLine("Przybył palacz " + smoker.name + " i posiada zapałki");

			smoker.CheckProviders();
		}
	}
}
