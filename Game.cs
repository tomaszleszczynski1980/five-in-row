using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FiveInARow
{
    public class Game : IGame
    {
        public int[,] Board { get; set; }

        public Game(int nRows, int nCols)
        {

            Board = new int[nRows + 1, nCols + 1];

            for (int i = 0; i <= nRows; i++)
            {
                for (int j = 0; j <= nCols; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }

        public (int, int) GetMove(int player)
        {
            char sign = player == 1 ? 'X' : 'O';
            
            Console.WriteLine("");
            input: 
            Console.Write($"Player {player} ({sign}) please input your move (e.g. a2) /quit to exit/:");
            var move = Console.ReadLine().ToLower();
            
            if (move == "quit" || move == "exit")
                Environment.Exit(0);

            if (move.Length == 0)
            {
                PrintBoard();
                Console.WriteLine("");
                Console.WriteLine("Please input valid coordinates");
                goto input;
            }
            
            char row = move[0];
            string col = move.Substring(1, move.Length - 1);
            int rowNumber = (int)row;
            bool isInt = int.TryParse(col, out int colNumber);

            // following magic numbers are: 97 UTF-8 for 'a'
            if (rowNumber < 97 || rowNumber > 96 + Board.GetLength(0) - 1 ||
                !isInt || colNumber > Board.GetLength(1) - 1)
            {
                PrintBoard();
                Console.WriteLine("");
                Console.WriteLine("Please input valid coordinates");
                goto input;
            }

            if (Board[rowNumber - 97, colNumber - 1] != 0)
            {
                PrintBoard();
                Console.WriteLine("");
                Console.WriteLine($"Field {move} is occupied, please select empty one");
                goto input;
            }
            
            return (rowNumber - 97, colNumber - 1);
        }

        // this is the most stupid AI one can ever imagine - it takes random empty field
        public (int, int) GetAiMove(int player)
        {
            List<Tuple<int, int>> emptyFields = new List<Tuple<int, int>>();
            
            for (int row = 0; row < Board.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < Board.GetLength(1) - 1; col++)
                {
                    if (Board[row, col] == 0)
                        emptyFields.Add(new Tuple<int, int>(row, col));
                }
            }
            
            var random = new Random();
            int index = random.Next(emptyFields.Count - 1);
            var randomField = emptyFields[index];

            Console.WriteLine("");
            Console.Write("Computer moves");

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(350);
                Console.Write(".");
                Thread.Sleep(350);
            }
            
            
            return (randomField.Item1, randomField.Item2);
        }

        public void Mark(int player, int row, int col)
        {
            if (row >= 0 && col >= 0 && row < Board.GetLength(0) - 1 && col < Board.GetLength(1) - 1)
            {
                if (Board[row, col] == 0)
                    Board[row, col] = player;
            }
        }

        private static bool Comparer(int[] elementsArray)
        {
            int first = elementsArray[0];

            return elementsArray.All(element => element == first);
        }

        public bool HasWon(int player, int howMany, (int, int) coords)
        {
            int[] myArray = new int[howMany];
            
            // Horizontal check
            for (int c = -howMany + 1; c <= 0; c++)        
            {
                for (int i = 0; i <= howMany - 1; i++)
                {
                    try
                    {
                        myArray[i] = Board[coords.Item1, coords.Item2 + c + i];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                    if (coords.Item2 == Board.GetLength(1) - 1)
                    {
                        break;
                    }
                }

                if (myArray.All(e => e == 0)) continue;
                if (Comparer(myArray))
                {
                    return true;
                }
            }
            // Horizontal check
            
            // Vertical check
            for (int c = -howMany + 1; c <= 0; c++)
            {
                for (int i = 0; i <= howMany - 1; i++)
                {
                    try
                    {
                        myArray[i] = Board[coords.Item1 + c + i, coords.Item2];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }

                    if (coords.Item1 == Board.GetLength(0) - 1)
                    {
                        break;
                    }
                }
            
                if (myArray.All(e => e == 0)) continue;
                if (Comparer(myArray))
                {
                    return true;
                }
            }
            // Vertical check
            
            // Diagonally (left higher ; right lower) check
            for (int c = -howMany + 1; c <= 0; c++)
            { 
                for (int i = 0; i <= howMany - 1; i++) 
                {
                    try
                    {
                        myArray[i] = Board[coords.Item1 + c + i, coords.Item2 + c + i];
                    }
                    catch (IndexOutOfRangeException)
                    { 
                        break;
                    }
                }

                if (myArray.All(e => e == 0)) continue;
                if (Comparer(myArray))
                { 
                    return true;
                }
            }
            // Diagonally (left higher ; right lower) check
            
            // Diagonally (left lower ; right higher) check
            // TODO: in this loop bug occurs (e1 - k1 wins one piece only)!
            for (int c = -howMany + 1; c <= 0; c++)
            {
                for (int i = 0; i <= howMany - 1; i++)
                {
                    try
                    {
                        myArray[i] = Board[coords.Item1 + c + i, coords.Item2 - c - i];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                }

                if (coords.Item2 == 0)
                {
                    break;
                }

            if (myArray.All(e => e == 0)) continue;
                if (Comparer(myArray))
                {
                    return true;
                }
            }
            // Diagonally (left lower ; right higher) check
            
            return false;
        }

        public bool IsNotFull()
        {
            for (int row = 0; row < Board.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < Board.GetLength(1) - 1; col++)
                {
                    if (Board[row, col] == 0)
                        return true;
                }
            }

            return false;
            // return Board.Cast<int>().Any(element => element == 0);
        }

        public void PrintBoard()
        {
            string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            Console.Clear();

            Console.Write(" ");
            for (int i = 1; i <= Board.GetLength(1) - 1; i++)
            {
                if (i <= 9)
                {
                    Console.Write("  " + i);
                }
                else
                {
                    Console.Write(" " + i);
                }
            }
            Console.WriteLine();

            for (int i = 0; i < Board.GetLength(0) - 1; i++)
            {
                Console.Write(letters[i]);
                char print;
                for (int j = 0; j < Board.GetLength(1) - 1; j++)
                {
                    switch (Board[i, j])
                    {
                        case 1:
                            print = 'X';
                            break;
                        case 2:
                            print = 'O';
                            break;
                        default:
                            print = '.';
                            break;
                    }
                    Console.Write("  " + print);
                }
                Console.WriteLine();

            }
        }   

        public void EnableAi(int player)
        {
            Console.WriteLine("Human player (X) starts, Computer (O) plays next");
        }

        public void Play(int howMany)
        {
            
            int numberOfPlayers = 0;
            int player = 2;
            (int, int) coords;
            
            while (numberOfPlayers != 1 && numberOfPlayers != 2)
            {
                Console.Clear();
                Console.Write("Enter number of players (1 - human vs computer, 2 - human vs human): ");
                int.TryParse(Console.ReadLine(), out numberOfPlayers);
            }
            
            if (numberOfPlayers == 1)
            {
                EnableAi(2);
            }

            do
            {
                PrintBoard();

                player = player == 1 ? 2 : 1;

                coords = numberOfPlayers == 1 && player == 2 ? GetAiMove(player) : GetMove(player);

                Mark(player, coords.Item1, coords.Item2);
                
                PrintBoard();
                
                if (numberOfPlayers == 1)
                    Thread.Sleep(500);
                
            } while (!HasWon(player, howMany, coords) && IsNotFull());
            
            PrintResult(player);
            
            Console.Write("Enter anything to quit (or just press enter)");
            Console.ReadLine();
        }
        
        public void PrintResult(int player)
        {
            char sign = player == 1 ? 'X' : 'O';
            Console.WriteLine("");
            
            if (IsNotFull())
                Console.WriteLine($"Player {player} ({sign}) won!");
            else
                Console.WriteLine("It is a tie");
            
            Console.WriteLine("");
        }
        }
    }

    /* DO NOT CHANGE THIS INTERFACE! It will be used to test your solution. */
    public interface IGame
    {
        int[,] Board { get; set; }
        (int, int) GetMove(int player);
        (int, int) GetAiMove(int player);
        void Mark(int player, int row, int col);
        bool HasWon(int player, int howMany, (int, int) coords);
        bool IsNotFull();
        void PrintBoard();
        void PrintResult(int player);
        void EnableAi(int player);
        void Play(int howMany);
    }