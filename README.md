# ImageProcessing
Pure C# Image Processing Library (Playground). As it is a personal pet project it (for now) only handles 24 bit BMP images. It is a C# rewrite of my old Delphi Image Processing Library.

## Legal information and credits

ImageProcessing is project by Ales Hotko and was first released in April 2018. It's licensed under the MIT license.

## Usage

```csharp
ImageProcessing imageProcessing = new ImageProcessing();

//Load image from File
imageProcessing.LoadFromFile(@"Image.bmp");

//Greyscale Image
imageProcessing.GreyScale(ImageProcessing.GreyScaleType.Average);

//2D Convolution with Kernel
imageProcessing.Convolution(new ConvolutionKernel(ConvolutionKernel.PresetKernel.LaplacianOfGaussian), ImageProcessing.EdgeHandling.Extend);

//Sepia effect
imageProcessing.Sepia();

//Negative image
imageProcessing.Negative();

//Gaussian blur with radius r
imageProcessing.GaussianBlur(r);

//AlphaBlending with Color
imageProcessing.AlphaBlend(Color.Red, 0.5);
imageProcessing.AlphaBlend(r, g, b, 0.5);

//Gamma Correction
imageProcessing.GammaCorrection(1.5);

//Threshold (Binarize) image
imageProcessing.Threshold(80);

//Save processed Image to File
imageProcessing.SaveToFile(@"NewImage.bmp");
```
### Available Modes and Kernels

#### Greyscale Modes
* `GreyScaleType.Average`
* `GreyScaleType.BWTV`
* `GreyScaleType.Normal`
* `GreyScaleType.ContemporaryPhosphors`

#### Convolution Edge Handling Modes
* `EdgeHandling.Extend` - The nearest border pixels are conceptually extended as far as necessary to provide values for the convolution. Corner pixels are extended in 90 degree wedges. Other edge pixels are extended in lines.
* `EdgeHandling.Wrap` - The image is conceptually wrapped (or tiled) and values are taken from the opposite edge or corner.
* `EdgeHandling.Mirror` - The image is conceptually mirrored at the edges. For example, attempting to read a pixel 3 units outside an edge reads one 3 units inside the edge instead.
* `EdgeHandling.Crop` - Any pixel in the output image which would require values from beyond the edge is skipped. This method can result in the output image being slightly smaller, with the edges having been cropped.

#### Preset Convolution Kernels (5x5)
* `PresetKernel.Identity`
* `PresetKernel.Laplace`
* `PresetKernel.HorizontalSobel`
* `PresetKernel.VerticalSobel`
* `PresetKernel.SmoothMore`
* `PresetKernel.ColoredEmboss`
* `PresetKernel.Emboss`
* `PresetKernel.Grease`
* `PresetKernel.HiPass`
* `PresetKernel.Vibrate`
* `PresetKernel.TraceContour`
* `PresetKernel.FindEdges`
* `PresetKernel.DoubleVision`
* `PresetKernel.HiPassSharpen`
* `PresetKernel.MeanRemoval`
* `PresetKernel.VerticalEdge`
* `PresetKernel.HorizontalEdge`
* `PresetKernel.Mean`
* `PresetKernel.Blur`
* `PresetKernel.BlurMore`
* `PresetKernel.Smooth`
* `PresetKernel.SmoothCircular`
* `PresetKernel.LaplacianOfGaussian`