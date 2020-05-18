using System;

namespace five_in_a_row
{
    public class Game : IGame
    {
        public int[,] Board { get; set; }

        public Game(int nRows, int nCols)
        {
        }

        public (int, int) GetMove(int player)
        {
            return (0, 0);
        }

        public (int, int) GetAiMove(int player)
        {
            return (0, 0);
        }

        public void Mark(int player, int row, int col)
        {
        }

        public bool HasWon(int player, int howMany)
        {
            return false;
        }

        public bool IsFull()
        {
            return false;
        }

        public void PrintBoard()
        {
        }

        public void EnableAi(int player)
        {
        }

        public void Play(int howMany)
        {
        }

        public void PrintResult(int player)
        {
        }
    }

    /* DO NOT CHANGE THIS INTERFACE! It will be used to test your solution. */
    public interface IGame
    {
        int[,] Board { get; set; }
        (int, int) GetMove(int player);
        (int, int) GetAiMove(int player);
        void Mark(int player, int row, int col);
        bool HasWon(int player, int howMany);
        bool IsFull();
        void PrintBoard();
        void PrintResult(int player);
        void EnableAi(int player);
        void Play(int howMany);
    }
}