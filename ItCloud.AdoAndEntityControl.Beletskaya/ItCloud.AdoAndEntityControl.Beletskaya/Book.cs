using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCloud.AdoAndEntityControl.Beletskaya
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }

        public int? UserId { get; set; }
        
        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Author: {Author}; Publisher: {Publisher}; Year: {Year}; UserId: {UserId}";
        }
    }
}
