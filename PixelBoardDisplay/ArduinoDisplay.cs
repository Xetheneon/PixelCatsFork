using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
//using System.IO.Ports;
using System.Threading;
using System.Timers;

namespace PixelBoard
{
    class ArduinoDisplay : IDisplay
    {
        private static SerialPort serialPort = new SerialPort();
        private DisplayHelper dh = new DisplayHelper();

        public ArduinoDisplay()
        {
            while (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.PortName = "COM4";
                    serialPort.BaudRate = 115200;
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
            Pixel[,] toDraw = new Pixel[this.dh.height, this.dh.width];
            Array.Copy(this.dh.currentBoard, toDraw, this.dh.currentBoard.Length);

            byte[] stream = new byte[601];

            bool changed = false;

            int counter = 1;
            for (sbyte i = 0; i < this.dh.height; i++)
            {
                for (sbyte j = 0; j < this.dh.width; j++)
                {
                    if (toDraw[i, j] != null)
                    {
                        if (this.dh.lastBoard == null || !toDraw[i, j].Equals(this.dh.lastBoard[i, j]))
                        {
                            stream[counter] = toDraw[Math.Abs(i - dh.height - 1), j].Red;
                            stream[counter + 1] = toDraw[Math.Abs(i - dh.height - 1), j].Green;
                            stream[counter + 2] = toDraw[Math.Abs(i - dh.height - 1), j].Blue;
                            counter = counter + 3;
                            changed = true;
                        }
                    }
                }
            }

            if (changed)
            {
                stream[0] = Convert.ToByte('g');
                serialPort.Write(stream, 0, 601);
            }



            if (this.dh.currentLCDNumber != this.dh.lastLCDNumber)
            {
                int outInteger = this.dh.currentLCDNumber;
                this.dh.lastLCDNumber = outInteger;
                byte[] LCDbytes = new byte[5];
                LCDbytes[0] = Convert.ToByte('s');
                byte[] intbytes = BitConverter.GetBytes(outInteger);
                int count = 1;
                foreach (byte b in intbytes)
                {
                    LCDbytes[count] = b;
                    count++;
                }
                serialPort.Write(LCDbytes, 0, 5);
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
