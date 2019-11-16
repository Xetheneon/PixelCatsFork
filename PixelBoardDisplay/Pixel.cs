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

        public override bool Equals(object obj)
        {
            

            if (obj == null || !typeof(IPixel).IsAssignableFrom(obj.GetType()))
            {
                return base.Equals(obj);
            }
            else
            {
                Type t = obj.GetType();
                bool b = t.IsAssignableFrom(typeof(IPixel));
                Pixel other = (Pixel)obj;
                if (this.Red == other.Red && this.Green == other.Green && this.Blue == other.Blue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
