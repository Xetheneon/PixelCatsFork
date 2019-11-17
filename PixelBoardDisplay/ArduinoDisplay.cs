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
        private static SerialPort serialPort = new SerialPort();
        private DisplayHelper dh = new DisplayHelper();
        private bool finishedStreaming = true;

        public ArduinoDisplay()
        {
            while (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.PortName = "COM3";
                    serialPort.BaudRate = 115200;
                    serialPort.WriteBufferSize = 606;
                    serialPort.Open();
                }
                catch (UnauthorizedAccessException e)
                {

                }
                catch (InvalidOperationException e)
                {

                }
                catch (System.IO.IOException e)
                {

                }
                catch (Exception e)
                {

                }
                Thread.Sleep(1);
            }

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
                            if (this.dh.lastBoard == null || !toDraw[i, j].Equals(this.dh.lastBoard[i, j]))
                            {
                                stream[counter] = toDraw[Math.Abs(i - dh.height + 1), j].Red;
                                stream[counter + 1] = toDraw[Math.Abs(i - dh.height + 1), j].Green;
                                stream[counter + 2] = toDraw[Math.Abs(i - dh.height + 1), j].Blue;
                                counter = counter + 3;
                                changed = true;
                            }
                        }
                    }
                }

                if (serialPort.BytesToWrite == 0)
                {
                    int outInteger = this.dh.currentLCDNumber;
                    this.dh.lastLCDNumber = outInteger;

                    string paddedNum = outInteger.ToString();
                    if (paddedNum.Length < 6)
                    {
                        for (int i = 0; i < 6 - outInteger.ToString().Length; i++)
                        {
                            paddedNum.Insert(0, "0");
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
                    serialPort.Write(delimiter, 0, 1);
                    serialPort.Write(LCDbytes, 0, 6);
                    serialPort.Write(stream, 0, 600);
                }
                else
                {

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
