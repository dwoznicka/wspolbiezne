using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotkania
{
	class Barber : BaseThread
	{
		public int name;
		Boss boss;

		private ManualResetEvent entryBegin = new ManualResetEvent(false);
		private AutoResetEvent entryEnd = new AutoResetEvent(false);
		private object entryLock = new object();

		public Barber(int _name, Boss _boss)
		{
			name = _name;
			boss = _boss;
		}

		public void Haircut(Client client)
		{
			Random rand = new Random();
			int time = rand.Next(1000, 3001);
			Console.WriteLine("Klient " + client.name + " idzie do fryzjera " + name);

			lock (this)
			{
				Console.WriteLine("Fryzjer " + name + " rozpoczyna strzyzenie (spotkanie) klienta " + client.name);
				Console.WriteLine("Strzyzenie bedzie trwac " + time / 100 + " minut");

				if (boss.waiting_room.Contains(client))
					boss.waiting_room.Remove(client);
				entryBegin.Set(); // sygnalizacja zdarzenia entryBegin (czyli rozpoczęcia spotkania Haircut)
				entryEnd.WaitOne(); //blokada wątku wywołującego do czasu zakończenia spotkania 
				Thread.Sleep(time); //strzyżenie trwa między 10-30 minut (1-3 sek)
				Console.WriteLine("Fryzjer " + name + " koniec strzyżenia (spotkania) klienta " + client.name);
			} //opuszczenie sekcji krytycznej => następny wątek może rozpocząć spotkanie
			client.haircut_done = true;
			if ((client.manicure_wanted && client.manicure_done) || !client.manicure_wanted)
			{
				Console.WriteLine("Klient " + client.name + " wychodzi.");
				boss.ClientLeaves(out client);
			}
		}

		public override void RunThread()
		{
			while(true)
			{
				entryBegin.WaitOne(); //blokada wątku serwera, jeśli zdarzenie entryBegin nie było sygnalizowane
				entryBegin.Reset(); //ustawia zdarzenie entryBegin jako nie sygnalizowane
				entryEnd.Set(); //zakonczenie spotkania i odblokowanie watku wywolujacego
			}
		}
	}
}
