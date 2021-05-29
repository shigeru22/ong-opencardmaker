using AssetsTools.NET.Extra;

namespace OpenCardMaker.Operations.AssetsTools
{
    public class Manager
    {
        private static Manager instance;
        private static readonly object mutex = new object();

        /// <summary>
        /// Returns active manager instance.
        /// </summary>
        public static Manager Instance
        {
            get
            {
                lock (mutex)
                {
                    if (instance == null) instance = new Manager();
                    return instance;
                }
            }
        }

        public AssetsManager manager;

        public Manager()
        {
            manager = new AssetsManager();
        }
    }
}
