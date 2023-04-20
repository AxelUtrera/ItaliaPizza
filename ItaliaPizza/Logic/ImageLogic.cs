using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Logic
{
    public static class ImageLogic
    {
        public static BitmapImage ConvertToBitMapImage(byte[] bytesChain)
        {
            var image = new BitmapImage();
                using (var stream = new MemoryStream(bytesChain))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
            return image;
        }

        public static byte[] ConvertToBytes(BitmapImage imageToConvert)
        {
            byte[] imageConverted = null;

            if (imageToConvert != null)
            {
                BitmapSource bitmapSource = imageToConvert;

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    imageConverted = stream.ToArray();
                }
            }

            return imageConverted;
        }


        public static BitmapImage ConvertToBitMapImage(ImageSource bitmapSource)
        {
            var bitmapImage = new BitmapImage();

            var bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapSource as BitmapSource));

            using (var stream = new MemoryStream())
            {
                bitmapEncoder.Save(stream);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }
}
