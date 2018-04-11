using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation
{
    public class ConvolutionKernel
    {
        public enum PresetKernel
        {
            Identity,
            Laplace,
            HorizontalSobel,
            VerticalSobel,
            SmoothMore,
            ColoredEmboss,
            Emboss,
            Grease,
            HiPass,
            Vibrate,
            TraceContour,
            FindEdges,
            DoubleVision,
            HiPassSharpen,
            MeanRemoval,
            VerticalEdge,
            HorizontalEdge,
            Mean,
            Blur,
            BlurMore,
            Smooth,
            SmoothCircular,
            LaplacianOfGaussian
        }

        private double[,] _kernel = new double[5, 5]
        {
            { 0,0,0,0,0 },
            { 0,0,0,0,0 },
            { 0,0,1,0,0 },
            { 0,0,0,0,0 },
            { 0,0,0,0,0 } };

        private int _size = 5;
        private int _offset = 2;

        private double _bias = 0.0;
        private double _normalizationFactor = 1.0;

        public double[,] Kernel { get { return _kernel; } set { _kernel = value; } }
        public double NormalizationFactor { get { return _normalizationFactor; } set { _normalizationFactor = value; } }
        public double Bias { get { return _bias; } set { _bias = value; } }
        public int Size { get { return _size; } set { _size = value; _offset = (_size - 1) / 2; } }
        public int Offset { get { return _offset; } }
        public double this[int x, int y] { get { return _kernel[x + _offset, y + _offset]; } }

        public ConvolutionKernel(PresetKernel kernel)
        {
            SetKernel(kernel);
        }

        public ConvolutionKernel() : this(PresetKernel.Identity) { }

        private void SetKernel(PresetKernel kernel)
        {
            this.Size = 5; //call setter, so all other calculations are made
            switch (kernel)
            {
                case PresetKernel.Identity:
                    _kernel = new double[5, 5]
                    {
                        { 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0 },
                        { 0, 0, 1, 0, 0 },
                        { 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0 } };
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Blur:
                    _kernel = new double[5, 5]
                    {
                        { 0, 0, 0, 0, 0 },
                        { 0, 1, 2, 1, 0 },
                        { 0, 2, 2, 2, 0 },
                        { 0, 1, 2, 1, 0 },
                        { 0, 0, 0, 0, 0 } };
                    _bias = 0.0;
                    _normalizationFactor = 14.0;
                    break;
                case PresetKernel.BlurMore:
                    _kernel = new double[5, 5]
                    {
                        { 0, 1, 1, 1, 0 },
                        { 1, 2, 2, 2, 1 },
                        { 1, 2, 4, 2, 1 },
                        { 1, 2, 2, 2, 1 },
                        { 0, 1, 1, 1, 0 } };
                    _bias = 0.0;
                    _normalizationFactor = 32.0;
                    break;
                case PresetKernel.ColoredEmboss:
                    _kernel = new double[5, 5]
                    {
                        { -20,   0,  0,  0, -10 },
                        {   0, -20, 10, -20,  0 },
                        {   0,  10, 35,  10,  0 },
                        {   0, -20, 10,  20,  0 },
                        { -20,   0,  0,  0,  10 } };
                    _bias = 80.0;
                    _normalizationFactor = 10.0;
                    break;
                case PresetKernel.DoubleVision:
                    _kernel = new double[5, 5]
                    {
                        { -1, -2, -1, -2, -1},
                        {  2, -1,  2, -1,  2},
                        { -1, -2,  5, -2, -1},
                        {  2, -1,  2, -1,  2},
                        { -1, -2, -1, -2, -1}};
                    _bias = 0.0;
                    _normalizationFactor = -7.0;
                    break;
                case PresetKernel.Emboss:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  1, -1, -1,  0},
                        {  0,  1, -1, -1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.FindEdges:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1, -1, -1,  0},
                        {  0, -1,  8, -1,  0},
                        {  0, -1, -1, -1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Grease:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  1, -7,  1,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.HiPass:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  1, -2,  1,  0},
                        {  0, -2,  6, -2,  0},
                        {  0,  1, -2,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 2.0;
                    break;
                case PresetKernel.HiPassSharpen:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  0, -1,  0,  0},
                        {  0, -1,  5, -1,  0},
                        {  0,  0, -1,  0,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.HorizontalEdge:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1, -1, -1,  0},
                        {  0,  0,  0,  0,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.HorizontalSobel:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1, -2, -1,  0},
                        {  0,  0,  0,  0,  0},
                        {  0,  1,  2,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Laplace:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  0, -1,  0,  0},
                        {  0, -1,  4, -1,  0},
                        {  0,  0, -1,  0,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Mean:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  1,  1,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 9.0;
                    break;
                case PresetKernel.MeanRemoval:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1, -1, -1,  0},
                        {  0, -1,  9, -1,  0},
                        {  0, -1, -1, -1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Smooth:

                    break;
                case PresetKernel.SmoothCircular:
                    _kernel = new double[5, 5]
                    {
                        {  0,  1,  1,  1,  0},
                        {  1,  1,  1,  1,  1},
                        {  1,  1,  1,  1,  1},
                        {  1,  1,  1,  1,  1},
                        {  0,  1,  1,  1,  0}};
                    _bias = 0.0;
                    _normalizationFactor = 21.0;
                    break;
                case PresetKernel.SmoothMore:
                    break;
                case PresetKernel.TraceContour:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1, -1, -1,  0},
                        {  0, -1,  9, -1,  0},
                        {  0, -1, -1, -1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 255.0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.VerticalEdge:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1,  0,  1,  0},
                        {  0, -1,  0,  1,  0},
                        {  0, -1,  0,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.VerticalSobel:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  0,  0,  0},
                        {  0, -1,  0,  1,  0},
                        {  0, -2,  0,  2,  0},
                        {  0, -1,  0,  1,  0},
                        {  0,  0,  0,  0,  0}};
                    _bias = 0;
                    _normalizationFactor = 1.0;
                    break;
                case PresetKernel.Vibrate:
                    _kernel = new double[5, 5]
                    {
                        {  9,  0,  3,  0,  9},
                        {  0,  0,  0,  0,  0},
                        {  3,  0,  1,  0,  3},
                        {  0,  0,  0,  0,  0},
                        {  9,  0,  3,  0,  9}};
                    _bias = 0;
                    _normalizationFactor = 49.0;
                    break;
                case PresetKernel.LaplacianOfGaussian:
                    _kernel = new double[5, 5]
                    {
                        {  0,  0,  1,  0,  0},
                        {  0,  1,  2,  1,  0},
                        {  1,  2,-16,  2,  1},
                        {  0,  1,  2,  1,  0},
                        {  0,  0,  1,  0,  0}};
                    _bias = 0;
                    _normalizationFactor = 1.0;
                    break;
                default:
                    break;
            }
        }

        public void Normalize(double normalizeValue = 0.0)
        {
            double _factor = 1.0;
            if(normalizeValue != 0.0)
            {
                _factor = 1.0 / normalizeValue;
            }
            else if(_kernel[0,0] != 0.0)
            {
                _factor = 1.0 / _kernel[0, 0];
            }
            double _normalizationFactor = 0.0;
            for(int _x=0; _x<_size; _x++)
                for (int _y = 0; _y < _size; _y++)
                {
                    _kernel[_x, _y] = _kernel[_x, _y] * _factor;
                    _normalizationFactor += _kernel[_x, _y];
                }
        }

        public void MakeGaussianKernel(double sigma = 1.0, int size = 0)
        {
            int _kernelSize = (size == 0) ? (int)Math.Round(6.0 * sigma) + 1 : size;
            if (_kernelSize % 2 == 0) _kernelSize++;
            this.Size = _kernelSize;
            _bias = 0.0;
            _normalizationFactor = 0.0;
            _kernel = new double[_size, _size];
            double _twoSigmaFactor = -1.0 / (2.0 * Math.Pow(sigma, 2));
            double _piSigmaFactor = 1.0 / (2.0 * Math.PI * Math.Pow(sigma, 2));
            for (int _x = 0; _x < _size; _x++)
            {
                double _xDist = Math.Pow(_x - _offset, 2);
                for (int _y = 0; _y < _size; _y++)
                {
                    double _yDist = Math.Pow(_y - _offset, 2);
                    double _value = _piSigmaFactor * Math.Exp((_xDist + _yDist) * _twoSigmaFactor);
                    _kernel[_x, _y] = _value;
                    _normalizationFactor += _value;
                }
            }
        }
    }
}
