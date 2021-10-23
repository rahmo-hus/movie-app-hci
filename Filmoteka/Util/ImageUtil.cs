using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Filmoteka.Util
{
    class ImageUtil
    {
        #region Byte To Bitmap converter
        public static BitmapImage ConvertByteArrToBitmap(byte[] arr)
        {
            BitmapImage bitmapImage = new();
            if (arr != null)
            {
                using MemoryStream ms = new(arr);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
            return null;

        }
        #endregion

        #region Bitmap to Byte Arr Converter
        public static byte[] ConvertBitmapToByteArr(BitmapImage image)
        {
            JpegBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using MemoryStream ms = new MemoryStream();
            encoder.Save(ms);
            return ms.ToArray();
        }
        #endregion
    }
}
