using System.Text.Json;

namespace YourNamespace.Utilities
{
    public static class JsonConsolePrinter
    {
        public static void PrintAsJson<T>(T obj)
        {
            string jsonString = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonString);
        }
    }
}
