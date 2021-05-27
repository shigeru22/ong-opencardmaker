using AssetsTools.NET.Extra;
using OpenCardMaker.Operations.AssetsTools;
using System.Drawing;
using System.IO;

namespace OpenCardMaker.Operations
{
    public class CardAssetInstance
    {
        AssetsManager manager;
        readonly string path;

        /// <summary>
        /// Creates a card assets instance. Must be instantiated.
        /// </summary>
        /// <param name="path">Path to main application.</param>
        public CardAssetInstance(string path)
        {
            if (File.Exists($"{path}\\mu3.exe")) this.path = $"{path}\\mu3_Data\\StreamingAssets\\assets";
            else if (File.Exists($"{path}\\package\\mu3.exe")) this.path = $"{path}\\package\\mu3_Data\\StreamingAssets\\assets";
            else throw new InvalidPathException();

            manager = new AssetsManager();
        }

        /// <summary>
        /// Returns image data from bundle with specified card ID.
        /// </summary>
        /// <param name="cardId">Card ID.</param>
        /// <returns></returns>
        public Bitmap GetImage(string cardId)
        {
            return BitmapExtractor.GetBitmap(path, cardId);
        }
    }
}
