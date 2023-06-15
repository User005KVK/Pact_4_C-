using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace money
{
    internal class Note
    {
        public Note(string name, string type, int price, DateTime date)
        {
            Name = name;
            Type = type;
            Price = price;
            Date = date;
            Accounting = price > 0;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public int Price { get; private set; }
        public bool Accounting { get; private set; }
        public DateTime Date { get; set; }
    }
}