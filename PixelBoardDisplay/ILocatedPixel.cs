using System;
using System.Collections.Generic;
using System.Text;

namespace PixelBoard
{
    /// <summary>
    /// A pixel that has a location. Useful for changing one pixel at a time.
    /// </summary>
    public interface ILocatedPixel : IPixel
    {
        /// <summary>
        /// Column (min 0, max 19)
        /// </summary>
        sbyte Column { set; get; }
        /// <summary>
        /// Row (min 0, max 9)
        /// </summary>
        sbyte Row { set; get; }
    }
}
