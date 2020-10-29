using System.Windows.Forms;

namespace OpenCardMaker
{
    class Operations
    {
        public static string Browse()
        {
            string location = null;

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    location = dialog.SelectedPath;
                }
            }

            return location;
        }
    }
}
