using System.Text.Json;

namespace SortingAlgortihm
{
    public static class q4_JsonLoader
    {
        public static List<q4_Hotel> LoadFromFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("JSON data file not found", path);

            var json = File.ReadAllText(path);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                var hotels = JsonSerializer.Deserialize<List<q4_Hotel>>(json, options);
                return hotels ?? new List<q4_Hotel>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to parse JSON file", ex);
            }
        }
    }
}
