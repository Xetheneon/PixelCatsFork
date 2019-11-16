using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    class LocatedPixel : ILocatedPixel
    {
        public sbyte Column { set => throw new NotImplementedException(); }
        public sbyte Row { set => throw new NotImplementedException(); }
        public byte Red { set => throw new NotImplementedException(); }
        public byte Green { set => throw new NotImplementedException(); }
        public byte Blue { set => throw new NotImplementedException(); }
    }
}
