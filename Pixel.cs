using System;

public class Pixel
{
    public int XPos { get; set; } 
    public int YPos { get; set; } 
    public ConsoleColor Color { get; set; } 
    public string Symbol { get; set; } 

    public Pixel(int xPos = 0, int yPos = 0, ConsoleColor color = ConsoleColor.White, string symbol = "■")
    {
        XPos = xPos;
        YPos = yPos;
        Color = color;
        Symbol = symbol;
    }
}