using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public void Threshold(byte threshold = 128, byte lowValue = 0, byte hiValue = 255)
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
                        byte _i = (byte)((currentLine[x] + currentLine[x + 1] + currentLine[x + 2]) / 3);
                        byte _value = (_i <= threshold) ? lowValue : hiValue;
                        currentLine[x] = _value;
                        currentLine[x + 1] = _value;
                        currentLine[x + 2] = _value;
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }
    }
}
