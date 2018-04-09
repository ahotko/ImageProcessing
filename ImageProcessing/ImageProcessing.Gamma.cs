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
        public void GammaCorrection(double gamma = 0.5)
        {
            Dictionary<int, byte> _gammaMap = new Dictionary<int, byte>();
            for(int i=0; i<256; i++)
            {
                double _gammaValue = 255.0 * Math.Exp(Math.Log(1.0 * i / 255.0) / gamma);
                _gammaMap.Add(i, Clamp(_gammaValue));
            }
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
                        currentLine[x] = _gammaMap[currentLine[x]];
                        currentLine[x + 1] = _gammaMap[currentLine[x+1]];
                        currentLine[x + 2] = _gammaMap[currentLine[x+2]];
                    }
                }
                _bitmap.UnlockBits(bitmapData);
            }
        }
    }
}
