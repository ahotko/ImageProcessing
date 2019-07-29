using System.Drawing;
using System.Drawing.Imaging;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public enum ImageAlphaBlendMode
        {
            Crop,
            Repeat,
            Resize
        }

        public void AlphaBlend(Color color)
        {
            AlphaBlend(color.R, color.G, color.B, 1.0 * color.A / 255.0);
        }

        public void AlphaBlend(Color color, double alphaValue = 0.5)
        {
            AlphaBlend(color.R, color.G, color.B, alphaValue);
        }

        public void AlphaBlend(byte r = 255, byte g = 255, byte b = 255, double alphaValue = 0.5)
        {
            double _r;
            double _g;
            double _b;

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
                        _b = alphaValue * (currentLine[x] - b) + b;
                        _g = alphaValue * (currentLine[x + 1] - g) + g;
                        _r = alphaValue * (currentLine[x + 2] - r) + r;
                        currentLine[x] = Clamp(_b);
                        currentLine[x + 1] = Clamp(_g);
                        currentLine[x + 2] = Clamp(_r);
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }
    }
}
