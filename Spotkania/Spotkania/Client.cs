using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotkania
{
	class Client : BaseThread
	{
		public bool manicure_wanted = false;
		public bool haircut_in_progress = false;
		public bool manicure_in_progress = false;
		public bool haircut_done = false;
		public bool manicure_done = false;
		public int name;
		Boss boss;

		public Client(int _name, Boss _boss)
		{
			name = _name;
			Random rand = new Random();
			int temp = rand.Next(0, 3);
			if (temp == 0)
				manicure_wanted = true;
			else
				manicure_wanted = false;

			boss = _boss;
		}

		public override void RunThread()
		{
			boss.TakeClient(this);

		}

		public bool WaitForRoom(Boss boss)
		{
			Random rand = new Random();
			int patience = rand.Next(0, 41);
			bool entered = false;

			Console.WriteLine(this.name + "  Poczekam " + patience / 2 + " minut na wolne miejsce w poczekalni");
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Elapsed += (sender, e) => 
			{
				patience--;
				if (patience > 0)
				{
					entered = HandleRoom(boss);
					if (entered == true)
					{
						Console.WriteLine(this.name + "  Zanim wyszedłem pojawiło się miejsce w poczekalni. Wchodzę do poczekalni.");
						timer.Stop();
					}
				}
				else
				{
					Console.WriteLine(this.name + "  Skończyła mi się cierpliwość. Wychodzę.");
					entered = false;
					timer.Stop();
				}
			};
			timer.Interval = 500;
			timer.Enabled = true;

			return entered;
		}

		bool HandleRoom(Boss boss)
		{
			if (boss.waiting_room.Count < boss.waiting_room.Capacity - 1)
			{
				boss.waiting_room.Add(this);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
