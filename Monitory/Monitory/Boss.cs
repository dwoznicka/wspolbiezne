using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monitory
{
    class Boss
    {
        List<Barber> barbers = new List<Barber>();
        List<Manicurist> manicurists = new List<Manicurist>();
        public List<Client> waiting_room;
        public List<Client> clients_at_barber = new List<Client>();
        public List<Client> clients_at_manicurist = new List<Client>();

        public Boss(int barbersCount, int manicuristsCount, int waitingRoomCapacity)
        {
            for (int i = 0; i < barbersCount; i++)
            {
                barbers.Add(new Barber(i));
            }

            for (int i = 0; i < manicuristsCount; i++)
            {
                manicurists.Add(new Manicurist(i));
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

            CheckClientsForManicure();
            CheckClientsForBarber();
        }

        public void ClientLeaves(out Client client)
        {
            client = null;
        }

        void CheckBarbers(Client client)
        {
            bool freeBarber = false;
            Console.WriteLine("Szukam fryzjera dla klienta " + client.name);
            foreach(Barber b in barbers)
            {
                Console.WriteLine("Sprawdzam fryzjera " + b.name);
                if (Monitor.TryEnter(b))
                {
                    Console.WriteLine("Fryzjer " + b.name + " wolny. Wchodzi do monitora");
                    waiting_room.Remove(client);
                    clients_at_barber.Add(client);
                    freeBarber = true;
                    try
                    {
                        client.Haircut(b);
                        Monitor.Pulse(b);
                    }
                    finally
                    {
                        Monitor.Exit(b);
                        Console.WriteLine("Fryzjer " + b.name + " opuszcza monitor.");
                        clients_at_barber.Remove(client);

                        if(!client.manicure_wanted)
                        {
                            Console.WriteLine("Klient " + client.name + " wychodzi.");
                            ClientLeaves(out client);
                        }
                        else if(client.manicure_wanted && !client.manicure_done)
                        {
                            CheckWaitingRoom(client);
                        }
                        CheckClientsForBarber();
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Fryzjer " + b.name + " zajęty.");
                }
            }

            if (!freeBarber)
            {
                Console.WriteLine("Brak wolnych fryzjerów");
            }
        }

        void CheckManicurist(Client client)
        {
            bool freeManicurist = false;
            Console.WriteLine("Szukam manikurzystki dla klienta " + client.name);

            foreach (Manicurist m in manicurists)
            {
                if (Monitor.TryEnter(m))
                {
                    Console.WriteLine("Manikurzystka " + m.name + " wolna. Wchodzi do monitora");
                    clients_at_manicurist.Add(client);
                    waiting_room.Remove(client);
                    freeManicurist = true;
                    try
                    {
                        client.Manicure(m);
                        Monitor.Pulse(m);
                    }
                    finally
                    {
                        Monitor.Exit(m);
                        Console.WriteLine("Manikurzystka " + m.name + " opuszcza monitor.");
                        clients_at_manicurist.Remove(client);

                        if (client.haircut_done)
                        {
                            Console.WriteLine("Klient " + client.name + " wychodzi.");
                            ClientLeaves(out client);
                        }
                        else
                        {
                            CheckWaitingRoom(client);
                        }
                        CheckClientsForManicure();

                    }
                    break;
                }
            }

            if (!freeManicurist)
            {
                Console.WriteLine("Brak wolnych manikurzystek");
            }
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
                Console.WriteLine(client.name + "  Nie ma miejsca w poczekalni." );
                return client.WaitForRoom(this);
            }
        }

        void CheckClientsForManicure()
        {
            Console.WriteLine("Kierownik sprawdza czy przekierować któregoś z klientów do manikurzystki.");
            if (clients_at_barber.Count > 0)
            {
                foreach(Client c in clients_at_barber)
                {
                    if(c.manicure_wanted && !c.manicure_done && !c.manicure_in_progress)
                    {
                        Console.WriteLine("Klient " + c.name + " jest w trakcie strzyżenia i chce otrzymać manicure.");
                        CheckManicurist(c);
                        break;
                    }
                }
            }
            if(waiting_room.Count > 0)
            {
                foreach (Client c in waiting_room)
                {
                    if (c.manicure_wanted && !c.manicure_done)
                    {
                        Console.WriteLine("Klient " + c.name + " jest w poczekalni i chce otrzymać manicure.");
                        CheckManicurist(c);
                        break;
                    }
                }
            }
        }

        void CheckClientsForBarber()
        {
            Console.WriteLine("Kierownik sprawdza czy przekierować któregoś z klientów do fryzjera.");
            if(clients_at_manicurist.Count > 0)
            {
                foreach(Client c in clients_at_manicurist)
                {
                    if(!c.haircut_done && !c.haircut_in_progress)
                    {
                        Console.WriteLine("Klient " + c.name + " jest w trakcie manicure i potrzebuje strzyżenia.");
                        CheckBarbers(c);
                        break;
                    }
                }
            }
            if (waiting_room.Count > 0)
            {
                foreach (Client c in waiting_room)
                {
                    if (!c.haircut_done)
                    {
                        Console.WriteLine("Klient " + c.name + " jest w poczekalni i potrzebuje strzyżenia.");
                        CheckBarbers(c);
                        break;
                    }
                }
            }
        }
    }
}
