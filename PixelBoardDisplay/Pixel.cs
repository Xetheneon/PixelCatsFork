using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    class Pixel : IPixel
    {
        public byte Red { set => throw new NotImplementedException(); }
        public byte Green { set => throw new NotImplementedException(); }
        public byte Blue { set => throw new NotImplementedException(); }
    }
}
