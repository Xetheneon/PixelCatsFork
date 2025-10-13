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
            this.dh.MakeTimer(dtfr);
            this.dh.SetSize(20, 10);

        }

        public ArduinoDisplay(sbyte height, sbyte width, sbyte framerate = 50)
        {
            this.dh.SetFramerate(framerate);
            this.dh.SetSize(height, width);
            initBoard();
            ElapsedEventHandler dtfr = drawToFramerate;
            this.dh.MakeTimer(dtfr);
        }
        public void DrawBatch(IEnumerable<ILocatedPixel> pixels)
        {
           
            List<byte> buffer = new List<byte>();

            foreach (var pixel in pixels)
            {
                
                buffer.Add((byte)pixel.Column);
                buffer.Add((byte)pixel.Row);
                buffer.Add(pixel.Red);
                buffer.Add(pixel.Green);
                buffer.Add(pixel.Blue);

                
            }

            if (buffer.Count > 0)
            {
                SerialPortManager.SerialPort.Write(buffer.ToArray(), 0, buffer.Count);
            }
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
                this.dh.RefreshDisplay(this);
                Pixel[,] toDraw = new Pixel[this.dh.height, this.dh.width];
                Array.Copy(this.dh.currentBoard, toDraw, this.dh.currentBoard.Length);

                byte[] stream = new byte[600];

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
                        }
                        counter += 3;
                    }
                }

                if (streamMode == "normal")
                {
                    string outScore = this.dh.currentLCDNumber;
                    this.dh.lastLCDNumber = outScore;

                    // Proper left-pad to 6 chars
                    string paddedNum = outScore.PadLeft(6, ' ');

                    byte[] LCDbytes = new byte[6];
                    char[] intAsCharArray = paddedNum.ToCharArray();

                    for (int i = 0; i < 6; i++)
                    {
                        LCDbytes[i] = Convert.ToByte(intAsCharArray[i]);
                    }

                    // Add sync marker
                    byte[] syncMarker = new byte[] { 0xAA, 0x55 };

                    SerialPortManager.SerialPort.Write(syncMarker, 0, syncMarker.Length);
                    SerialPortManager.SerialPort.Write(LCDbytes, 0, 6);
                    SerialPortManager.SerialPort.Write(stream, 0, 600);
                }
                else if (streamMode == "partial")
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
