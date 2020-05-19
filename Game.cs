using System;

namespace five_in_a_row
{
    public class Game : IGame
    {
        public int[,] Board { get; set; }

        public Game(int nRows, int nCols)
        {
            Board = new int[nRows, nCols];

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    Board[i, j] = 0;
                }
            }
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
            
            int numberOfPlayers = 0;
            int counter = 1;
            int player;

            while (numberOfPlayers != 1 && numberOfPlayers != 2)
            {
                Console.Write("Enter number of players: ");
                int.TryParse(Console.ReadLine(), out numberOfPlayers);
            }


            if (numberOfPlayers == 1) {
                EnableAi(2);
            }
            
            while (!HasWon(1, howMany) && !HasWon(2, howMany) && !IsFull())
            {
                player = counter % 2 == 1 ? 1 : 2;  // if (counter % 2 == 1) player = 1 else player = 2;

                if (numberOfPlayers == 2) {
                    var coords = GetAiMove(player);
                }
                else {
                    var coords = GetMove(player);
                }
                
                //Mark(player, coords, coords );
            }
            
            PrintBoard();
            
            Console.Write("Enter any key to quit: ");
            Console.ReadLine();
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