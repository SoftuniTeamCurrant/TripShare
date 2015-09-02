using System.Runtime.CompilerServices;

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
                .Select(c => c.Name)
                .ToList();

            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }

            var trip = data.Trips.Find(1);

            Console.WriteLine("Ot" + " " + trip.DepartureCity.Name + " do " + trip.ArrivalCity.Name + " na {0:dd/MM}",
                trip.DepartureDate);
        }
    }
}
