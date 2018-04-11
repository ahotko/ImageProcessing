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
        public enum GreyScaleType
        {
            Normal,
            Average,
            ContemporaryPhosphors,
            BWTV
        }

        public void GreyScale(GreyScaleType greyScaleType = GreyScaleType.Normal)
        {
            double _r;
            double _g;
            double _b;
            switch (greyScaleType)
            {
                case GreyScaleType.Average:
                    _r = 0.33333;
                    _g = 0.33333;
                    _b = 0.33333;
                    break;
                case GreyScaleType.ContemporaryPhosphors:
                    _r = 0.2127;
                    _g = 0.7152;
                    _b = 0.0721;
                    break;
                case GreyScaleType.BWTV:
                    _r = 0.299;
                    _g = 0.587;
                    _b = 0.114;
                    break;
                case GreyScaleType.Normal:
                default:
                    _r = 0.33;
                    _g = 0.56;
                    _b = 0.11;
                    break;
            }
            unsafe
            {
                BitmapData bitmapData = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height), ImageLockMode.ReadWrite, Image.PixelFormat);
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(Image.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        byte _gs = (byte)Math.Round(currentLine[x] * _b + currentLine[x + 1] * _g + currentLine[x + 2] * _r);
                        currentLine[x] = _gs;
                        currentLine[x + 1] = _gs;
                        currentLine[x + 2] = _gs;
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }
    }
}
