using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;

namespace PixelBoard
{
    public class ConsoleDisplay : IDisplay
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        private sbyte height = 20;
        private sbyte width = 10;

        private sbyte framerate = 50;

        private static System.Timers.Timer displayRefreshTimer;

        private IPixel[,] lastBoard;
        private IPixel[,] currentBoard;

        private int lastLCDNumber = 0;
        private int currentLCDNumber = 0;

        private bool refreshing = false;

        /// <summary>
        /// Construct a display with the default size (20x10)
        /// </summary>
        public ConsoleDisplay()
        {
            initBoard();
            makeTimer();
        }

        /// <summary>
        /// Construct a display with a custom size and optional framerate
        /// </summary>
        public ConsoleDisplay(sbyte height, sbyte width, sbyte framerate = 50)
        {
            if (framerate > 0 && framerate <= 60)
            {
                this.framerate = framerate;
            }
            else if (framerate > 60)
            {
                this.framerate = 60;
            }
            else
            {
                this.framerate = 1;
            }

            this.height = height;
            this.width = width;

            initBoard();
            makeTimer();
        }

        private void initBoard()
        {
            currentBoard = new Pixel[height, width];
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("000000");

            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);

            for (sbyte i = 0; i < height; i++)
            {
                for (sbyte j = 0; j < width; j++)
                {
                        Console.Write(" ");
                        Console.Write("\x1b[38;2;" + 0 + ";" + 0 + ";" + 0 + "m" + "■");
                }
                Console.WriteLine("");
            }
        }

        private void makeTimer()
        {
            displayRefreshTimer = new System.Timers.Timer(1000 / this.framerate);
            displayRefreshTimer.Elapsed += drawToFramerate;
            displayRefreshTimer.AutoReset = true;
            displayRefreshTimer.Enabled = true;
        }

        private void drawToFramerate(Object source, ElapsedEventArgs e)
        {
            if (!refreshing)
            {
                refreshing = true;
                Pixel[,] toDraw = new Pixel[height,width];
                Array.Copy(currentBoard, toDraw, currentBoard.Length);

                if (currentLCDNumber != lastLCDNumber)
                {
                    Console.SetCursorPosition(0, 0);
                    string paddedNum = currentLCDNumber.ToString();
                    lastLCDNumber = currentLCDNumber;
                    if (paddedNum.Length < 6)
                    {
                        for (int i = 1; i < 6 - paddedNum.Length; i++)
                        {
                            paddedNum.Insert(0, "0");
                        }

                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(paddedNum);
                }

                for (sbyte i = 0; i < height; i++)
                {
                    int spacer = 1;
                    for (sbyte j = 0; j < width; j++)
                    {
                        if (toDraw[i, j] != null)
                        {
                            if (lastBoard == null || !toDraw[i,j].Equals(lastBoard[i,j]))
                            {
                                Console.SetCursorPosition(j + spacer, i + 1);
                                Console.Write("\x1b[38;2;" + toDraw[i, j].Red + ";" + toDraw[i, j].Green + ";" + toDraw[i, j].Blue + "m" + "■");
                            }
                        }
                        spacer++;
                    }
                }
                if (Console.CursorTop != height + 1 && Console.CursorLeft != 0)
                {
                    Console.SetCursorPosition(0, height + 1);
                }
                lastBoard = toDraw;
                refreshing = false;
            }
        }

        private void ValidateLCDValue(int value, int max, string argumentName)
        {
            if (value > max)
            {
                throw new ArgumentOutOfRangeException(argumentName, "Int to display was over 6 digits, which is not valid");
            }
            else if (value < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, "Int to display was negative, which is not valid");
            }
        }

        public void DisplayInt(int value)
        {
            ValidateLCDValue(value, 999999, "value");
            currentLCDNumber = value;

        }

        public void DisplayInt(int value, bool? leftAligned)
        {
            ValidateLCDValue(value, 999999, "value");
            if (leftAligned != null || leftAligned == true)
            {
                while (value < 99999)
                {
                    value *= 10;
                }
            }
            currentLCDNumber = value;
        }

        public void DisplayInts(int leftValue, int rightValue)
        {
            ValidateLCDValue(leftValue, 999, "leftValue");
            ValidateLCDValue(rightValue, 999, "rightValue");

            while (leftValue < 99999)
            {
                leftValue *= 10;
            }
            leftValue += rightValue;

            currentLCDNumber = leftValue;
        }

        public void Draw(IPixel[,] pixels)
        {
            if (pixels.GetLength(0) == height)
            {
                if (pixels.GetLength(1) == width)
                {
                    currentBoard = pixels;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("pixels", "pixels width was not the defined display width");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("pixels", "pixels height was not the defined display height");
            }
        }

        public void Draw(ILocatedPixel pixel)
        {
            if (pixel.Column < width)
            {
                if (pixel.Row < height)
                {
                    currentBoard[pixel.Row, pixel.Column] = (IPixel)pixel;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("pixel", "pixel row was not within the defined display height");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("pixel", "pixel column was not within the defined display width");
            }
        }
    }
}
