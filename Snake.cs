using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static int screenWidth;
    static int screenHeight;
    static int score;
    static string currentMovement;
    static string previousMovement;
    static bool foodEatenThisFrame;

    static List<Pixel> snake;
    static Obstacle food;
    static Random randomNumberGenerator = new Random();
    static bool gameOver;

    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        screenWidth = Console.WindowWidth;
        screenHeight = Console.WindowHeight;
        Console.CursorVisible = false;

        InitializeGame();

        while (!gameOver)
        {
            HandleInput();
            if (!gameOver)
            {
                UpdateGameLogic();
            }
            if (!gameOver)
            {
                DrawGame();
            }
            Thread.Sleep(100);
        }

        ShowGameOverScreen();
    }

    static void InitializeGame()
    {
        score = 0;
        currentMovement = "RIGHT";
        previousMovement = "RIGHT";
        foodEatenThisFrame = false;
        gameOver = false;

        snake = new List<Pixel>();
        int initialSnakeX = screenWidth / 2;
        int initialSnakeY = screenHeight / 2;
        snake.Add(new Pixel(initialSnakeX, initialSnakeY, ConsoleColor.Red, "■"));

        PlaceFood();
    }

    static void PlaceFood()
    {
        bool foodOnSnake;
        int foodX, foodY;
        do
        {
            foodOnSnake = false;
            foodX = randomNumberGenerator.Next(1, screenWidth - 1);
            foodY = randomNumberGenerator.Next(1, screenHeight - 1);

            foreach (Pixel segment in snake)
            {
                if (segment.IsAt(foodX, foodY))
                {
                    foodOnSnake = true;
                    break;
                }
            }
        } while (foodOnSnake);

        if (food == null)
        {
            food = new Obstacle(foodX, foodY, ConsoleColor.Cyan, "*");
        }
        else
        {
            food.XPos = foodX;
            food.YPos = foodY;
            food.Color = ConsoleColor.Cyan;
            food.Symbol = "*";
        }
    }

    static void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            previousMovement = currentMovement;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (previousMovement != "DOWN" || snake.Count == 1) currentMovement = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    if (previousMovement != "UP" || snake.Count == 1) currentMovement = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    if (previousMovement != "RIGHT" || snake.Count == 1) currentMovement = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    if (previousMovement != "LEFT" || snake.Count == 1) currentMovement = "RIGHT";
                    break;
                case ConsoleKey.Escape:
                    gameOver = true;
                    break;
            }
        }
    }

    static void UpdateGameLogic()
    {
        foodEatenThisFrame = false;

        Pixel currentHead = snake[0];
        int newHeadX = currentHead.XPos;
        int newHeadY = currentHead.YPos;

        switch (currentMovement)
        {
            case "UP": newHeadY--; break;
            case "DOWN": newHeadY++; break;
            case "LEFT": newHeadX--; break;
            case "RIGHT": newHeadX++; break;
        }

        if (newHeadX <= 0 || newHeadX >= screenWidth - 1 ||
            newHeadY <= 0 || newHeadY >= screenHeight - 1)
        {
            gameOver = true;
            return;
        }

        for (int i = 0; i < snake.Count - (foodEatenThisFrame ? 0 : (snake.Count > 1 ? 1 : 0)); i++)
        {
            if (snake[i].IsAt(newHeadX, newHeadY))
            {
                gameOver = true;
                return;
            }
        }

        Pixel newHead = new Pixel(newHeadX, newHeadY, ConsoleColor.Red, "■");
        snake.Insert(0, newHead);

        if (snake.Count > 1)
        {
            snake[1].Color = ConsoleColor.Green;
        }

        if (newHead.IsAt(food.XPos, food.YPos))
        {
            score++;
            foodEatenThisFrame = true;
            PlaceFood();
        }

        if (!foodEatenThisFrame)
        {
            if (snake.Count > 1)
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }
    }

    static void DrawGame()
    {
        Console.Clear();
        DrawBorders();
        DrawFood();
        DrawSnake();
        DrawScore();
        Console.SetCursorPosition(0, screenHeight);
    }

    static void DrawBorders()
    {
        Console.ForegroundColor = ConsoleColor.White;
        string borderSymbol = "█";

        for (int i = 0; i < screenWidth; i++)
        {
            Console.SetCursorPosition(i, 0); Console.Write(borderSymbol);
            Console.SetCursorPosition(i, screenHeight - 1); Console.Write(borderSymbol);
        }
        for (int i = 0; i < screenHeight; i++)
        {
            Console.SetCursorPosition(0, i); Console.Write(borderSymbol);
            Console.SetCursorPosition(screenWidth - 1, i); Console.Write(borderSymbol);
        }
    }

    static void DrawFood()
    {
        Console.ForegroundColor = food.Color;
        Console.SetCursorPosition(food.XPos, food.YPos);
        Console.Write(food.Symbol);
    }

    static void DrawSnake()
    {
        foreach (Pixel segment in snake)
        {
            Console.ForegroundColor = segment.Color;
            Console.SetCursorPosition(segment.XPos, segment.YPos);
            Console.Write(segment.Symbol);
        }
    }

    static void DrawScore()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(1, 1);
        Console.Write($"Score: {score}    ");
    }

    static void ShowGameOverScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;

        string gameOverMessage = "Game Over!";
        string scoreMessage = $"Your Score is: {score}";
        string exitMessage = "Press any key to exit.";

        Console.SetCursorPosition((screenWidth - gameOverMessage.Length) / 2, screenHeight / 2 - 1);
        Console.WriteLine(gameOverMessage);

        Console.SetCursorPosition((screenWidth - scoreMessage.Length) / 2, screenHeight / 2);
        Console.WriteLine(scoreMessage);

        Console.SetCursorPosition((screenWidth - exitMessage.Length) / 2, screenHeight / 2 + 2);
        Console.WriteLine(exitMessage);

        Console.ReadKey(true);
    }
}
