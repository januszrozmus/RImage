using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor
{
    public class RImage
    {
        private Bitmap _InputBitmap = null;
        private Bitmap _OutputBitmap = null;

        public RImage(Bitmap bitmap)
        {
            _InputBitmap = bitmap;
            _OutputBitmap = _InputBitmap;
        }

        public RImage(string bitmapFile)
        {
            _InputBitmap = new Bitmap(bitmapFile);
            _OutputBitmap = _InputBitmap;
            Array a = Mat;
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

        public Array GetChannel(Bitmap image, Enum.Colors channel)
        {
            int width = image.Width;
            int height = image.Height;

            int[,,] res = new int[width, height, 1];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    switch (channel)
                    {
                        case Enum.Colors.Red:
                            {
                                res[x, y, 0] = pixelColor.R;
                                break;
                            }
                        case Enum.Colors.Green:
                            {
                                res[x, y, 0] = pixelColor.G;
                                break;
                            }
                        case Enum.Colors.Blue:
                            {
                                res[x, y, 0] = pixelColor.B;
                                break;
                            }
                        default:
                            {
                                res[x, y, 0] = pixelColor.G;
                                break;
                            }
                    }
                }
            }
            return res;
        }

        public Array GetImageGray()
        {
            Bitmap image = _OutputBitmap;
            int width = image.Width;
            int height = image.Height;

            int[,,] res = new int[width, height, 1];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayValue = ColorToGray(pixelColor);
                }
            }
            return res;
        }

        private int ColorToGray(Color pixelColor)
        {
            return (int)((pixelColor.R * pixelColor.G * pixelColor.B) / (255 * 255));
        }
    }
}
