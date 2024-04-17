using System;

namespace TicTacToe
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("enter 1 for dumb bot else continue");
            var data = Console.ReadLine();
            // Creating a TicTacToe game with one human player
            Models.Player humanPlayer = new Models.Player('X', Models.PlayerType.Human);
            Models.Player botPlayer = new Models.Player('O', data == "1" ? Models.PlayerType.RandomBot : Models.PlayerType.SmartBot);
            Models.TicTacToe game = new Models.TicTacToe(humanPlayer, botPlayer, 3, 3);

            // Start the game

            Console.WriteLine("You are playing as X. Make your move by entering the column and row numbers.");

            game.Play();

            Console.WriteLine("Game over. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
