using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    public class Pixel : IPixel
    {
        private byte red = 0;
        private byte green = 0;
        private byte blue = 0;

        public byte Red { get => red; set => red = value; }
        public byte Green { get => green; set => green = value; }
        public byte Blue { get => blue; set => blue = value; }

        public Pixel(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}
