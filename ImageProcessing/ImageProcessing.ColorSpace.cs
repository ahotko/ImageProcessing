using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public partial class ImageProcessing
    {
        public enum ColorSpace
        {
            RGB,
            CMYK,
            HSV,
            HSL,
            YCbCr
        }

        public class RgbColorSpace
        {
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }

            public RgbColorSpace():this(0,0,0) { }

            public RgbColorSpace(byte r, byte g, byte b)
            {
                R = r;
                G = g;
                B = b;
            }

            private byte Clamp(double value)
            {
                int _result = (int)Math.Round(value);
                if (_result < 0)
                    return 0;
                else if (_result > 255)
                    return 255;
                else
                    return (byte)_result;
            }

            private double ColorBound(double p, double q, double c)
            {
                double _result = c;
                if (_result < 0.0)
                {
                    _result += 1.0;
                }
                else if (_result > 1.0)
                {
                    _result -= 1.0;
                }
                if (_result > 0.0 && _result < 0.1666666667) _result = p + ((q - p) * 6 * c);
                else if (_result >= 0.1666666667 && _result < 0.5) _result = q;
                else if (_result >= 0.5 && _result < 0.666666667) _result = p + ((q - p) * 6.0 * (2.0 / 3.0 - c));
                else _result = p;
                return _result;
            }

            public void FromCmyk(double c, double m, double y, double k)
            {
                /*R = Clamp(255 * (1.0 - (c * (1 - k) + k)));
                G = Clamp(255 * (1.0 - (m * (1 - k) + k)));
                B = Clamp(255 * (1.0 - (y * (1 - k) + k)));*/
            }

            public void FromHsv()
            {

            }

            public void FromYCbCr(double y, double cb, double cr)
            {
                /*R = Clamp(y + 1.402 * (cr - 128.0));
                G = Clamp(y - 0.344136 * (cb - 128.0) - 0.714136 * (cr - 128.0));
                B = Clamp(y + 1.772 * (cb - 128.0));*/
            }

            public void FromHsl(double h, double s, double l)
            {
                double _h = h / 360.0;
                double _q = (l < 0.5) ? l * (1.0 + s) : l + s - (l * s);
                double _p = 2.0 * l - _q;
                if (s == 0.0)
                {
                    R = Clamp(255 * l);
                    G = R;
                    B = R;
                }
                else
                {
                    R = Clamp(255 * ColorBound(_p, _q, _h + 0.33333333333));
                    G = Clamp(255 * ColorBound(_p, _q, _h));
                    B = Clamp(255 * ColorBound(_p, _q, _h - 0.33333333333));
                }
            }
        }
    }
}
