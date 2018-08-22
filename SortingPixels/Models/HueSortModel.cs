using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SortingPixels.Models
{
    class HueSortModel
    {
        // Suppose the Hue ranges from 0 ~ 360
        private const int HUE_RANGE = 361;
        private PixelFormat format;
        private byte[] buffer;
        private double dpix, dpiy;
        private int stride, bytesPerPixel;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public HueSortModel(int width, int height)
        {
            format = PixelFormats.Rgb24;
            dpix = 96;
            dpiy = 96;
            bytesPerPixel = 3;
            Width = width;
            Height = height;
            stride = width * bytesPerPixel;
            buffer = new byte[stride * height];
        }


        /// <summary>
        /// Compute the Hue-Saturation-Brightness (HSB) hue value, in degrees.
        /// If R == G == B, the hue is meaningless, and the return value is 0. 
        /// </summary>
        /// <returns>Hue value in degrees</returns>
        private float GetHue(byte R, byte G, byte B)
        {
            if (R == G && G == B)
                return 0; // 0 makes as good an UNDEFINED value as any

            float r = (float)R / 255.0f;
            float g = (float)G / 255.0f;
            float b = (float)B / 255.0f;

            float max, min;
            float delta;
            float hue = 0.0f;

            max = r; min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            delta = max - min;

            if (r == max)
            {
                hue = (g - b) / delta;
            }
            else if (g == max)
            {
                hue = 2 + (b - r) / delta;
            }
            else if (b == max)
            {
                hue = 4 + (r - g) / delta;
            }
            hue *= 60;

            if (hue < 0.0f)
            {
                hue += 360.0f;
            }
            return hue;
        }

        private BitmapSource CreateBitmapSourceFromBuffer()
        {
            return BitmapImage.Create(Width, Height, dpix, dpiy, format, BitmapPalettes.WebPalette, buffer, stride);
        }

        private void SortLineByHue(int j)
        {
            // use bucket sorting for each line
            var bucket = new List<short>[HUE_RANGE];
            for (short i = 0; i < Width; i++)
            {
                var index = j * stride + i * bytesPerPixel;
                var hue = (short)GetHue(buffer[index], buffer[index + 1], buffer[index + 2]);
                var list = bucket[hue];
                if (list == null)
                {
                    list = new List<short>();
                    bucket[hue] = list;
                }
                list.Add(i);
            }
            // reconstruct the sorted line
            var line = new byte[Width * bytesPerPixel];
            var pos = 0;
            foreach (var list in bucket)
            {
                if (list != null)
                {
                    foreach (var li in list)
                    {
                        var index = j * stride + li * bytesPerPixel;
                        line[pos++] = buffer[index];
                        line[pos++] = buffer[index + 1];
                        line[pos++] = buffer[index + 2];
                    }
                }
            }
            Array.Copy(line, 0, buffer, j * stride, line.Length);
        }
        
        public BitmapSource Randomize()
        {
            var random = new Random(Environment.TickCount);
            random.NextBytes(buffer);
            return CreateBitmapSourceFromBuffer();
        }

        public BitmapSource SortByHue()
        {
            // process line by line and use less memory
            for (int j = 0; j < Height; j++)
            {
                SortLineByHue(j);
            }

            return CreateBitmapSourceFromBuffer();
        }


        public BitmapSource SortByHueParallel()
        {
            // use parallel for all lines if the time-performance is more important than memory consumption
            Parallel.For(0, Height, j =>
            {
                SortLineByHue(j);
            });

            return CreateBitmapSourceFromBuffer();
        }
    }
}
