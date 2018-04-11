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
        public enum EdgeHandling
        {
            Extend, //The nearest border pixels are conceptually extended as far as necessary to provide values for the convolution. Corner pixels are extended in 90 degree wedges. Other edge pixels are extended in lines.
            Wrap, //The image is conceptually wrapped (or tiled) and values are taken from the opposite edge or corner.
            Mirror, //The image is conceptually mirrored at the edges. For example, attempting to read a pixel 3 units outside an edge reads one 3 units inside the edge instead.
            Crop //Any pixel in the output image which would require values from beyond the edge is skipped. This method can result in the output image being slightly smaller, with the edges having been cropped.
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

        private int ClampExtend(int value, int max)
        {
            if (value < 0) return 0;
            else if (value > max) return max;
            return value;
        }

        private int ClampWrap(int value, int max)
        {
            if (value < 0) return max + value + 1;
            else if (value > max) return value - max - 1;
            return value;
        }

        private int ClampMirror(int value, int max)
        {
            if (value < 0) return -value;
            else if (value > max) return 2 * max - value;
            return value;
        }

        private int ClampCrop(int value, int max)
        {
            return value;
        }


        public void GaussianBlur(double sigma = 1.0, bool normalize = false, int? kernelSize = null)
        {
            var _kernel = new ConvolutionKernel();
            _kernel.MakeGaussianKernel(sigma, kernelSize);
            if (normalize) _kernel.Normalize();
            Convolution(_kernel, EdgeHandling.Extend);
        }

         public void Convolution(ConvolutionKernel kernel, EdgeHandling edgeHandling)
        {
            unsafe
            {
                var _workingBitmap = new Bitmap(_bitmap);
                var _rect = new Rectangle(0, 0, _bitmap.Width, _bitmap.Height);
                BitmapData _originalBitmapData = _bitmap.LockBits(_rect, ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                var _workingBitmapData = _workingBitmap.LockBits(_rect, ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                int _bytesPerPixel = Bitmap.GetPixelFormatSize(_bitmap.PixelFormat) / 8;
                int _heightInPixels = _originalBitmapData.Height;
                int _widthInBytes = _originalBitmapData.Width * _bytesPerPixel;
                int _offset = kernel.Offset;
                int _imageOffset = 0; //edge position
                byte* _firstPixelPointerOriginal = (byte*)_originalBitmapData.Scan0;
                byte* _firstPixelPointerWorking = (byte*)_workingBitmapData.Scan0;
                byte*[] _rowsOriginal = new byte*[_heightInPixels];
                Func<int, int, int> _clamping = null;
                switch (edgeHandling)
                {
                    case EdgeHandling.Extend:
                        _clamping = ClampExtend;
                        break;
                    case EdgeHandling.Wrap:
                        _clamping = ClampWrap;
                        break;
                    case EdgeHandling.Mirror:
                        _clamping = ClampMirror;
                        break;
                    case EdgeHandling.Crop:
                    default:
                        _imageOffset = kernel.Offset;
                        _clamping = ClampCrop;
                        break;
                }
                for (int i = 0; i < _heightInPixels; i++) _rowsOriginal[i] = _firstPixelPointerOriginal + (i * _originalBitmapData.Stride);
                //BGR format
                for (int y = _imageOffset; y < _heightInPixels - _imageOffset; y++)
                {
                    for (int x = _imageOffset * _bytesPerPixel; x < _widthInBytes - _imageOffset * _bytesPerPixel; x = x + _bytesPerPixel)
                    {
                        double _r = 0.0;
                        double _g = 0.0;
                        double _b = 0.0;
                        byte* _workingScanLine = _firstPixelPointerWorking + (y * _workingBitmapData.Stride);
                        for (int ky = -_offset; ky <= _offset; ky++)
                        {
                            int _yOffset = _clamping(y + ky, _heightInPixels - 1);
                            //
                            byte* _originalScanLine = _rowsOriginal[_yOffset];
                            for (int kx = -_offset; kx <= _offset; kx++)
                            {
                                double _k = kernel[kx, ky];
                                int _xOffset = _clamping(x + kx * _bytesPerPixel, _widthInBytes - 3);
                                _b += _originalScanLine[_xOffset] * _k;
                                _g += _originalScanLine[_xOffset + 1] * _k;
                                _r += _originalScanLine[_xOffset + 2] * _k;
                            }
                        }
                        _workingScanLine[x] = Clamp(_b / kernel.NormalizationFactor + kernel.Bias);
                        _workingScanLine[x + 1] = Clamp(_g / kernel.NormalizationFactor + kernel.Bias);
                        _workingScanLine[x + 2] = Clamp(_r / kernel.NormalizationFactor + kernel.Bias);
                    }
                }
                _workingBitmap.UnlockBits(_workingBitmapData);
                _bitmap.UnlockBits(_originalBitmapData);
                _bitmap = _workingBitmap;
                //originalBitmap = _workingBitmap.Clone(_rect, originalBitmap.PixelFormat);
                //_workingBitmap.Save(@"C:\Users\rokr\Desktop\aaa_no.bmp");
            }
        }
    }
}
