using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    public class LocatedPixel : Pixel, ILocatedPixel
    {
        private sbyte column;
        private sbyte row;

        public sbyte Column { get => column; set => column = value; }
        public sbyte Row { get => row; set => row = value; }

        public LocatedPixel(byte red, byte green, byte blue, sbyte column, sbyte row) : base(red, green, blue) 
        {
            Column = column;
            Row = row;
        }

        public override bool Equals(object obj)
        {
            if(base.Equals(obj))
            {
                LocatedPixel other = (LocatedPixel)obj;
                if (this.Column == other.Column && this.Row == other.Row)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
