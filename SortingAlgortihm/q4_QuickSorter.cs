namespace SortingAlgortihm
{
    // q4_ class: quicksort implementation and metric selector
    public class q4_QuickSorter
    {
        // Marker can change this string in code to a different metric: name, nightly_rate, stars, distance_from_airport
        private string _metric = "name";

        public q4_QuickSorter() { }

        // Provide a constructor so marker can set metric easily
        public q4_QuickSorter(string metric)
        {
            _metric = metric;
        }

        public void Sort(List<q4_Hotel> items)
        {
            if (items == null || items.Count <= 1) return;

            // choose a key selector based on metric
            Comparison<q4_Hotel> cmp = _metric switch
            {
                "name" => (a, b) => string.Compare(a?.name ?? string.Empty, b?.name ?? string.Empty, StringComparison.OrdinalIgnoreCase),
                "nightly_rate" => (a, b) => a.nightly_rate.CompareTo(b.nightly_rate),
                "stars" => (a, b) => a.stars.CompareTo(b.stars),
                "distance_from_airport" => (a, b) => a.distance_from_airport.CompareTo(b.distance_from_airport),
                _ => throw new ArgumentException($"Unknown metric: {_metric}")
            };

            QuickSort(items, 0, items.Count - 1, cmp);
        }

        // In-place QuickSort using Lomuto partition scheme
        private void QuickSort(List<q4_Hotel> arr, int low, int high, Comparison<q4_Hotel> cmp)
        {
            if (low < high)
            {
                int p = Partition(arr, low, high, cmp);
                QuickSort(arr, low, p - 1, cmp);
                QuickSort(arr, p + 1, high, cmp);
            }
        }

        private int Partition(List<q4_Hotel> arr, int low, int high, Comparison<q4_Hotel> cmp)
        {
            // choose last element as pivot
            var pivot = arr[high];
            int i = low - 1;
            for (int j = low; j <= high - 1; j++)
            {
                if (cmp(arr[j], pivot) <= 0)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }

        private void Swap(List<q4_Hotel> arr, int i, int j)
        {
            if (i == j) return;
            var tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}
