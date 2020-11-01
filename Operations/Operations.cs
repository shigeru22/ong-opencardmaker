using System.Windows.Forms;

namespace OpenCardMaker
{
    class FrontEndOperations
    {
        public static string Browse()
        {
            string location = null;

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    location = dialog.SelectedPath;
                }
            }

            return location;
        }
    }
}
