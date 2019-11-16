using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    public class LocatedPixel : Pixel, ILocatedPixel
    {
        public LocatedPixel(byte red, byte green, byte blue) : base(red, green, blue) { }
        public sbyte Column { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public sbyte Row { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }
}
