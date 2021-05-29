using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace OpenCardMaker.Operations
{
    class UserOperations
    {
        private static UserOperations instance;
        private static readonly object mutex = new object();

        /// <summary>
        /// Returns active user data operations instance.
        /// Set path before calling instance methods.
        /// </summary>
        public static UserOperations Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null) instance = new UserOperations();
                    return instance;
                }
            }
        }

        private bool isPathSpecified;
        private string _path;

        public string path
        {
            set
            {
                if (File.Exists($"{value}\\UserData.json") && File.Exists($"{value}\\UserCard.json")) _path = value;
                else throw new InvalidPathException();

                isPathSpecified = true;
            }
        }

        public UserOperations()
        {
            isPathSpecified = false;
        }

        public UserData GetUserData()
        {
            if (!isPathSpecified) throw new UnsetPathException();

            using (StreamReader reader = new StreamReader(File.Open($"{_path}\\UserData.json", FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserData>(temp);
            }
        }

        public UserCard GetUserCard()
        {
            if (!isPathSpecified) throw new UnsetPathException();

            using (StreamReader reader = new StreamReader(File.Open($"{_path}\\UserCard.json", FileMode.Open)))
            {
                string temp = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<UserCard>(temp);
            }
        }

        public int SaveUserCard(UserCard data)
        {
            if (!isPathSpecified) throw new UnsetPathException();

            // if not changed, don't save it
            UserCard temp = GetUserCard();
            if (temp.length == data.length && Enumerable.SequenceEqual(temp.userCardList, data.userCardList)) return 0;

            using (StreamWriter writer = new StreamWriter(File.Open($"{_path}\\UserCard.json", FileMode.Create)))
            {
                writer.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
                return data.length - temp.length;
            }
        }
    }
}
