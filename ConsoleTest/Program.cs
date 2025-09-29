using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Configuration;
using PixelBoard;

namespace SnakeGame
{
    class Program
    {
        public static IConfiguration _config = null;
        static void Main(string[] args)
        {
            // Load configuration
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var useEmulator = bool.Parse(_config.GetValue(Type.GetType("System.String"), "UseEmulator").ToString());

            // Choose display based on config
            IDisplay display = useEmulator ? new ConsoleDisplay() : new ArduinoDisplay();
            IPixel[,] pixels = new IPixel[20, 10];

            // Snake setup
            Queue<(int x, int y)> snake = new Queue<(int x, int y)>();
            int snakeLength = 5;
            int headX = 10;
            int headY = 5;
            int directionX = 1; // moving down
            int directionY = 0; // no horizontal movement

            while (true)
            {
                Thread.Sleep(100);
                display.DisplayInt(800813);

                // Check for input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.W:
                            directionX = -1; directionY = 0;
                            break;
                        case ConsoleKey.S:
                            directionX = 1; directionY = 0;
                            break;
                        case ConsoleKey.A:
                            directionX = 0; directionY = -1;
                            break;
                        case ConsoleKey.D:
                            directionX = 0; directionY = 1;
                            break;
                    }
                }

                // Clear background
                for (sbyte i = 0; i < 20; i++)
                {
                    for (sbyte j = 0; j < 10; j++)
                    {
                        pixels[i, j] = new Pixel(255, 30, 255); // Dark gray
                    }
                }

                // Move snake
                headX += directionX;
                headY += directionY;

                if (headX >= 20) headX = 0;
                if (headX < 0) headX = 19;
                if (headY >= 10) headY = 0;
                if (headY < 0) headY = 9;

                snake.Enqueue((headX, headY));
                if (snake.Count > snakeLength)
                    snake.Dequeue();

                foreach (var (x, y) in snake)
                {
                    pixels[x, y] = new Pixel(0, 255, 0); // Green snake
                }

                display.Draw(pixels);
            }

        }
    }
}
