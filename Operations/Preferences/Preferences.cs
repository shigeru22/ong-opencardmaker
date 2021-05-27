using Newtonsoft.Json;
using System.IO;

namespace OpenCardMaker.Operations.Preferences
{
    class Preferences
    {
        public static bool SavePreferences(PathSettings contents)
        {
            string path = Directory.GetCurrentDirectory();

            if (!Directory.Exists($"{path}\\config")) Directory.CreateDirectory($"{path}\\config");
            using (StreamWriter writer = new StreamWriter(File.Open($"{path}\\config\\path.json", FileMode.Create)))
            {
                writer.WriteLine(JsonConvert.SerializeObject(contents, Formatting.Indented));
                return true;
            }
        }

        public static PathSettings LoadPreferences()
        {
            string path = Directory.GetCurrentDirectory();

            if (!Directory.Exists($"{path}\\config")) Directory.CreateDirectory($"{path}\\config");
            if (!File.Exists($"{path}\\config\\path.json"))
            {
                PathSettings temp = new PathSettings
                {
                    ongeki = "",
                    config = ""
                };

                using (StreamWriter writer = new StreamWriter(File.Open($"{path}\\config\\path.json", FileMode.Create)))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(temp, Formatting.Indented));
                }
            }

            using (StreamReader reader = new StreamReader(File.Open($"{path}\\config\\path.json", FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<PathSettings>(temp);
            }
        }

        public static PathSettings LoadPreferences(string path)
        {
            using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<PathSettings>(temp);
            }
        }
    }
}
