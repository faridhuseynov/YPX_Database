using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPX
{
    public enum DESCRIPTION { Alcohol = 1, Red_Light = 2, Overspeed = 3 }
    [Serializable]
    public class Fines
    {
        public DESCRIPTION desc; 
        public DateTime DTime { get; set; } = new DateTime();
        public string Street { get; set; }
        public int Amount { get; set; }
        public override string ToString()
        {
            return $"Fine Date(Day/Month/Year): {DTime.Day}/{DTime.Month}/{DTime.Year}\n" +
                $"Street: {Street}\n" +
                $"Description: {desc.ToString()}\n" +
                $"Amount: {Amount}AZN\n";
        }
        public Fines() { }
    }
}
