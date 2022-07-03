using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_project
{
    internal class Singer
    {
        public string Name { get; }
        public string Popularity { get; }
        public string Genre { get; }
        public string Country { get; }
        public int Price { get; }
        public int ConcertNumber { get; }
        public int PriceAll { get; }

        public Singer(string name, string popularity, string genre, string country, int price, int concertNum, int priceAll)
        {
            Name = name;
            Popularity = popularity;
            Genre = genre;
            Country = country;
            Price = price;
            ConcertNumber = concertNum;
            PriceAll = priceAll;
        }
    }
}
