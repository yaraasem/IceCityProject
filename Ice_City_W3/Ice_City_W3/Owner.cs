using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{
    public class Owner
    {
        private List<House> houses = new List<House>();

        public string Name { get; set; }

        public Owner(string name)
        {
            Name = name; 
        }

        public void AddHouse(House house)
        {
            houses.Add(house);
        }
        public List<House> GetHouses()
        {
            return houses; 
        }
        public int HouseCount(List<House> houses)
        {
            return houses.Count;
        }
    }
}
