using System;

namespace PixelBoard
{
    public class ConsoleDisplay : IDisplay
    {
        public void DisplayInt(int value)
        {
            throw new NotImplementedException();
        }

        public void DisplayInt(int value, bool? leftAligned)
        {
            throw new NotImplementedException();
        }

        public void DisplayInts(int leftValue, int rightValue)
        {
            throw new NotImplementedException();
        }

        public void Draw(IPixel[] pixels)
        {
            throw new NotImplementedException();
        }

        public void Draw(ILocatedPixel pixel)
        {
            throw new NotImplementedException();
        }
    }
}
