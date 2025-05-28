using System;

public class Obstacle 
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor Color { get; set; }
    public string Symbol { get; set; }

    public Obstacle(int xPos = 0, int yPos = 0, ConsoleColor color = ConsoleColor.Cyan, string symbol = "*")
    {
        XPos = xPos;
        YPos = yPos;
        Color = color;
        Symbol = symbol;
    }
}