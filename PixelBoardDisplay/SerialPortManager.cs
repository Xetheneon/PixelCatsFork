using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace PixelBoard
{
    public class SerialPortManager
    {
        private static SerialPort serialPort = new SerialPort();

        public SerialPort SerialPort { get => serialPort; }

        public SerialPortManager()
        {
            while (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.PortName = "COM3";
                    serialPort.BaudRate = 115200;
                    serialPort.WriteBufferSize = 64;
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
    }
}
