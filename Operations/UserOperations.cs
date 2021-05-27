using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace OpenCardMaker.Operations
{
    class UserOperations
    {
        string path;

        /// <summary>
        /// Creates a UserOperations instance. Must be instantiated.
        /// </summary>
        /// <param name="configPath"></param>
        public UserOperations(string configPath)
        {
            if (File.Exists($"{configPath}\\UserData.json") && File.Exists($"{configPath}\\UserCard.json")) path = configPath;
            else throw new InvalidPathException();
        }

        public UserData GetUserData()
        {
            using (StreamReader reader = new StreamReader(File.Open($"{path}\\UserData.json", FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserData>(temp);
            }
        }

        public UserCard GetUserCard()
        {
            using (StreamReader reader = new StreamReader(File.Open($"{path}\\UserCard.json", FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserCard>(temp);
            }
        }

        public int SaveUserCard(UserCard data)
        {
            // if not changed, don't save it
            UserCard temp = GetUserCard();
            if (temp.length == data.length && Enumerable.SequenceEqual(temp.userCardList, data.userCardList)) return 0;

            using (StreamWriter writer = new StreamWriter(File.Open($"{path}\\UserCard.json", FileMode.Create)))
            {
                writer.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
                return data.length - temp.length;
            }
        }
    }
}
