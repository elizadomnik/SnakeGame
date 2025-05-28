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
        snakeHead.Symbol = "■";

        string currentMovement = "RIGHT";
        string previousMovement = "RIGHT";

        List<int> snakeBodyPositions = new List<int>();
        snakeBodyPositions.Add(snakeHead.XPos);
        snakeBodyPositions.Add(snakeHead.YPos);

        int score = 0;
        DateTime frameTimestamp = DateTime.Now;

        Obstacle food = new Obstacle();
        food.XPos = randomNumberGenerator.Next(1, screenWidth - 1);
        food.YPos = randomNumberGenerator.Next(1, screenHeight - 1);
        food.Symbol = "*";
        food.Color = ConsoleColor.Cyan;

        while (true)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0); Console.Write("■");
                Console.SetCursorPosition(i, screenHeight - 1); Console.Write("■");
            }
            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i); Console.Write("■");
                Console.SetCursorPosition(screenWidth - 1, i); Console.Write("■");
            }

            Console.ForegroundColor = food.Color;
            Console.SetCursorPosition(food.XPos, food.YPos);
            Console.Write(food.Symbol);

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < snakeBodyPositions.Count; i += 2)
            {
                if (snakeBodyPositions[i] == snakeHead.XPos && snakeBodyPositions[i + 1] == snakeHead.YPos && i == 0) 
                {
                }
                else 
                {
                    Console.SetCursorPosition(snakeBodyPositions[i], snakeBodyPositions[i + 1]);
                    Console.Write("■");
                }
            }

            Console.ForegroundColor = snakeHead.Color;
            Console.SetCursorPosition(snakeHead.XPos, snakeHead.YPos);
            Console.Write(snakeHead.Symbol);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(1, 1);
            Console.Write($"Score: {score}    ");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                previousMovement = currentMovement;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (previousMovement != "DOWN") currentMovement = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (previousMovement != "UP") currentMovement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (previousMovement != "RIGHT") currentMovement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (previousMovement != "LEFT") currentMovement = "RIGHT";
                        break;
                }
            }

            int oldHeadX = snakeHead.XPos;
            int oldHeadY = snakeHead.YPos;

            if (currentMovement == "UP") snakeHead.YPos--;
            if (currentMovement == "DOWN") snakeHead.YPos++;
            if (currentMovement == "LEFT") snakeHead.XPos--;
            if (currentMovement == "RIGHT") snakeHead.XPos++;

            bool ateFoodThisFrame = false;

            if (snakeHead.XPos == food.XPos && snakeHead.YPos == food.YPos)
            {
                score++;
                ateFoodThisFrame = true;
                food.XPos = randomNumberGenerator.Next(1, screenWidth - 1);
                food.YPos = randomNumberGenerator.Next(1, screenHeight - 1);
            }

            snakeBodyPositions.Insert(0, snakeHead.XPos);
            snakeBodyPositions.Insert(1, snakeHead.YPos);

            if (!ateFoodThisFrame)
            {
                if (snakeBodyPositions.Count > 2)
                {
                    snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);
                    snakeBodyPositions.RemoveAt(snakeBodyPositions.Count - 1);
                }
            }

            if (snakeHead.XPos <= 0 || snakeHead.XPos >= screenWidth - 1 ||
                snakeHead.YPos <= 0 || snakeHead.YPos >= screenHeight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                Console.WriteLine("Game Over - Wall Collision!");
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                Console.WriteLine($"Your Score is: {score}");
                Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 2);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            for (int i = 2; i < snakeBodyPositions.Count; i += 2)
            {
                if (snakeHead.XPos == snakeBodyPositions[i] && snakeHead.YPos == snakeBodyPositions[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
                    Console.WriteLine("Game Over - Self Collision!");
                    Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
                    Console.WriteLine($"Your Score is: {score}");
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
