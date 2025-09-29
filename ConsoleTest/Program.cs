using Microsoft.Extensions.Configuration;
using PixelBoard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        public enum State { Title, Playing, GameOver };
        public static State state = State.Playing;
        public enum GameChoiceState { Snake, Tetris, Education};

        public static GameChoiceState game = GameChoiceState.Snake;

        public static IConfiguration _config = null;
        public static IPixel[,] title = null;
        public static IPixel[,] board = new IPixel[20, 10];
        public static IPixel[,] background = new IPixel[20, 10];
        //public static bool isInitialised = false;


        static void Main(string[] args)
        {
            // Load configuration
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var useEmulator = bool.Parse(_config.GetValue(Type.GetType("System.String"), "UseEmulator").ToString());

            //ReadBMP("hello.txt", ref title);


            IDisplay display = useEmulator ? new ConsoleDisplay() : new ArduinoDisplay();
            IPixel[,] pixels = new IPixel[20, 10];
            Queue<(int x, int y)> snake = new Queue<(int x, int y)>();
            int snakeLength = 5;
            int headX = 10;
            int headY = 5;
            int directionX = 1;
            int directionY = 0;
            Random rand = new Random();
            (int x, int y) food = (rand.Next(20), rand.Next(10));
            int score = 0;

            state = State.Title;
            while (true)
            {
                if (state == State.Title)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;

                        switch (key)
                        {
                            case ConsoleKey.S:
                                state = State.Playing;
                                break;
                            case ConsoleKey.A:
                                game = (GameChoiceState)(((int)game + 2) % 3);
                                break;
                            case ConsoleKey.D:
                                game = (GameChoiceState)(((int)game + 1) % 3);
                                break;
                        }
                    }
                    if (game == GameChoiceState.Snake)
                    {
                        for (sbyte i = 0; i < 20; i++)
                        {
                            for (sbyte j = 0; j < 10; j++)
                            {
                                pixels[i, j] = new Pixel(255, 30, 255);
                            }
                        }

                        int[,] sShape = new int[,]
                        {
                            {1, 1, 1, 0, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 1, 1}
                        };
                        int shapeWidth = sShape.GetLength(1);
                        int shapeHeight = sShape.GetLength(0);
                        int offsetX = (20 - shapeWidth) / 2;
                        int offsetY = (10 - shapeHeight) / 2;

                        for (int y = 0; y < shapeHeight; y++)
                        {
                            for (int x = 0; x < shapeWidth; x++)
                            {
                                if (sShape[y, x] == 1)
                                {
                                    pixels[offsetX + x, offsetY + y] = new Pixel(0, 0, 0); // Black pixel
                                }
                            }
                        }

                    }
                    else if (game == GameChoiceState.Tetris)
                    {
                        for (sbyte i = 0; i < 20; i++)
                        {
                            for (sbyte j = 0; j < 10; j++)
                            {
                                pixels[i, j] = new Pixel(100, 75, 200);
                            }
                        }
                        int[,] sShape = new int[,]
                        {
                            {1, 0, 0, 0, 0},
                            {1, 0, 0, 0, 0},
                            {1, 1, 1, 1, 1},
                            {1, 0, 0, 0, 0},
                            {1, 0, 0, 0, 0}
                        };
                        int shapeWidth = sShape.GetLength(1);
                        int shapeHeight = sShape.GetLength(0);
                        int offsetX = (20 - shapeWidth) / 2;
                        int offsetY = (10 - shapeHeight) / 2;

                        for (int y = 0; y < shapeHeight; y++)
                        {
                            for (int x = 0; x < shapeWidth; x++)
                            {
                                if (sShape[y, x] == 1)
                                {
                                    pixels[offsetX + x, offsetY + y] = new Pixel(0, 0, 0); // Black pixel
                                }
                            }
                        }
                    }
                    else if (game == GameChoiceState.Education)
                    {
                        for (sbyte i = 0; i < 20; i++)
                        {
                            for (sbyte j = 0; j < 10; j++)
                            {
                                pixels[i, j] = new Pixel(200, 50, 150);
                            }
                        }
                        int[,] sShape = new int[,]
                       {
                            {1, 1, 1, 1, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1},
                            {1, 0, 1, 0, 1}
                       };
                        int shapeWidth = sShape.GetLength(1);
                        int shapeHeight = sShape.GetLength(0);
                        int offsetX = (20 - shapeWidth) / 2;
                        int offsetY = (10 - shapeHeight) / 2;

                        for (int y = 0; y < shapeHeight; y++)
                        {
                            for (int x = 0; x < shapeWidth; x++)
                            {
                                if (sShape[y, x] == 1)
                                {
                                    pixels[offsetX + x, offsetY + y] = new Pixel(0, 0, 0); // Black pixel
                                }
                            }
                        }
                    }
                }

                
                if (state == State.Playing)
                {
                    Thread.Sleep(100);

                    // Snake setup
                    if (game == GameChoiceState.Snake) 
                    {
                        display.DisplayInt(score);
                        // Check if snake eats food
                        if (headX == food.x && headY == food.y)
                        {
                            score++;
                            snakeLength++;
                            food = (rand.Next(20), rand.Next(10));
                        }

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
                                pixels[i, j] = new Pixel(255, 30, 255);
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
                        // Draw food
                        pixels[food.x, food.y] = new Pixel(255, 0, 0); // Red food
                    }

                   

                }
                display.Draw(pixels);
            }
            
        }
    }
}
    
