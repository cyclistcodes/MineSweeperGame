﻿using System;

namespace Minesweeper;

class Tile
{
    public int x;
    public int y;
    public bool hasBomb;
    public bool isOpen;
    public int numberOfBombsAround;
}

class Minesweeper
{
    static Random random = new Random();

    static Tile[,] createGameBoard()
    {
        Tile[,] gameBoard = new Tile[5, 5];
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                gameBoard[x, y] = new Tile { x = x, y = y, hasBomb = false, isOpen = false, numberOfBombsAround = 0 };
            }
        }
        return gameBoard;
    }

    static void placeRandomBombs(Tile[,] gameBoard, int numberOfBombs)
    {
        int bombsPlaced = 0;
        while (bombsPlaced < numberOfBombs)
        {
            int x = random.Next(0, 5);
            int y = random.Next(0, 5);
            Tile tile = gameBoard[x, y];
            if (!tile.hasBomb)
            {
                tile.hasBomb = true;
                bombsPlaced++;
            }
        }
    }

    static void displayGameBoard(Tile[,] gameBoard)
    {
        Console.WriteLine("   0  1  2  3  4 ");
        for (int y = 4; y >= 0; y--)
        {
            Console.Write($"{y}|");
            for (int x = 0; x < 5; x++)
            {
                Tile tile = gameBoard[x, y];
                if (tile.isOpen)
                {
                    if (tile.hasBomb)
                    {
                        Console.Write(" * ");
                    }
                    else
                    {
                        Console.Write($" {tile.numberOfBombsAround} ");
                    }
                }
                else
                {
                    Console.Write(" ? ");
                }
            }
            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        Tile[,] gameBoard = createGameBoard();
        placeRandomBombs(gameBoard, 5); 
        displayGameBoard(gameBoard); 
    }
}

static bool isGameWon(Tile[,] gameBoard)
{
    for (int x = 0; x < 5; x++)
    {
        for (int y = 0; y < 5; y++)
        {
            Tile tile = gameBoard[x, y];
            if (!tile.isOpen && !tile.hasBomb)
            {
                return false;
            }
        }
    }
    return true;
}

static void openTile(Tile[,] gameBoard, int x, int y)
{
    Tile tile = gameBoard[x, y];
    if (tile.isOpen)
    {
        return;
    }
    tile.isOpen = true;
    if (tile.hasBomb)
    {
        Console.WriteLine("You lost!");
        Console.ReadLine();
        displayGameBoard(gameBoard);
    }
    else
    {
        tile.numberOfBombsAround = countBombsAroundTile(gameBoard, x, y);
        if (tile.numberOfBombsAround == 0)
        {
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int i = 0; i < 8; i++)
            {
                int new_x = x + dx[i];
                int new_y = y + dy[i];
                if (new_x >= 0 && new_x < 5 && new_y >= 0 && new_y < 5)
                {
                    openTile(gameBoard, new_x, new_y);
                }
            }
        }
    }
}