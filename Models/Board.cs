using System;

namespace Models
{
    public class Board
    {
        public char[,] grid; // 2D array to represent the board

        public int Width { get { return grid.GetLength(0); } }
        public int Height { get { return grid.GetLength(1); } }

        // Constructor to initialize the board with specified dimensions
        public Board(int width, int height)
        {
            if (width < 3 || height < 3)
            {
                throw new ArgumentException("Board dimensions must be at least 3x3.");
            }

            // Adjusting height to make it a multiple of 3
            int adjustedHeight = height ;

            grid = new char[width, adjustedHeight];

            // Initialize the board with empty cells
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < adjustedHeight; j++)
                {
                    grid[i, j] = '-';
                }
            }
        }
        public Board Clone()
        {
            Board clonedBoard = new Board(Width, Height);

            // Copy the state of the current board to the cloned board
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    clonedBoard.grid[row, col] = grid[row, col];
                }
            }

            return clonedBoard;
        }

        // Method to display the current state of the board
        public void Display()
        {
            Console.Clear();
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Method to place a symbol (X or O) at the specified position on the board
        public void PlaceSymbol(int column, int row, char symbol)
        {
            if (column < 0 || column >= grid.GetLength(0) || row < 0 || row >= grid.GetLength(1))
            {
                throw new ArgumentException("Invalid position.");
            }

            if (grid[column, row] != '-')
            {
                throw new ArgumentException("Cell already occupied.");
            }

            grid[column, row] = symbol;
        }

        // Method to check if the board is full
        public bool IsFull()
        {
            foreach (char cell in grid)
            {
                if (cell == '-')
                {
                    return false;
                }
            }
            return true;
        }

        // Method to check if a cell is occupied
        public bool IsCellOccupied(int column, int row)
        {
            return grid[column, row] != '-';
        }
    }
}
