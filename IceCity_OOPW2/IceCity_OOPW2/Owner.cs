using System;

namespace IceCity_OOPW2
{
    internal class Owner
    {
        private string name;
        private House[] houses;
        private int houseCount;

        public Owner(string name, int maxHouses)
        {
            this.name = name;
            houses = new House[maxHouses];
            houseCount = 0;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool AddHouse(House house)
        {
            if (houseCount < houses.Length)
            {
                houses[houseCount] = house;
                houseCount++;
                return true;
            }
            return false;
        }

        public House[] GetHouses()
        {
            return houses;
        }

        public int GetHouseCount()
        {
            return houseCount;
        }
    }
}
