using System.IO;

namespace OpenCardMaker.Operations
{
    class FileChecker
    {
        public static bool OngekiFolderCheck(string path)
        {
            if (Directory.Exists($"{path}\\package")) path += "\\package";
            if (Directory.Exists($"{path}\\mu3_Data") && File.Exists($"{path}\\mu3.exe")) return true;

            return false;
        }

        public static bool ConfigFolderCheck(string path)
        {
            if (File.Exists($"{path}\\UserCard.json") && File.Exists($"{path}\\UserData.json")) return true;

            return false;
        }
    }
}
