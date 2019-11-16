using System;
using PixelBoard;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            IDisplay display = new ConsoleDisplay();
            IPixel[,] pixels = new IPixel[20, 10];
            while (true)
            {
                Thread.Sleep(10);
                for (sbyte i = 0; i < 20; i++)
                {
                    for (sbyte j = 0; j < 10; j++)
                    {
                        pixels[i, j] = new Pixel((byte)random.Next(1, 256), (byte)random.Next(1, 256), (byte)random.Next(1, 256));
                    }
                }
                display.Draw(pixels);
            }
        }
    }
}
