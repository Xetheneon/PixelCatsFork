using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
//using System.IO.Ports;
using System.Threading;
using System.Timers;

namespace PixelBoard
{
    public class ArduinoDisplay : IDisplay
    {
        private DisplayHelper dh = new DisplayHelper();
        private bool finishedStreaming = true;
        private SerialPortManager SerialPortManager = new SerialPortManager();

        private const string streamMode = "normal"; // options: normal, partial - this should be moved to config file

        public ArduinoDisplay()
        {
            new ArduinoInput(SerialPortManager); // This line must be here to allow button presses
            initBoard();
            ElapsedEventHandler dtfr = drawToFramerate;
            this.dh.makeTimer(dtfr);
        }

        public ArduinoDisplay(sbyte height, sbyte width, sbyte framerate = 50)
        {
            this.dh.SetFramerate(framerate);
            this.dh.SetSize(height, width);
            initBoard();
            ElapsedEventHandler dtfr = drawToFramerate;
            this.dh.makeTimer(dtfr);
        }

        private void initBoard()
        {
            this.dh.currentBoard = new Pixel[this.dh.height, this.dh.width];
            for (sbyte i = 0; i < this.dh.height; i++)
            {
                for (sbyte j = 0; j < this.dh.width; j++)
                {
                    dh.currentBoard[i, j] = new Pixel(0, 0, 0);
                }
            }
        }

        private void drawToFramerate(Object source, ElapsedEventArgs e)
        {
            if (finishedStreaming)
            {
                finishedStreaming = false;
                Pixel[,] toDraw = new Pixel[this.dh.height, this.dh.width];
                Array.Copy(this.dh.currentBoard, toDraw, this.dh.currentBoard.Length);

                byte[] stream = new byte[600];

                bool changed = false;

                int counter = 0;
                for (sbyte i = 0; i < this.dh.height; i++)
                {
                    for (sbyte j = 0; j < this.dh.width; j++)
                    {
                        if (toDraw[i, j] != null)
                        {
                            stream[counter] = toDraw[Math.Abs(i - dh.height + 1), j].Red;
                            stream[counter + 1] = toDraw[Math.Abs(i - dh.height + 1), j].Green;
                            stream[counter + 2] = toDraw[Math.Abs(i - dh.height + 1), j].Blue;
                            counter = counter + 3;
                            changed = true;

                        }
                    }
                }

                if (streamMode == "normal")
                {
                    string outScore = this.dh.currentLCDNumber;
                    this.dh.lastLCDNumber = outScore;

                    string paddedNum = outScore.ToString();
                    if (paddedNum.Length < 6)
                    {
                        for (int i = 0; i < 6 - outScore.ToString().Length; i++)
                        {
                            paddedNum.Insert(0, " ");
                        }

                    }

                    byte[] LCDbytes = new byte[6];
                    char[] intAsCharArray = paddedNum.ToCharArray();

                    int count = 0;
                    foreach (char c in intAsCharArray)
                    {
                        LCDbytes[count] = Convert.ToByte(c);
                        count++;
                    }

                    char[] delimiter = new char[1] { 's' };
                    SerialPortManager.SerialPort.Write(delimiter, 0, 1);
                    SerialPortManager.SerialPort.Write(LCDbytes, 0, 6);
                    SerialPortManager.SerialPort.Write(stream, 0, 600);
                }
                else if(streamMode == "partial")
                {
                    // TODO: Send partial update
                }
                finishedStreaming = true;
            }

        }

        public void DisplayInt(int value)
        {
            this.dh.DisplayInt(value);

        }

        public void DisplayInt(int value, bool? leftAligned)
        {
            this.dh.DisplayInt(value, leftAligned);
        }

        public void DisplayInts(int leftValue, int rightValue)
        {
            this.dh.DisplayInts(leftValue, rightValue);
        }

        public void Draw(IPixel[,] pixels)
        {
            this.dh.Draw(pixels);
        }

        public void Draw(ILocatedPixel pixel)
        {
            this.dh.Draw(pixel);
        }
    }
}
