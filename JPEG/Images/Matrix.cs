using System.Drawing;
using System.Drawing.Imaging;

namespace JPEG.Images
{
    public class Matrix
    {
        public readonly int Height;
        public readonly Pixel[,] Pixels;
        public readonly int Width;

        public Matrix(int height, int width)
        {
            this.Height = height;
            this.Width = width;

            this.Pixels = new Pixel[height, width];
            for (var i = 0; i < height; ++i)
            for (var j = 0; j < width; ++j)
                this.Pixels[i, j] = new Pixel(0, 0, 0, PixelFormat.RGB);
        }

        public static explicit operator Matrix(Bitmap bmp)
        {
            var height = bmp.Height - bmp.Height % 8;
            var width = bmp.Width - bmp.Width % 8;
            var matrix = new Matrix(height, width);

//            for (var j = 0; j < height; j++)
//            {
//                for (var i = 0; i < width; i++)
//                {
//                    var pixel = bmp.GetPixel(i, j);
//                    matrix.Pixels[j, i] = new Pixel(pixel.R, pixel.G, pixel.B, PixelFormat.RGB);
//                }
//            }

            var bitmapData = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.ReadOnly,
                bmp.PixelFormat);

            unsafe
            {
                for (var j = 0; j < height; j++)
                {
                    var row = (byte*) bitmapData.Scan0 + j * bitmapData.Stride;
                    for (var i = 0; i < width; i++)
                        matrix.Pixels[j, i] = new Pixel(row[3 * i + 2], row[3 * i + 1], row[3 * i], PixelFormat.RGB);
                }
            }

            bmp.UnlockBits(bitmapData);

            return matrix;
        }

        public static explicit operator Bitmap(Matrix matrix)
        {
//            var bmp = new Bitmap(matrix.Width, matrix.Height);
            var bmp = new Bitmap(matrix.Width, matrix.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

//            for (var j = 0; j < bmp.Height; j++)
//            {
//                for (var i = 0; i < bmp.Width; i++)
//                {
//                    var pixel = matrix.Pixels[j, i];
//                    bmp.SetPixel(i, j, Color.FromArgb(ToByte(pixel.R), ToByte(pixel.G), ToByte(pixel.B)));
//                }
//            }

            var bitmapData = bmp.LockBits(
                new Rectangle(Point.Empty, bmp.Size),
                ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            unsafe
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    var row = (byte*) bitmapData.Scan0 + j * bitmapData.Stride;
                    for (var i = 0; i < bmp.Width; i++)
                    {
                        var pixel = matrix.Pixels[j, i];
                        row[3 * i + 2] = pixel.R;
                        row[3 * i + 1] = pixel.G;
                        row[3 * i] = pixel.B;
                    }
                }
            }

            return bmp;
        }
    }
}