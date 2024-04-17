using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Player
    {
        public char Symbol { get; private set; }
        public PlayerType Type { get; private set; }
        public int difficulty { get; private set; }
        public Player(char symbol, PlayerType type, int difficulty= 5)
        {
            Symbol = symbol;
            Type = type;
            this.difficulty = difficulty;
        }

        // Method to make a move on the board
        public void MakeMove(Board board)
        {
            switch (Type)
            {
                case PlayerType.Human:
                    HumanMove(board);
                    break;
                case PlayerType.RandomBot:
                    RandomMove(board);
                    break;
                case PlayerType.SmartBot:
                    SmartMove(board);
                    break;
                default:
                    throw new InvalidOperationException("Invalid player type.");
            }
        }

        // Method for human player to input their move
        private void HumanMove(Board board)
        {
            bool validMove = false;
            do
            {
                Console.WriteLine($"Player {Symbol}, enter your move (column<SPACE>row):");
                string[] input = Console.ReadLine()?.Split();
                if (input != null && input.Length == 2 && int.TryParse(input[0], out int col) && int.TryParse(input[1], out int row))
                {
                    try
                    {
                        board.PlaceSymbol(col, row, Symbol);
                        validMove = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Invalid move: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter two integers separated by space.");
                }
            } while (!validMove);
        }

        // Method for random bot to make a move
        private void RandomMove(Board board)
        {
            Random rand = new Random();
            int width = board.Width;
            int height = board.Height;
            int col, row;
            int power = 0;
 
                do
                {
                    col = rand.Next(width);
                    row = rand.Next(height);
                } while (board.IsCellOccupied(col, row));
                board.PlaceSymbol(col, row, Symbol);
           
           
        }

        // Method for smart bot to make a move
        private void SmartMove(Board board)
        {
            int[] nextMove = FindBestMove(board);

            if (nextMove != null)
            {
                int col = nextMove[1];
                int row = nextMove[0];
                board.PlaceSymbol(col, row, Symbol);
            }
            else
            {
                RandomMove(board);
            }
        }

        private int[] FindBestMove(Board board)
        {
            // Check for immediate winning move
            int[] winningMove = FindImmediateWinningMove(board, Symbol);
            if (winningMove != null)
            {
                return winningMove;
            }

            // Check for immediate blocking move
            int[] blockingMove = FindImmediateWinningMove(board, GetOpponentSymbol(Symbol));
            if (blockingMove != null)
            {
                return blockingMove;
            }

            return null; // No best move found
        }

        private int[] FindImmediateWinningMove(Board board, char symbol)
        {
            Board testBoard = board.Clone();
            // Check rows for immediate winning move
            for (int row = 0; row < testBoard.Height; row++)
            {
                for (int col = 0; col < testBoard.Width; col++)
                {
                    if (!testBoard.IsCellOccupied(col, row))
                    {
                         // Create a clone to test the move
                        testBoard.PlaceSymbol(col, row, symbol);
                        if (CheckWin(testBoard.grid, symbol))
                        {
                            return new int[] { row, col };
                        }
                    }
                }
            }


            return null; // No immediate winning move found
        }
        private bool CheckWin(char[,] board, char symbol)
        {
            // Check rows for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                    return true;
            }

            // Check columns for a win
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == symbol && board[1, j] == symbol && board[2, j] == symbol)
                    return true;
            }

            // Check diagonals for a win
            if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
                return true;
            if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
                return true;

            return false;
        }
        private char GetOpponentSymbol(char symbol)
        {
            return symbol == 'X' ? 'O' : 'X';
        }



    }
}
