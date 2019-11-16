using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    /// <summary>
    /// Display for writing out to the PixelBoard and LCD panel
    /// </summary>
    public interface IDisplay
    { 
        /// <summary>
        /// Draw a 2D array of pixels. 2D array should be size [20,10]
        /// </summary>
        /// <param name="pixels">Array of pixels to display. Max size [20,10]</param>
        void Draw(IPixel[] pixels);

        /// <summary>
        /// Draw a single pixel on the board.
        /// </summary>
        /// <param name="pixel">A pixel with a specific location</param>
        void Draw(ILocatedPixel pixel);

        /// <summary>
        /// Display a 1-6 digit integer on the lcd panel. Right aligned by default
        /// </summary>
        /// <param name="value">Integer to display</param>
        void DisplayInt(int value);

        /// <summary>
        /// Display a 1-6 digit integer on the lcd panel. Right aligned by default
        /// </summary>
        /// <param name="value">Integer to display</param>
        /// <param name="leftAligned">True to left-align the integer</param>
        void DisplayInt(int value, bool? leftAligned);

        /// <summary>
        /// Display two 1-3 digit integers on the lcd panel.
        /// </summary>
        /// <param name="leftValue">Left 3 digits (left aligned)</param>
        /// <param name="rightValue">Right 3 digits (right aligned)</param>
        void DisplayInts(int leftValue, int rightValue);
    }
}
