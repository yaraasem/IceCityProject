using System.Collections.Generic;

namespace IceCity_W4CC
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

