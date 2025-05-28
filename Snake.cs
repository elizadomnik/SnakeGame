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
        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;
        Random randomnummer = new Random();

        Pixel hoofd = new Pixel(); 
        hoofd.XPos = screenwidth / 2; 
        hoofd.YPos = screenheight / 2; 
        hoofd.Color = ConsoleColor.Red; 

        string movement = "RIGHT";
        List<int> telje = new List<int>(); 
        int score = 0;
        
        List<int> teljePositie = new List<int>();
        teljePositie.Add(hoofd.XPos);
        teljePositie.Add(hoofd.YPos);

        DateTime tijd = DateTime.Now;

        
        Obstacle foodItem = new Obstacle(); 
        foodItem.XPos = randomnummer.Next(1, screenwidth -1); 
        foodItem.YPos = randomnummer.Next(1, screenheight-1); 
        foodItem.Symbol = "*"; 

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = foodItem.Color; 
            Console.SetCursorPosition(foodItem.XPos, foodItem.YPos); 
            Console.Write(foodItem.Symbol); 

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.SetCursorPosition(hoofd.XPos, hoofd.YPos); 
            Console.Write("■"); 

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine("Score: " + score); 
            
            Console.ForegroundColor = hoofd.Color;
            Console.SetCursorPosition(hoofd.XPos, hoofd.YPos);
            Console.Write("■"); 
            ConsoleKeyInfo info = Console.ReadKey();

            switch (info.Key)
            {
                case ConsoleKey.UpArrow: movement = "UP"; break;
                case ConsoleKey.DownArrow: movement = "DOWN"; /* missing break */ break; // Added break for now
                case ConsoleKey.LeftArrow: movement = "LEFT"; break;
                case ConsoleKey.RightArrow: movement = "RIGHT"; break;
            }

            if (movement == "UP") hoofd.YPos--;
            if (movement == "DOWN") hoofd.YPos++;
            if (movement == "LEFT") hoofd.XPos--;
            if (movement == "RIGHT") hoofd.XPos++;
            
            if (hoofd.XPos == foodItem.XPos && hoofd.YPos == foodItem.YPos) 
            {
                score++;
                foodItem.XPos = randomnummer.Next(1, screenwidth -1);
                foodItem.YPos = randomnummer.Next(1, screenheight -1);
            }

            teljePositie.Insert(0, hoofd.XPos);
            teljePositie.Insert(1, hoofd.YPos);
            teljePositie.RemoveAt(teljePositie.Count - 1);
            teljePositie.RemoveAt(teljePositie.Count - 1);

            if (hoofd.XPos <= 0 || hoofd.XPos >= screenwidth - 1 || hoofd.YPos <= 0 || hoofd.YPos >= screenheight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2);
                Console.WriteLine("Game Over");
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 1);
                Console.WriteLine("Dein Score ist: " + score);
                Console.SetCursorPosition(screenwidth / 5, screenheight / 2 + 2);
                Environment.Exit(0);
            }


            for (int i = 0; i < telje.Count(); i += 2)
            {
                if (hoofd.XPos == telje[i] && hoofd.YPos == telje[i + 1])
                {
                    Environment.Exit(0);
                }
            }
            Thread.Sleep(50);
        }
    }
}