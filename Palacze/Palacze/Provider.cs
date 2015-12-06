using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Palacze
{
	class Provider : BaseThread
	{
		public string name;
		public bool tobacco = false;
		public bool papers = false;
		public bool matches = false;

		public Provider(bool _tobacco, bool _papers, bool _matches, string _name)
		{
			tobacco = _tobacco;
			papers = _papers;
			matches = _matches;
			name = _name;
		}

		public override void RunThread()
		{
		//	Thread transaction = Thread.CurrentThread;

		//	Random random = new Random();
		//	int time = random.Next(500, 2001);
		//	Console.WriteLine("Transakcja z " + name + " będzie trwała " + time / 1000.00f + " sekund");
		//	Thread.Sleep(time);
		//	Console.WriteLine("Koniec transakcji " + name);
		}

		public void Transaction(Smoker s)
		{			
			Thread transaction = Thread.CurrentThread;

			Random random = new Random();
			int time = random.Next(1500, 3001);
			Console.WriteLine("Transakcja dostawcy " + name + " z palaczem " + s.name + " będzie trwała " + time / 1000.00f + " sekund");
			Thread.Sleep(time);
			Console.WriteLine("Koniec transakcji dostawcy " + name + " z palaczem " + s.name);
		}

		public void SmokerLeaves(Smoker s)
		{
			Console.WriteLine("Palacz " + s.name + " wychodzi");
			s = null;
		}
	}
}
