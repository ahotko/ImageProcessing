using System.Drawing;
using System.Drawing.Imaging;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public void Sepia(Bitmap bitmap, int depth)
        {
            double _r;
            double _g;
            double _b;

            GreyScale(GreyScaleType.Average);

            unsafe
            {
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)bitmapData.Scan0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        _b = currentLine[x];
                        _g = currentLine[x + 1] + depth;
                        _r = currentLine[x + 2] + 2.0 * depth;
                        if (_r <= (depth * 2 - 1)) _r = 255.0;
                        if (_g <= (depth - 1)) _g = 255.0;
                        currentLine[x] = Clamp(_b);
                        currentLine[x + 1] = Clamp(_g);
                        currentLine[x + 2] = Clamp(_r);
                    }
                }
                bitmap.UnlockBits(bitmapData);
            }
        }

        public void Sepia()
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
                        _b = currentLine[x];
                        _g = currentLine[x + 1];
                        _r = currentLine[x + 2];
                        currentLine[x] = Clamp(_r * 0.272 + _g * 0.534 + _b * 0.131);
                        currentLine[x + 1] = Clamp(_r * 0.349 + _g * 0.686 + _b * 0.168);
                        currentLine[x + 2] = Clamp(_r * 0.393 + _g * 0.769 + _b * 0.189);
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }

        public void Sepia2(int depth)
        {
            double _b;
            var _rgb = new RgbColorSpace();

            GreyScale(GreyScaleType.Average);

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
                        _b = currentLine[x];
                        _rgb.FromHsl(20.0, _b/255.0, depth/100.0);
                        currentLine[x] = _rgb.B;
                        currentLine[x + 1] = _rgb.G;
                        currentLine[x + 2] = _rgb.R;
                    }
                }
                Image.UnlockBits(bitmapData);
            }
        }
    }
}
