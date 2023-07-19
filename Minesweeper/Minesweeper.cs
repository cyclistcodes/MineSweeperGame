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
}
