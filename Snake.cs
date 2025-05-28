using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;
        Random randomNumberGenerator = new Random();

        Pixel snakeHead = new Pixel();
        snakeHead.XPos = screenWidth / 2;
        snakeHead.YPos = screenHeight / 2;
        snakeHead.Color = ConsoleColor.Red;
        snakeHead.Symbol = "S";

        string currentMovement = "RIGHT";
        List<int> snakeBodySegments = new List<int>();
        int score = 0;

        List<int> snakeBodyPositions = new List<int>();
        snakeBodyPositions.Add(snakeHead.XPos);
        snakeBodyPositions.Add(snakeHead.YPos);

        DateTime frameTimestamp = DateTime.Now;

        Obstacle food = new Obstacle();
        food.XPos = randomNumberGenerator.Next(1, screenWidth - 1);
        food.YPos = randomNumberGenerator.Next(1, screenHeight - 1);
        food.Symbol = "*";
        food.Color = ConsoleColor.Cyan;

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = food.Color;
            Console.SetCursorPosition(food.XPos, food.YPos);
            Console.Write(food.Symbol);

            Console.ForegroundColor = snakeHead.Color;
            Console.SetCursorPosition(snakeHead.XPos, snakeHead.YPos);
            Console.Write(snakeHead.Symbol);

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, screenHeight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 1);
            Console.Write($"Score: {score} ");

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < snakeBodySegments.Count(); i += 2)
            {
                // Console.SetCursorPosition(snakeBodySegments[i], snakeBodySegments[i + 1]);
                // Console.Write("■");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow: currentMovement = "UP"; break;
                case ConsoleKey.DownArrow: currentMovement = "DOWN"; break;
                case ConsoleKey.LeftArrow: currentMovement = "LEFT"; break;
                case ConsoleKey.RightArrow: currentMovement = "RIGHT"; break;
            }

            if (currentMovement == "UP") snakeHead.YPos--;
            if (currentMovement == "DOWN") snakeHead.YPos++;
            if (currentMovement == "LEFT") snakeHead.XPos--;
            if (currentMovement == "RIGHT") snakeHead.XPos++;

            if (snakeHead.XPos == food.XPos && snakeHead.YPos == food.YPos)
            {
                score++;
                food.XPos = randomNumberGenerator.Next(1, screenWidth - 1);
                food.YPos = randomNumberGenerator.Next(1, screenHeight - 1);
            }

            snakeBodyPositions.Insert(0, snakeHead.XPos);
            snakeBodyPositions.Insert(1, snakeHead.YPos);
            snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);
            snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);

            if (snakeHead.XPos <= 0 || snakeHead.XPos >= screenWidth - 1 ||
                snakeHead.YPos <= 0 || snakeHead.YPos >= screenHeight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                Console.WriteLine("Game Over");
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                Console.WriteLine("Your Score is: " + score);
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            for (int i = 0; i < snakeBodySegments.Count(); i += 2)
            {
                if (snakeHead.XPos == snakeBodySegments[i] && snakeHead.YPos == snakeBodySegments[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                    Console.WriteLine("Game Over - Self Collision!");
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                    Console.WriteLine("Your Score is: " + score);
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(100);
        }
    }
}