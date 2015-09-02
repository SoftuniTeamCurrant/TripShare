namespace TripShare.Client.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TripShare.Data;
    using TripShare.Models;

    public class ConsoleClient
    {
        static void Main()
        {
            var data = new TripShareData();

            var cities = data.Cities
                .All()
                .Select(c => c.CityName)
                .ToList();

            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }
    }
}
