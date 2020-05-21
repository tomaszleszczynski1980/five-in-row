using System;
using System.Linq;

namespace FiveInARow
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
            input: Console.Write($"Player {player} please input your move (e.g. a2):");
            var move = Console.ReadLine().ToLower();

            if (move.Length == 0)
            {
                Console.WriteLine("Please input valid coordinates");
                goto input;
            }
            
            char row = move[0];
            string col = move.Substring(1, move.Length - 1);
            int rowNumber = (int)row;
            bool isInt = int.TryParse(col, out int colNumber);
            
            // following magic numbers are: 97 UTF-8 for 'a'
            if (rowNumber < 97 || rowNumber > 96 + Board.GetLength(0) ||
                !isInt || colNumber > Board.GetLength(1))
            { 
                Console.WriteLine("Please input valid coordinates");
                goto input;
            }

            if (Board[rowNumber - 97, colNumber - 1] != 0)
            {
                Console.WriteLine("This field is occupied, please select empty one");
                goto input;
            }
            
            return (rowNumber - 97, colNumber - 1);
        }

        public (int, int) GetAiMove(int player)
        {
            return (0, 0);
        }

        public void Mark(int player, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < Board.GetLength(0) && col < Board.GetLength(1))
            {
                if (Board[row, col] == 0)
                    Board[row, col] = player;
            }
        }

        private static bool Comparer(int[] elementsArray)
        {
            int comparedElements = 0;
            for (int i = 0; i < elementsArray.Length; i++)
            {
                try
                {
                    if (elementsArray[i] != elementsArray[i + 1])
                    { 
                        break;
                    }
                    
                    comparedElements++;
                }
                catch (IndexOutOfRangeException)
                {
                    if (elementsArray[4] == elementsArray[3])
                    {
                        comparedElements++;
                    }
                }
            }
            
            return comparedElements == 5;
        }

        public bool HasWon(int player, int howMany)
        {
            return false;
        }

        public bool IsFull()
        {
            return Board.Cast<int>().Any(element => element == 0);
        }

        public void PrintBoard()
        {

            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    Console.Write(Board[i,j]+" ");
                }
                Console.WriteLine();
            }
        }

        public void EnableAi(int player)
        {
        }

        public void Play(int howMany)
        {
            
            int numberOfPlayers = 0;
            int player = 2;

            while (numberOfPlayers != 1 && numberOfPlayers != 2)
            {
                Console.Write("Enter number of players: ");
                int.TryParse(Console.ReadLine(), out numberOfPlayers);
            }


            if (numberOfPlayers == 1)
            {
                EnableAi(2);
            }
            
            while (!HasWon(1, howMany) && !HasWon(2, howMany) && !IsFull())
            {
                player = player == 1 ? 2 : 1;
                
                var coords = numberOfPlayers == 1 ? GetAiMove(player) : GetMove(player);

                Mark(player, coords.Item1, coords.Item2 );
                
                PrintBoard();
            }
            
            PrintResult(player);
            
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