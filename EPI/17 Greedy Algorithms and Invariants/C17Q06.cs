using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPI.C17_Greedy_Algorithms_and_Invariants
{
    public class Q06
    {
        public static City FindAmpleCity(City[] cities, int milesPerGallon)
        {
            int surplusAfterRefuel = 0, surplusBeforeRefuel = 0, minSurplusBeforeRefuel = 0, minIndex = 0;
            int totalSurplus = 0;

            for (int i = 0; i < cities.Length; i++)
            {
                if (i > 0)
                {
                    surplusBeforeRefuel = surplusAfterRefuel - cities[i - 1].DistanceToNextCity;
                    if (surplusBeforeRefuel < minSurplusBeforeRefuel)
                    {
                        minSurplusBeforeRefuel = surplusBeforeRefuel;
                        minIndex = i;
                    }
                }
                surplusAfterRefuel = cities[i].GasGallons * milesPerGallon + surplusBeforeRefuel;
                totalSurplus += surplusAfterRefuel + cities[i].DistanceToNextCity;
            }
            if (totalSurplus < 0)
                return null;
            return cities[minIndex];
        }

    }

    public class City
    {
        public string Name { get; }
        public int GasGallons { get; }
        public int DistanceToNextCity { get; }
        public City(string name, int gas, int distance)
        {
            Name = name;
            GasGallons = gas;
            DistanceToNextCity = distance;
        }
    }

    [TestClass]
    public class C17Q06_Tests
    {
        private City[] exampleCities;
        public C17Q06_Tests()
        {
            exampleCities = new City[] {
                new City("A", 50, 900),
                new City("B", 20, 600),
                new City("C", 05, 200),
                new City("D", 30, 400),
                new City("E", 25, 600),
                new City("F", 10, 200),
                new City("G", 10, 100),
            };
        }

        [TestMethod]
        public void Example()
        {
            Assert.AreEqual("D", Q06.FindAmpleCity(exampleCities, 20)?.Name);
        }
    }
}
