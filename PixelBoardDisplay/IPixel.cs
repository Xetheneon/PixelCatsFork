using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    /// <summary>
    /// A pixel with no location. Location should be managed with a 2D array.
    /// </summary>
    public interface IPixel
    {
        /// <summary>
        /// O-255 Red LED Value
        /// </summary>
        byte Red { set; get; }
        /// <summary>
        /// O-255 Green LED Value
        /// </summary>
        byte Green { set; get; }
        /// <summary>
        /// O-255 Blue LED Value
        /// </summary>
        byte Blue { set; get; } 
    }
}
