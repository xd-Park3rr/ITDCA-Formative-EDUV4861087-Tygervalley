namespace SortingAlgortihm
{
    // q4_ class: quicksort implementation and metric selector
    public class q4_QuickSorter
    {
        // Metric selector (store normalized lower-case name).
        // Supported metrics: "name", "nightly_rate", "stars", "distance_from_airport" (plus a few synonyms)
        private string _metric = "stars";

        // Expose the metric being used (useful for logging/debugging)
        public string Metric => _metric;

        public q4_QuickSorter() { }

        // Provide a constructor so marker can set metric easily.
        // Input is normalized to lower-case so callers may use different casing or minor variants.
        public q4_QuickSorter(string metric)
        {
            _metric = (metric ?? _metric).ToLowerInvariant();
        }

        // Main sort method - sorts the list based on the selected metric
        public void Sort(List<q4_Hotel> items)
        {
            // Skip sorting if list is null or has 0-1 items
            if (items == null || items.Count <= 1) return;

            // Choose a comparison function based on the selected metric
            Comparison<q4_Hotel> cmp = _metric switch
            {
                // Sort by hotel name alphabetically (case-insensitive)
                "name" or "hotel_name" => (a, b) => string.Compare(a?.name ?? string.Empty, b?.name ?? string.Empty, StringComparison.OrdinalIgnoreCase),

                // Sort by nightly rate (price) - supports multiple naming variations
                "nightly_rate" or "nightlyrate" or "rate" or "price" => (a, b) => a.nightly_rate.CompareTo(b.nightly_rate),

                // Sort by star rating
                "stars" or "rating" => (a, b) => a.stars.CompareTo(b.stars),

                // Sort by distance from airport
                "distance_from_airport" or "distance" or "distancefromairport" => (a, b) => a.distance_from_airport.CompareTo(b.distance_from_airport),

                // Throw error if metric is not recognized
                _ => throw new ArgumentException($"Unknown metric: {_metric}")
            };

            // Start the recursive QuickSort algorithm
            QuickSort(items, 0, items.Count - 1, cmp);
        }

        // Recursive QuickSort using Lomuto partition scheme
        private void QuickSort(List<q4_Hotel> arr, int low, int high, Comparison<q4_Hotel> cmp)
        {
            // Only sort if there are at least 2 elements in this section
            if (low < high)
            {
                // Partition the array and get the pivot's final position
                int p = Partition(arr, low, high, cmp);
                // Recursively sort elements before the pivot
                QuickSort(arr, low, p - 1, cmp);
                // Recursively sort elements after the pivot
                QuickSort(arr, p + 1, high, cmp);
            }
        }

        // Partition method - rearranges elements around a pivot
        private int Partition(List<q4_Hotel> arr, int low, int high, Comparison<q4_Hotel> cmp)
        {
            // Choose the last element as the pivot
            var pivot = arr[high];
            // Index of the smaller element (tracks partition boundary)
            int i = low - 1;
            // Iterate through all elements except the pivot
            for (int j = low; j <= high - 1; j++)
            {
                // If current element is less than or equal to pivot
                if (cmp(arr[j], pivot) <= 0)
                {
                    i++; // Move boundary of smaller elements
                    Swap(arr, i, j); // Swap current element with boundary element
                }
            }
            // Place pivot in its correct sorted position
            Swap(arr, i + 1, high);
            // Return the pivot's final position
            return i + 1;
        }

        // Helper method to swap two elements in the list
        private void Swap(List<q4_Hotel> arr, int i, int j)
        {
            // No swap needed if indices are the same
            if (i == j) return;
            // Standard three-way swap using temporary variable
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}
