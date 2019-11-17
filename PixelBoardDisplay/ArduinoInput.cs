using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace PixelBoard
{
    public class ArduinoButtonEventArgs : EventArgs
    {
        public bool Left = false;
        public bool Right = false;
        public bool Fire = false;

        public ArduinoButtonEventArgs(bool left, bool right, bool fire) { this.Left = left; this.Right = right; this.Fire = fire; }
    }
    public class ArduinoInput
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const int VK_LEFT = 0x41;
        const int VK_RIGHT = 0x44;
        const int VK_FIRE = 0x53;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        private bool LastLeft = false;
        private bool LastRight = false;
        private bool LastFire = false;

        private SerialPortManager SerialPortManager;

        private event ButtonEventHandler ButtonPressEvent;
        public delegate void ButtonEventHandler(object sender, ArduinoButtonEventArgs e);

        public ArduinoInput(SerialPortManager serialPortManager)
        {
            SerialPortManager = serialPortManager;
            ManageKeyPresses(HandleKeys);
        }

        private void HandleKeys(object sender, ArduinoButtonEventArgs e)
        {
            if(e.Left)
            {
                keybd_event((byte)VK_LEFT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }
            else if(LastLeft)
            {
                keybd_event((byte)VK_LEFT, 0, KEYEVENTF_KEYUP | 0, 0);
            }
            if(e.Right)
            {
                keybd_event((byte)VK_RIGHT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }
            else if (LastRight)
            {
                keybd_event((byte)VK_RIGHT, 0, KEYEVENTF_KEYUP | 0, 0);
            }
            if (e.Fire)
            {
                keybd_event((byte)VK_FIRE, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            }
            else if (LastFire)
            {
                keybd_event((byte)VK_FIRE, 0, KEYEVENTF_KEYUP | 0, 0);
            }
        }


        private void ManageKeyPresses(ButtonEventHandler buttonPressEvent)
        {
            Thread buttonManager = new Thread(ButtonThread);
            ButtonPressEvent += buttonPressEvent;
            buttonManager.Start();
        }

        private void ButtonThread()
        {
            while (true)
            {
                if (SerialPortManager.SerialPort.IsOpen)
                {
                    if(SerialPortManager.SerialPort.BytesToRead > 0)
                    {
                        if(Convert.ToChar(SerialPortManager.SerialPort.ReadByte()) == 'b')
                        {
                            int input = SerialPortManager.SerialPort.ReadByte();
                            ArduinoButtonEventArgs e;
                            switch (input)
                            {
                                case 7:
                                    e = new ArduinoButtonEventArgs(true, true, true);
                                    break;
                                case 6:
                                    e = new ArduinoButtonEventArgs(true, true, false);
                                    break;
                                case 5:
                                    e = new ArduinoButtonEventArgs(true, false, true);
                                    break;
                                case 4:
                                    e = new ArduinoButtonEventArgs(true, false, false);
                                    break;
                                case 3:
                                    e = new ArduinoButtonEventArgs(false, true, true);
                                    break;
                                case 2:
                                    e = new ArduinoButtonEventArgs(false, true, false);
                                    break;
                                case 1:
                                    e = new ArduinoButtonEventArgs(false, false, true);
                                    break;
                                case 0:
                                    e = new ArduinoButtonEventArgs(false, false, false);
                                    break;
                                default:
                                    continue;
                            }

                            ButtonPressEvent.Invoke(this, e);
                        }
                    }
                }
            }
        }
    }
}
