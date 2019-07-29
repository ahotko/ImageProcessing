using System.Drawing;
using System.Drawing.Imaging;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public void Negative()
        {
            unsafe
            {
                BitmapData bitmapData = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadWrite, Image.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(Image.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        currentLine[x] = (byte)(255 - currentLine[x]);
                        currentLine[x + 1] = (byte)(255 - currentLine[x + 1]);
                        currentLine[x + 2] = (byte)(255 - currentLine[x + 2]);
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }
    }
}
