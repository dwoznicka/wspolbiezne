using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spotkania
{
	class Manicurist : BaseThread
	{
		public int name;
		Boss boss;

		private ManualResetEvent entryBegin = new ManualResetEvent(false);
		private AutoResetEvent entryEnd = new AutoResetEvent(false);

		public Manicurist(int _name, Boss _boss)
		{
			name = _name;
			boss = _boss;
		}

		public void Manicure(Client client)
		{
			Console.WriteLine("Klient " + client.name + " idzie do manikiurzystki " + name);

			lock (this)
			{
				Console.WriteLine("Manikiurzystka " + name + " rozpoczyna manicure (spotkanie) klienta " + client.name);
				Console.WriteLine("Manicure bedzie trwac 15 minut");
				//client.manicure_wanted = false;
				if (boss.waiting_room.Contains(client))
					boss.waiting_room.Remove(client);
				entryBegin.Set(); // sygnalizacja zdarzenia entryBegin (czyli rozpoczęcia spotkania Manicure)
				entryEnd.WaitOne(); //blokada wątku wywołującego do czasu zakończenia spotkania 
				Thread.Sleep(1500); //manicure trwa 15 minut (1.5 sek)
				Console.WriteLine("Manikiurzystka " + name + " koniec manicure (spotkania) klienta " + client.name);

			} //opuszczenie sekcji krytycznej => następny wątek może rozpocząć spotkanie
			client.manicure_done = true;
			if (client.haircut_done)
			{
				Console.WriteLine("Klient " + client.name + " wychodzi.");
				boss.ClientLeaves(out client);
			}
		}

		public override void RunThread()
		{
			while (true)
			{
				entryBegin.WaitOne(); //blokada wątku serwera, jeśli zdarzenie entryBegin nie było sygnalizowane
				entryBegin.Reset(); //ustawia zdarzenie entryBegin jako nie sygnalizowane
				entryEnd.Set(); //zakonczenie spotkania i odblokowanie watku wywolujacego
			}
		}
	}
}
