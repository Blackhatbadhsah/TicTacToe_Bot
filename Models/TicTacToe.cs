using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TicTacToe
    {
        private Board board;
        private Player playerX;
        private Player playerO;
        private Player currentPlayer;

        public TicTacToe(Player playerX, Player playerO, int width, int height)
        {
            this.playerX = playerX;
            this.playerO = playerO;
            board = new Board(width,height);
            currentPlayer = playerX;
        }

        public void Play()
        {
            while (!board.IsFull())
            {
                board.Display();
                currentPlayer.MakeMove(board);
                Console.Clear();
                board.Display();
                if (CheckWin(this.board.grid,currentPlayer.Symbol))
                {
                    Console.WriteLine($"Player {currentPlayer.Symbol} wins!");
                    return;
                }
                SwitchPlayer();
            }
            Console.WriteLine("It's a draw!");
        }

        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == playerX ? playerO : playerX;
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

    }
}

