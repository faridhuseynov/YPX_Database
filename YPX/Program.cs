using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace YPX
{
    class Program
    {
        static Dictionary<string, Owner> database = new Dictionary<string, Owner>();
        static void ScreenPrint()
        {
            Console.WriteLine($"1.Show Database\n2.Add new serial number\n3.Search by car serial number\n" +
                $"4.Add fine\n5.Remove fine\n6.Fines by Date\n7.Statistics\n8.Save/Load database\n9.Exit");
        }
        static void AddNewSerial()
        {
            Owner temp = new Owner();
            Console.WriteLine("Enter the car serial number:");
            string serial = Console.ReadLine();
            serial = serial.ToUpper();
            Console.WriteLine("Enter the name of the owner:");
            temp.Name = Console.ReadLine();
            Console.WriteLine("Enter the surname of the owner:");
            temp.Surname = Console.ReadLine();
            temp.AddCar();
            database.Add(serial, temp);
        }
        static void Print(string serial, Owner owner)
        {

            Console.WriteLine($"Serial number:{serial}\n{owner.ToString()}");
            for (int i = 0; i <owner.Cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}.");
                Console.WriteLine($"{owner.Cars[i].ToString()}\n");
                Console.WriteLine("Fines:");
                for (int j = 0; j < owner.Cars[i].fines.Count; j++)
                {
                    Console.WriteLine($"{j + 1}.");
                    Console.WriteLine(owner.Cars[i].fines[j]);
                }
            }
        }
        static Owner Search(string serial)
        {
            if (database.TryGetValue(serial, out Owner find) == true)
                return find;
            else
                return null;
        }
        static void SaveLoad() {
            Console.WriteLine("Press S to save or L to load the database");
            BinaryFormatter formatter = new BinaryFormatter();
            ConsoleKeyInfo choice = Console.ReadKey(true);
            string file;
            if (choice.Key == ConsoleKey.S)
            {
                Console.WriteLine("Enter the name of the database");
                file = Console.ReadLine();
                using (FileStream fs = new FileStream($"{file}.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, database);
                }
                Console.WriteLine("The file was successfully saved");
            }
            else if (choice.Key == ConsoleKey.L)
            {
                Console.WriteLine("Enter the name of the database");
                file = Console.ReadLine();
                using (FileStream fs = new FileStream($"{file}.dat", FileMode.OpenOrCreate))
                {
                    database = (Dictionary<string, Owner>)formatter.Deserialize(fs);
                    Console.WriteLine("Database was successfully loaded");
                }
            }
        }
        static void FineByDate(DateTime dateTime)
        {
            foreach (var item in database)
            {
                    for (int i = 0; i < item.Value.Cars.Count; i++)
                    {
                        for (int j = 0; j < item.Value.Cars[i].fines.Count; j++)
                        {
                            if (item.Value.Cars[i].fines[j].DTime==dateTime)
                            {
                                Console.WriteLine($"Serial number:{item.Key.ToString()}");
                                Console.WriteLine(item.Value.Cars[i].ToString());
                                Console.WriteLine(item.Value.Cars[i].fines[j].ToString());
                            }
                        }
                    }
            }
        }
        static void Statistics()
        {
            int alcohol_count=0;
            int redlight_count = 0;
            int overspeed_count=0;

            foreach (var item in database)
            {
                for (int i = 0; i < item.Value.Cars.Count; i++)
                {
                    for (int j = 0; j < item.Value.Cars[i].fines.Count; j++)
                    {
                        if (item.Value.Cars[i].fines[j].desc.ToString()=="Alcohol")
                            ++alcohol_count;
                        else if (item.Value.Cars[i].fines[j].desc.ToString() == "Red_Light")
                            ++redlight_count;
                        else if (item.Value.Cars[i].fines[j].desc.ToString() == "Overspeed")
                            ++overspeed_count;
                    }
                }
            }
            Console.WriteLine($"\t\tSTATISTICS\nAlcohol\t\tRed Light\tOverspeed\n   {alcohol_count}\t\t    {redlight_count}\t\t    {overspeed_count}");
        }
        static void Main(string[] args)
        {
            ConsoleKeyInfo choice = new ConsoleKeyInfo();
            while (choice.Key != ConsoleKey.NumPad9 && choice.Key != ConsoleKey.D9)
            {
                Console.Clear();
                ScreenPrint();
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.NumPad1 || choice.Key == ConsoleKey.D1)
                {
                    foreach (var item in database)
                    {
                        Print(item.Key, item.Value);

                    }
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad2 || choice.Key == ConsoleKey.D2)
                {
                    AddNewSerial();
                }
                else if (choice.Key == ConsoleKey.NumPad3 || choice.Key == ConsoleKey.D3)
                {
                    Console.WriteLine("Enter the car serial number");
                    string serial = Console.ReadLine();
                    serial = serial.ToUpper();
                    var find = Search(serial);
                    if (find != null)
                        Print(serial, find);
                    else
                        Console.WriteLine("The serial number doesn't exist in database");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad4 || choice.Key == ConsoleKey.D4)//delete fine
                {
                    Console.WriteLine("Enter the car serial number");
                    string serial = Console.ReadLine();
                    serial = serial.ToUpper();
                    var find = Search(serial);
                    if (find != null)
                    {
                        Print(serial, find);
                        Console.WriteLine("Select the car index:");
                        int index_car = Int32.Parse(Console.ReadLine());
                        database[serial].Cars[index_car - 1].AddFine();
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                else if (choice.Key == ConsoleKey.NumPad5 || choice.Key == ConsoleKey.D5)//delete fine
                {
                    Console.WriteLine("Enter the car serial number");
                    string serial = Console.ReadLine();
                    serial = serial.ToUpper();
                    var find = Search(serial);
                    if (find != null)
                    {
                        Print(serial, find);
                        Console.WriteLine("Select the car index:");
                        int index_car = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Select the fine index you want to delete:");
                        int index_fine = Int32.Parse(Console.ReadLine());
                        if (database[serial].Cars[index_car - 1].RemoveFine(index_fine - 1) == true)
                            Console.WriteLine($"Fine {index_fine} successfully removed");
                        else
                            Console.WriteLine($"Car index &/or fine index is wrong");
                    }
                    else
                        Console.Write("Car with this serial number not found\nPress any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad6 || choice.Key == ConsoleKey.D6)
                {
                    Console.WriteLine("Enter the year of the fine");
                    Int32.TryParse(Console.ReadLine(), out int year);
                    Console.WriteLine("Enter the month of the fine");
                    Int32.TryParse(Console.ReadLine(), out int month);
                    Console.WriteLine("Enter the day of the fine");
                    Int32.TryParse(Console.ReadLine(), out int day);
                    var check_date = new DateTime(year, month, day);
                    FineByDate(check_date);
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad7 || choice.Key == ConsoleKey.D7)
                {
                    Statistics();
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
                else if (choice.Key == ConsoleKey.NumPad8 || choice.Key == ConsoleKey.D8)//serialization
                {
                    SaveLoad();
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}