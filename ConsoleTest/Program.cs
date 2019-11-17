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
            IDisplay display = new ArduinoDisplay();
            IPixel[,] pixels = new IPixel[20, 10];
            while (true)
            {
                Thread.Sleep(1);
                display.DisplayInt(100);
                for (sbyte i = 0; i < 20; i++)
                {
                    for (sbyte j = 0; j < 10; j++)
                    {
                        //pixels[i, j] = new Pixel((byte)random.Next(1, 256), (byte)random.Next(1, 256), (byte)random.Next(1, 256));
                        pixels[i, j] = new Pixel(0, 0, 0);
                    }
                }

                display.Draw(pixels);
            }
        }
    }
}
