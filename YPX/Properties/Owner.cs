using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPX
{
    [Serializable]
    public class Owner
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public override string ToString()
        {
            return $"Name: {Name}\nSurname: {Surname}\n\nCar:";
        }

        public Owner() { }
        public void AddCar()
        {
            Console.WriteLine("Enter the new car model:");
            string carmodel = Console.ReadLine();
            Console.WriteLine("Enter the new car color:");
            string carcolor = Console.ReadLine();
            Cars.Add(new Car { Model = carmodel, Color = carcolor });
            Console.WriteLine("Do you want to add the fine for this serial number? 1 (Yes) or 0 (No)");
            ConsoleKeyInfo cki1 = Console.ReadKey(true);
            if (cki1.Key == ConsoleKey.D1 || cki1.Key == ConsoleKey.NumPad1)
            {
                ConsoleKeyInfo cki2 = new ConsoleKeyInfo();
                while (true)
                {
                    Cars[Cars.Count - 1].AddFine();
                    Console.WriteLine("Do you want to add another fine for this number? 1 (Yes) or 0 (No)");
                    cki2 = Console.ReadKey(true);
                    if (cki2.Key == ConsoleKey.D0 || cki2.Key == ConsoleKey.NumPad0)
                        break;
                }
            }
        }
    }
}
