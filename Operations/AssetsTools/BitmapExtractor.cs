using AssetsTools.NET;
using AssetsTools.NET.Extra;
using OpenCardMaker.Operations.Interop;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenCardMaker.Operations.AssetsTools
{
    public static class BitmapExtractor
    {
        public static Bitmap GetBitmap(string assetsPath, string cardId)
        {
            var inst = Manager.Instance.manager.LoadAssetsFileFromBundle(Manager.Instance.manager.LoadBundleFile($"{assetsPath}\\ui_card_{cardId}"), 0);
            var table = inst.table;
            var atvf = Manager.Instance.manager.GetTypeInstance(inst.file, table.GetAssetInfo(Path.GetFileName($"UI_Card_{cardId}"))).GetBaseField();
            var texFile = TextureFile.ReadTextureFile(atvf);
            var texData = texFile.GetTextureData(inst);

            if (texData != null && texData.Length > 0)
            {
                Bitmap temp = new Bitmap(
                    texFile.m_Width,
                    texFile.m_Height,
                    texFile.m_Width * 4,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                    Marshal.UnsafeAddrOfPinnedArrayElement(texData, 0));
                temp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                return temp;
            }
            else throw new InvalidPathException();
        }

        public static ImageSource BitmapToImageSource(Bitmap image)
        {
            var handle = image.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { ObjectManipulator.DeleteObject(handle); }
        }
    }
}
