# Minesweeper Game 

This is a simple command-line implementation of the Minesweeper game written in C#. The game is played on a 5x5 grid, where the player tries to uncover all tiles without hitting a bomb.

## How to Run

1. Make sure you have the .NET SDK installed on your system.

2. Clone the project and utilize it in your preferred C# development environment (e.g., Visual Studio, Visual Studio Code).

3. Build and run the project.

4. Follow the on-screen instructions to play the Minesweeper game.

## How to Play

1. When you run the application, a 5x5 grid will be displayed with all tiles hidden, represented by question marks ("?").

2. The player will be prompted to enter coordinates (x y) to reveal the tile at that position.

3. Enter the coordinates as space-separated integers, where `x` represents the column number (0 to 4), and `y` represents the row number (0 to 4).

4. If the revealed tile contains a bomb, the game will end, and the player will lose. The board will be displayed with an asterisk ("*") representing the bomb's position.

5. If the revealed tile does not contain a bomb, the number of bombs around that tile will be displayed. If there are no bombs around the tile, neighboring tiles will be automatically revealed recursively.

6. Continue revealing tiles until all non-bomb tiles are opened. If successful, the board will be displayed, and the player wins the game.


## Future improvements

1. Custom Board Size: Allow users to create custom game boards with their own dimensions.

2.  Difficulty Levels: Add options for different difficulty levels (easy, medium, hard), allowing players to choose the number of bombs.

3. High Scores: Implement a high-score system to track and display the best performances of players. 