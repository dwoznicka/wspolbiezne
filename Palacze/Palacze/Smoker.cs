using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palacze
{
	class Smoker
	{
		public int name;
		public bool tobacco = false;
		public bool papers = false;
		public bool matches = false;

		List<Provider> providers;

		public Smoker(int _name, List<Provider> _providers)
		{
			name = _name;
			providers = _providers;

			Random random = new Random();
			int rand = random.Next(0, 3);
			switch(rand)
			{
				case 0:
					tobacco = true;
					break;
				case 1:
					papers = true;
					break;
				case 2:
					matches = true;
					break;
				default:
					break;
			}
		}

		public void CheckProviders()
		{
			foreach(Provider p in providers)
			{
				if(tobacco && !p.tobacco)
				{
					Console.WriteLine("Palacz " + name + " znalazł dostawcę " + p.name);
					Transaction(p);
				}
				else if(papers && !p.papers)
				{
					Console.WriteLine("Palacz " + name + " znalazł dostawcę " + p.name);
					Transaction(p);
				}
				else if(matches && !p.matches)
				{
					Console.WriteLine("Palacz " + name + " znalazł dostawcę " + p.name);
					Transaction(p);
				}
			}
		}

		public void Transaction(Provider p)
		{
			Console.WriteLine("Palacz " + name + " przychodzi do dostawcy " + p.name);
			Monitor.Enter(p);
			try
			{
				Console.WriteLine("Palacz " + name + " rozpoczyna transakcję z " + p.name);
				p.Transaction(this);
			}
			finally
			{
				Console.WriteLine("Palacz " + name + " kończy transakcję z " + p.name);
				Monitor.Exit(p);
				p.SmokerLeaves(this);
			}
		}

		
	}
}
