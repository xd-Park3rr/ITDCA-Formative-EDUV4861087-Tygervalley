using System;

namespace SortingAlgortihm.Properties
{
    // q4_ class: represents a hotel record loaded from JSON
    public class q4_Hotel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public double nightly_rate { get; set; }
        public int stars { get; set; }
        public double distance_from_airport { get; set; }

        public override string ToString()
        {
            return $"{name} (rate: {nightly_rate}, stars: {stars}, distance: {distance_from_airport})";
        }
    }
}
