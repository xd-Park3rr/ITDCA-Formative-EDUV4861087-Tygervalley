using System.Text.Json;

namespace SortingAlgortihm
{
    // q4_ class: loads hotels from a JSON file using System.Text.Json
    public static class q4_JsonLoader
    {
        // Load and deserialize hotel data from a JSON file
        public static List<q4_Hotel> LoadFromFile(string path)
        {
            // Check if the file exists before attempting to read
            if (!File.Exists(path))
                throw new FileNotFoundException("JSON data file not found", path);

            // Read the entire file content as a string
            var json = File.ReadAllText(path);
            
            // Configure JSON deserialization options
            var options = new JsonSerializerOptions
            {
                // Allow property names to match regardless of case (Name = name = NAME)
                PropertyNameCaseInsensitive = true
            };

            try
            {
                // Deserialize JSON string into a list of Hotel objects
                var hotels = JsonSerializer.Deserialize<List<q4_Hotel>>(json, options);
                // Return the deserialized list, or an empty list if null
                return hotels ?? new List<q4_Hotel>();
            }
            catch (JsonException ex)
            {
                // Wrap JSON parsing errors with a more descriptive message
                throw new InvalidOperationException("Failed to parse JSON file", ex);
            }
        }
    }
}
