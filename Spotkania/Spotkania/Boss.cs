using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotkania
{
	class Boss
	{
		List<Barber> barbers = new List<Barber>();
		List<Manicurist> manicurists = new List<Manicurist>();
		public List<Client> waiting_room;
		public List<Client> clients_at_barber = new List<Client>();
		public List<Client> clients_at_manicurist = new List<Client>();

		private int lastBarber = -1;
		private int lastManicurist = -1;

		public Boss(int barbersCount, int manicuristsCount, int waitingRoomCapacity)
		{
			for (int i = 0; i < barbersCount; i++)
			{
				barbers.Add(new Barber(i, this));
				barbers[i].Start();
			}

			for (int i = 0; i < manicuristsCount; i++)
			{
				manicurists.Add(new Manicurist(i, this));
				manicurists[i].Start();
			}

			waiting_room = new List<Client>(waitingRoomCapacity);
		}

		public void TakeClient(Client client)
		{
			bool wantManicure = client.manicure_wanted;
			bool entered = false;

			entered = CheckWaitingRoom(client);

			if (!entered)
			{
				Console.WriteLine("Klient " + client.name + " wychodzi.");
				ClientLeaves(out client);
			}
			else
			{
				if (lastBarber < barbers.Count - 1)
					lastBarber++;
				else
					lastBarber = 0;

				barbers[lastBarber].Haircut(client);				

				if(wantManicure)
				{
					if (lastManicurist < manicurists.Count - 1)
						lastManicurist++;
					else
						lastManicurist = 0;
					manicurists[lastManicurist].Manicure(client);
				}
			}
		}

		public void ClientLeaves(out Client client)
		{
			client = null;
		}

		public bool CheckWaitingRoom(Client client)
		{
			Console.WriteLine(client.name + "  Sprawdzam dostępność poczekalni");
			if (waiting_room.Count < waiting_room.Capacity)
			{
				Console.WriteLine(client.name + "  Wolne miejsce. Wchodzę do poczekalni.");
				waiting_room.Add(client);
				return true;
			}
			else
			{
				Console.WriteLine(client.name + "  Nie ma miejsca w poczekalni.");
				return client.WaitForRoom(this);
			}
		}

		bool CheckFreeBarbers(Client client)
		{

			return false;
		}
	}
}
