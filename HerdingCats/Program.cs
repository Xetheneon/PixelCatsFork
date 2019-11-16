using System;
using System.Collections.Generic;
using System.Threading;
using PixelBoard;

namespace HerdingCats
{
    class Program
    {
        public enum State { Playing, GameOver };
        public static State state = State.Playing;
        public static IPixel[,] board = new IPixel[20, 10];
        public static IPixel[,] background = new IPixel[20, 10];
        public static Random rng = new Random();

        public static IPixel[,] image = { { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(7, 6, 4), new Pixel(46, 32, 25), new Pixel(54, 36, 27), new Pixel(54, 34, 25), new Pixel(61, 40, 30), new Pixel(56, 38, 29), new Pixel(21, 16, 11), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(16, 11, 8), new Pixel(103, 69, 54), new Pixel(143, 97, 80), new Pixel(139, 90, 73), new Pixel(125, 76, 63), new Pixel(124, 77, 62), new Pixel(131, 82, 66), new Pixel(110, 72, 56), new Pixel(75, 51, 38), new Pixel(25, 18, 14), }, { new Pixel(126, 81, 63), new Pixel(150, 94, 74), new Pixel(185, 126, 110), new Pixel(200, 142, 129), new Pixel(210, 159, 150), new Pixel(201, 148, 138), new Pixel(175, 120, 111), new Pixel(160, 111, 98), new Pixel(117, 77, 63), new Pixel(111, 78, 64), }, { new Pixel(142, 92, 75), new Pixel(196, 134, 120), new Pixel(223, 159, 147), new Pixel(235, 177, 172), new Pixel(234, 179, 176), new Pixel(230, 176, 173), new Pixel(220, 163, 160), new Pixel(193, 138, 132), new Pixel(135, 91, 81), new Pixel(126, 88, 75), }, { new Pixel(181, 130, 116), new Pixel(222, 161, 149), new Pixel(233, 173, 162), new Pixel(238, 181, 174), new Pixel(236, 184, 179), new Pixel(234, 183, 180), new Pixel(222, 166, 163), new Pixel(208, 151, 149), new Pixel(183, 136, 129), new Pixel(144, 105, 95), }, { new Pixel(196, 146, 132), new Pixel(224, 165, 154), new Pixel(236, 177, 169), new Pixel(240, 189, 184), new Pixel(236, 188, 185), new Pixel(235, 187, 184), new Pixel(226, 174, 171), new Pixel(214, 161, 157), new Pixel(185, 138, 131), new Pixel(146, 108, 101), }, { new Pixel(184, 134, 120), new Pixel(203, 153, 142), new Pixel(196, 144, 137), new Pixel(194, 143, 137), new Pixel(222, 172, 169), new Pixel(214, 164, 161), new Pixel(187, 137, 132), new Pixel(184, 135, 131), new Pixel(166, 120, 115), new Pixel(141, 102, 95), }, { new Pixel(190, 140, 129), new Pixel(194, 143, 133), new Pixel(144, 111, 109), new Pixel(193, 145, 142), new Pixel(225, 177, 171), new Pixel(201, 153, 149), new Pixel(189, 145, 144), new Pixel(140, 109, 108), new Pixel(160, 114, 109), new Pixel(133, 91, 85), }, { new Pixel(205, 149, 135), new Pixel(225, 172, 161), new Pixel(224, 172, 164), new Pixel(225, 174, 166), new Pixel(229, 172, 163), new Pixel(206, 152, 145), new Pixel(211, 160, 156), new Pixel(212, 163, 159), new Pixel(201, 152, 148), new Pixel(137, 94, 87), }, { new Pixel(212, 151, 138), new Pixel(236, 177, 170), new Pixel(235, 179, 174), new Pixel(224, 162, 153), new Pixel(230, 168, 159), new Pixel(206, 147, 139), new Pixel(200, 145, 137), new Pixel(228, 176, 172), new Pixel(214, 156, 153), new Pixel(147, 98, 89), }, { new Pixel(170, 120, 110), new Pixel(223, 166, 161), new Pixel(210, 152, 146), new Pixel(211, 149, 143), new Pixel(195, 137, 132), new Pixel(165, 103, 98), new Pixel(180, 121, 117), new Pixel(200, 146, 143), new Pixel(201, 147, 144), new Pixel(106, 69, 60), }, { new Pixel(109, 80, 74), new Pixel(200, 150, 142), new Pixel(195, 140, 136), new Pixel(196, 134, 132), new Pixel(198, 141, 140), new Pixel(188, 130, 129), new Pixel(158, 100, 98), new Pixel(173, 118, 116), new Pixel(191, 146, 140), new Pixel(72, 47, 43), }, { new Pixel(77, 56, 51), new Pixel(202, 155, 148), new Pixel(211, 159, 153), new Pixel(187, 132, 129), new Pixel(206, 154, 153), new Pixel(198, 143, 144), new Pixel(171, 109, 112), new Pixel(195, 141, 138), new Pixel(172, 127, 121), new Pixel(45, 29, 26), }, { new Pixel(33, 24, 21), new Pixel(179, 132, 123), new Pixel(213, 161, 151), new Pixel(219, 161, 154), new Pixel(219, 160, 158), new Pixel(209, 149, 148), new Pixel(196, 144, 140), new Pixel(186, 139, 134), new Pixel(120, 83, 78), new Pixel(5, 3, 3), }, { new Pixel(0, 0, 0), new Pixel(50, 35, 32), new Pixel(182, 135, 125), new Pixel(221, 162, 151), new Pixel(223, 163, 157), new Pixel(216, 155, 154), new Pixel(200, 143, 140), new Pixel(132, 93, 88), new Pixel(13, 9, 8), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(15, 11, 10), new Pixel(77, 56, 53), new Pixel(91, 65, 65), new Pixel(87, 61, 60), new Pixel(68, 47, 46), new Pixel(9, 6, 6), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), }, { new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), new Pixel(0, 0, 0), } };

        public static IPixel[] rainbow = {
                new Pixel(230, 0, 2), new Pixel(255, 106, 0),
                new Pixel(252, 154, 3), new Pixel(253, 220, 89),
                new Pixel(162, 207, 0), new Pixel(35, 169, 74),
                new Pixel(35, 169, 204), new Pixel(2, 82, 141),
                new Pixel(151, 14, 128), new Pixel(236, 19, 132) };
    

        static int score = 0;

        public static int[] cats = { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 };

        public class Coord
        {
            public int Row;
            public int Col;
            public Pixel Colour;
        }

        public class Bat
        {
            public int Col;
            public int shootFrameDelay;
            public int framesTillShoot;

            public Bat()
            {
                shootFrameDelay = 10;
                Col = 5;
            }
        }

        public static Bat bat = new Bat();

        public class Laser
        {
            public Coord[] Bolt;

            public Laser(int pCol)
            {
                Bolt = new Coord[4];

                for (int i = 0; i < Bolt.Length; i++)
                {
                    Bolt[i] = new Coord();
                    Bolt[i].Col = pCol;
                    Bolt[i].Row = board.GetLength(0) - 3 + i;
                }

                Bolt[0].Colour = new Pixel(255, 255, 255);
                Bolt[1].Colour = new Pixel(200, 200, 225);
                Bolt[2].Colour = new Pixel(150, 150, 225);
                Bolt[3].Colour = new Pixel(75, 75, 255);
            }
        }

        public static List<Coord> Falling = new List<Coord>();

        public static List<Laser> Lasers = new List<Laser>();

        public class FrameTimer
        {
            public int frameDelay;
            public int timeTillThing;

            public bool DoThing()
            {
                timeTillThing--;
                if(timeTillThing <= 0)
                {
                    timeTillThing = frameDelay;
                    return true;
                }
                return false;
            }

            public void ModifyPeriod(int pInt)
            {
                frameDelay += pInt;
            }

            public FrameTimer(int pDelay)
            {
                frameDelay = pDelay;
                timeTillThing = pDelay;
            }
        }

        static FrameTimer spawnTimer = new FrameTimer(45);
        static FrameTimer dropTimer = new FrameTimer(30);

        #region
        
        // https://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb

        public static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        #endregion

        static void UpdateBackground()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = background[i, j];
                }
            }
        }

        static void UpdateCats()
        {
            for (int i = 0; i < cats.Length; i++)
            {
                if (cats[i] != 0)
                {
                    int move = rng.Next(3);
                    switch (move)
                    {
                        case 1:
                            if (i > 0 && cats[i - 1] == 0)
                            {
                                cats[i - 1] = 1;
                                cats[i] = 0;
                            }
                            break;
                        case 2:
                            if (i != 9 && cats[i + 1] == 0)
                            {
                                cats[i + 1] = 1;
                                cats[i] = 0;
                            }
                            break;
                    }
                }
            }

            for (int i = 0; i < cats.Length; i++)
            {
                if (cats[i] == 1)
                {
                    board[board.GetLength(0) - 1, i] = new Pixel(0, 255, 0);
                }
                else
                {
                    board[board.GetLength(0) - 1, i] = new Pixel(255, 0, 0);
                }
            }
        }

        static void UpdateBat()
        {
            int batRow = board.GetLength(0) - 2;
            board[batRow, bat.Col] = background[batRow, bat.Col];

            bat.framesTillShoot = bat.framesTillShoot > 0 ? bat.framesTillShoot - 1 : 0;

            while (Console.KeyAvailable)
            {
                char ch = Console.ReadKey(true).KeyChar;

                switch (ch)
                {
                    case 'a':
                        bat.Col = bat.Col != 0 ? bat.Col - 1 : 0;
                        break;
                    case 'd':
                        bat.Col = bat.Col != 9 ? bat.Col + 1 : 9;
                        break;
                    case 's':
                        if (bat.framesTillShoot <= 0)
                        {
                            bat.framesTillShoot = bat.shootFrameDelay;
                            Laser laser = new Laser(bat.Col);
                            Lasers.Add(laser);
                        }

                        break;
                }
            }

            board[batRow, bat.Col] = new Pixel(255, 255, 255);
        }

        static void UpdateLasers()
        {
            foreach (Laser l in Lasers)
            {
                for (int i = 0; i < l.Bolt.Length; i++)
                {
                    l.Bolt[i].Row--;
                }
            }


            foreach (Laser l in Lasers)
            {
                foreach (Coord c in l.Bolt)
                {
                    if (c.Row < board.GetLength(0) && c.Row >= 0)
                    {
                        board[c.Row, c.Col] = c.Colour;
                    }
                }
            }

            for(int laserIndex = Lasers.Count - 1; laserIndex >= 0; laserIndex--)
            {
                for(int fallerIndex = Falling.Count - 1; fallerIndex >= 0; fallerIndex--)
                {
                    if(Lasers[laserIndex].Bolt[0].Col == Falling[fallerIndex].Col &&
                        Lasers[laserIndex].Bolt[0].Row == Falling[fallerIndex].Row)
                    {
                        score++;
                        if (score % 2 == 0)
                        {
                            if (rng.Next(2) == 1)
                            {
                                spawnTimer.ModifyPeriod(-1);
                            }
                            else
                            {
                                dropTimer.ModifyPeriod(-1);
                            }
                        }
                        Lasers.RemoveAt(laserIndex);
                        Falling.RemoveAt(fallerIndex);
                        break;
                    }
                }
            }
        }

        static void UpdateFallingStuff()
        {
            if (dropTimer.DoThing())
            {
                for (int i = 0; i < Falling.Count; i++)
                {
                    Falling[i].Row++;
                }
            }

            if (spawnTimer.DoThing())
            {
                Coord faller = new Coord();
                faller.Col = rng.Next(0, board.GetLength(1));
                faller.Row = 0;
                faller.Colour = new Pixel(0, 0, 0);
                Falling.Add(faller);
            }

            for(int i = Falling.Count - 1; i >= 0; i--)
            {
                if(Falling[i].Row == board.GetLength(0))
                {
                    if(cats[Falling[i].Col] == 1)
                    {
                        cats[Falling[i].Col] = 0;
                    }

                    Falling.RemoveAt(i);
                }
            }

            foreach(Coord c in Falling)
            {
                board[c.Row, c.Col] = c.Colour;
            }
        }

        static void ScrollImage()
        {

      //      imageOffset++;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < background.GetLength(0); i++)
            {
                for (int j = 0; j < background.GetLength(1); j++)
                {
                    background[i, j] = rainbow[j];
                }
            }

            IDisplay display = new ConsoleDisplay();
            while (true)
            {
               // state = State.GameOver;
                if (state == State.Playing)
                {
                    UpdateBackground();
                    UpdateCats();
                    UpdateLasers();
                    UpdateBat();
                    UpdateFallingStuff();

                    Thread.Sleep(50);
                    display.DisplayInt(score);
                    display.Draw(board);

                    bool allCatsDead = true;

                    foreach (int i in cats)
                    {
                        if (i == 1)
                        {
                            allCatsDead = false;
                            break;
                        }
                    }

                    if (allCatsDead)
                    {
                        state = State.GameOver;
                    }
                }
                else if(state == State.GameOver)
                {
                    ScrollImage();
                    display.Draw(image);
                }
            }
        }
    }
}