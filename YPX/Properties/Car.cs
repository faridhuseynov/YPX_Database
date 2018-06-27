using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPX
{
    [Serializable]
    public class Car
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public List<Fines> fines;
        public Car()
        {
            this.fines =new List<Fines>();
        }
        public void AddFine()
        {
            var temp = new Fines();
            Console.WriteLine("Enter the year of the fine");
            Int32.TryParse(Console.ReadLine(), out int year);
            Console.WriteLine("Enter the month of the fine");
            Int32.TryParse(Console.ReadLine(), out int month);
            Console.WriteLine("Enter the day of the fine");
            Int32.TryParse(Console.ReadLine(), out int day);
            temp.DTime = new DateTime(year, month, day);
            Console.WriteLine("Enter the type of the fine:\n1.Alcohol\t2.Red Light\t3.Overspeed");
            ConsoleKeyInfo desc = Console.ReadKey(true);
            if (desc.Key == ConsoleKey.D1 || desc.Key == ConsoleKey.NumPad1)
                temp.desc = DESCRIPTION.Alcohol;
            else if (desc.Key == ConsoleKey.D2 || desc.Key == ConsoleKey.NumPad2)
                temp.desc = DESCRIPTION.Red_Light;
            else
                temp.desc = DESCRIPTION.Overspeed;
            Console.WriteLine("Enter the street where fine was issued:");
            string street = Console.ReadLine();
            temp.Street = street;
            Console.WriteLine("Enter the amount of fine in manats:");
            int amount = Int32.Parse(Console.ReadLine());
            temp.Amount = amount;
            fines.Add(temp);
        }
        public bool RemoveFine(int index)
        {
            if (fines.Remove(fines[index])==true)
                return true;
            return false;
        }
        public override string ToString()
        {
            return $"Model: {Model}\nColor: {Color}";
        }
    }
}