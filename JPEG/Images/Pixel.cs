using System;

namespace JPEG.Images
{
    public class Pixel
    {
//        private readonly PixelFormat format;
        public Pixel(double a, double b, double c, PixelFormat format)
            : this(ToByte(a), ToByte(b), ToByte(c), format)
        {
        }

        public Pixel(byte firstComponent, byte secondComponent, byte thirdComponent, PixelFormat pixelFormat)
        {
//            if (!new[]{PixelFormat.RGB, PixelFormat.YCbCr}.Contains(pixelFormat))
//                throw new FormatException("Unknown pixel format: " + pixelFormat);
//           
//            this.format = pixelFormat;

            switch (pixelFormat)
            {
                case PixelFormat.RGB:
                    this.R = firstComponent;
                    this.G = secondComponent;
                    this.B = thirdComponent;
                    this.Y = ToByte(16.0 + (65.738 * this.R + 129.057 * this.G + 24.064 * this.B) / 256.0);
                    this.Cb = ToByte(128.0 + (-37.945 * this.R - 74.494 * this.G + 112.439 * this.B) / 256.0);
                    this.Cr = ToByte(128.0 + (112.439 * this.R - 94.154 * this.G - 18.285 * this.B) / 256.0);
                    break;
                case PixelFormat.YCbCr:
                    this.Y = firstComponent;
                    this.Cb = secondComponent;
                    this.Cr = thirdComponent;
                    this.R = ToByte((298.082 * this.Y + 408.583 * this.Cr) / 256.0 - 222.921);
                    this.G = ToByte((298.082 * this.Y - 100.291 * this.Cb - 208.120 * this.Cr) / 256.0 + 135.576);
                    this.B = ToByte((298.082 * this.Y + 516.412 * this.Cb) / 256.0 - 276.836);
                    break;
                default:
                    throw new FormatException("Unknown pixel format: " + pixelFormat);
            }
        }

//        private readonly double r;
//        private readonly double g;
//        private readonly double b;
//
//        private readonly double y;
//        private readonly double cb;
//        private readonly double cr;

        public byte R { get; }

        public byte G { get; }

        public byte B { get; }

        public byte Y { get; }

        public byte Cb { get; }

        public byte Cr { get; }

        private static byte ToByte(double d)
        {
            var val = (int) d;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            if (val < byte.MinValue)
                return byte.MinValue;
            return (byte) val;
        }
    }
}