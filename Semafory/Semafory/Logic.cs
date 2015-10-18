using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semafory
{
    public class Logic
    {
        public int temp = -1;
        public int indexY = -1;
        public int indexX = -1;
        public List<int> x = new List<int>();
        public List<int> y = new List<int>();
        public bool stop = false;

        public void InsertSize()
        {
            Console.WriteLine("Wpisz rozmiar ciągu x: ");
            var tempSizeX = Console.ReadLine();
            int sizeX = int.Parse(tempSizeX.ToString());

            Console.WriteLine("Wpisz rozmiar ciągu y: ");
            var tempSizeY = Console.ReadLine();
            int sizeY = int.Parse(tempSizeY.ToString());
            Random rand = new Random();


            for (int i = 0; i < sizeX; i++)
            {
                int temp = rand.Next(1, 100);
                x.Add(temp);
            }

            for (int i = 0; i < sizeY; i++)
            {
                int temp = rand.Next(1, 100);
                y.Add(temp);
            }
        }

        public void ShowLists()
        {
            Console.WriteLine("Czy chcesz wyświetlić listy? (y/n)");

            string input = Console.ReadLine();
            if (input == "y" || input == "yes" || input == "Y")
            {
                Console.WriteLine("Ciąg x:");
                foreach (int val in x)
                {
                    Console.WriteLine(val);
                }

                Console.WriteLine("");

                Console.WriteLine("Ciąg y:");

                foreach (int val in y)
                {
                    Console.WriteLine(val);
                }
            }
        }

        public int FindOddNumberInY()
        {
            for (int i = 0; i < y.Count; i++)
            {
                if (y[i] % 2 != 0)
                {
                    if (indexX == -1 && temp == -1)
                    {
                        indexY = i;
                        return y[i];
                    }
                    else if(indexX != -1 && temp != -1)
                    {
                        SwapFromYtoX(y[i], i);
                    }
                }
            }
            stop = true;
            return -1;
        }

        public int FindEvenNumberInX()
        {
            for (int i = 0; i < x.Count; i++)
            {
                if (x[i] % 2 == 0)
                {
                    if (indexY == -1 && temp == -1)
                    {
                        indexX = i;
                        return x[i];
                    }
                    else if(indexY != -1 && temp != -1)
                    {
                        SwapFromXtoY(x[i], i);
                    }
                }
            }
            stop = true;

            return -1;
        }

        public void SwapFromYtoX(int currentY, int currentYindex)
        {
            Console.WriteLine("Swapping y[" + currentYindex + "] value (" + currentY + ") with " + x[indexX]);

            x[indexX] = currentY;
            y[currentYindex] = temp;
            temp = -1;
            indexX = -1;
        }

        public void SwapFromXtoY(int currentX, int currentXindex)
        {
            Console.WriteLine("Swapping x[" + currentXindex + "] value (" + currentX + ") with " + y[indexY]);

            y[indexY] = currentX;
            x[currentXindex] = temp;
            temp = -1;
            indexY = -1;
        }
    }
}
