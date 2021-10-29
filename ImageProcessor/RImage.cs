using System;
using System.Drawing;

namespace ImageProcessor
{
    public class RImage
    {
        private Bitmap _InputBitmap = null;
        private Bitmap _OutputBitmap = null;

        public RImage()
        {
        }

        public RImage(int width, int height)
        {
        }

        public RImage(Bitmap bitmap)
        {
            _InputBitmap = bitmap;
            _OutputBitmap = _InputBitmap;
        }

        public RImage(string bitmapFile)
        {
            _InputBitmap = new Bitmap(bitmapFile);
            _OutputBitmap = _InputBitmap;
        }

        public Bitmap ToBimap()
        {
            return _OutputBitmap;
        }

        public Array Mat
        {
            get
            {
                return Bitmap2Array(_OutputBitmap);
            }
        }

        public Bitmap GetOriginalBitmap
        {
            get
            {
                return _InputBitmap;
            }
        }


        private Array Bitmap2Array(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            int[,,] res = new int[width, height, 3];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    for (int pxcol = 0; pxcol < 3; pxcol++)
                    {
                        switch (pxcol)
                        {
                            case 0:
                                {
                                    res[x, y, pxcol] = pixelColor.R;
                                    break;
                                }
                            case 1:
                                {
                                    res[x, y, pxcol] = pixelColor.G;
                                    break;
                                }
                            case 2:
                                {
                                    res[x, y, pxcol] = pixelColor.B;
                                    break;
                                }
                            default:
                                {
                                    res[x, y, pxcol] = pixelColor.R;
                                    break;
                                }
                        }
                    }
                }
            }
            return res;
        }

        public Bitmap GetChannel(Enum.Colors channel)
        {
            switch (channel)
            {
                case Enum.Colors.Red:
                    {
                        return RGBFilter(0, 255, 0, 0, 0, 0);
                    }
                case Enum.Colors.Green:
                    {
                        return RGBFilter(0, 0, 0, 255, 0, 0);
                    }
                case Enum.Colors.Blue:
                    {
                        return RGBFilter(0, 0, 0, 0, 0, 255);
                    }
                default:
                    {
                        return RGBFilter(0, 0, 0, 255, 0, 0);
                    }
            }
        }

        public Bitmap RGBFilter(byte redLow, byte redHigh, byte greenLow, byte greenHigh, byte blueLow, byte blueHigh)
        {
            Bitmap image = _OutputBitmap;
            int width = image.Width;
            int height = image.Height;

            Bitmap res = new Bitmap(_OutputBitmap.Width, _OutputBitmap.Height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color oldPixelColor = image.GetPixel(x, y);
                    Color newPixelColor = FilteredColor(oldPixelColor, redLow, redHigh, greenLow, greenHigh, blueLow, blueHigh);
                    res.SetPixel(x, y, newPixelColor);
                }
            }
            return res;
        }

        private Color FilteredColor(Color oldPixelColor, byte redLimitLow, byte redLimitHigh, byte greenLimitLow, byte greenLimitHigh, byte blueLimitLow, byte blueLimitHigh)
        {
            int red = (oldPixelColor.R > redLimitLow && oldPixelColor.R < redLimitHigh) ? oldPixelColor.R : 0;
            int green = (oldPixelColor.G > greenLimitLow && oldPixelColor.G < greenLimitHigh) ? oldPixelColor.G : 0;
            int blue = (oldPixelColor.B > blueLimitLow && oldPixelColor.B < blueLimitHigh) ? oldPixelColor.B : 0;
            return Color.FromArgb(red, green, blue);
        }

        public Bitmap GetImageGray()
        {
            Bitmap image = _OutputBitmap;
            int width = image.Width;
            int height = image.Height;

            Bitmap res = new Bitmap(_OutputBitmap.Width, _OutputBitmap.Height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayValue = ColorToGrayDark(pixelColor);
                    res.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }
            return res;
        }

        private int ColorToGray(Color pixelColor)
        {
            return (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);
        }

        private int ColorToGrayDark(Color pixelColor)
        {
            return (int)((pixelColor.R * pixelColor.G * pixelColor.B) / 65025);
        }

        //private Bitmap Array2Bitmap(Array imageArray)
        //{
        //    Bitmap res = new Bitmap(_OutputBitmap.Width, _OutputBitmap.Height);

        //    for (int row = 0; row < res.Height; row++)
        //    {
        //        for (int col = 0; col < res.Width; col++)
        //        {

        //            //res.SetPixel(col, row, imageArray)
        //        }
        //    }

        //    return res;
        //}
    }
}
