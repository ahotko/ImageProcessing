using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public void Negative()
        {
            unsafe
            {
                BitmapData bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
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
                _bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
