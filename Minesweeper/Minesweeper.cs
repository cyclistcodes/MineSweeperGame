using System;

namespace Minesweeper;

//An individual tile on the game board
class Tile
{
    public int x;
    public int y;
    public bool hasBomb;
    public bool isOpen;
    public int numberOfBombsAround;
}

// The main class for the Minesweeper game.
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

    // Checks if all non-bomb tiles are opened on the game board.
    static int countBombsAroundTile(Tile[,] gameBoard, int x, int y)
{
    int bombCount = 0;
    int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
    int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
    for (int i = 0; i < 8; i++)
    {
        int new_x = x + dx[i];
        int new_y = y + dy[i];
        if (new_x >= 0 && new_x < 5 && new_y >= 0 && new_y < 5 && gameBoard[new_x, new_y].hasBomb)
        {
            bombCount++;
        }
    }
    return bombCount;
}

static bool areAllTilesOpened(Tile[,] gameBoard)
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

static void Main(string[] args)
{
    Tile[,] gameBoard = createGameBoard();

    gameBoard[0, 0].hasBomb = true;
    gameBoard[0, 1].hasBomb = true;
    gameBoard[1, 1].hasBomb = true;
    gameBoard[1, 4].hasBomb = true;
    gameBoard[4, 2].hasBomb = true;


    while (true)
    {
        displayGameBoard(gameBoard);
        Console.Write("Enter coordinates (x y): ");
        string input = Console.ReadLine();
        string[] coordinates = input.Split(' ');

        if (coordinates.Length == 2 && int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y))
        {
            if (x >= 0 && x < 5 && y >= 0 && y < 5)
            {
                openTile(gameBoard, x, y);

                if (areAllTilesOpened(gameBoard))
                {
                    displayGameBoard(gameBoard);

                    Console.WriteLine("Congratulations! You won!");
                    Console.ReadLine();
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid coordinates. Try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter two integers separated by a space.");
        }
    }
}
}