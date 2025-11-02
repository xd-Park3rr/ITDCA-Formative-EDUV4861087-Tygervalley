// q4: Sorting for Booking Agent
// This program reads a JSON file of hotels, sorts it by a metric (change metric in q4_QuickSorter),
// and prints the hotel names in sorted order.

string dataPath = Path.Combine(AppContext.BaseDirectory, "data", "hotels.json");


var hotels = q4_JsonLoader.LoadFromFile(dataPath);

// Change the metric here by editing the string passed to q4_QuickSorter (e.g., "name", "nightly_rate", "stars", "distance_from_airport").
// The marker will manually change this value when testing.
var sorter = new q4_QuickSorter("nightly_rate");

sorter.Sort(hotels);

Console.WriteLine($"Sorted by metric: nightly_rate");
foreach (var h in hotels)
{
	Console.WriteLine(h.name);
}


