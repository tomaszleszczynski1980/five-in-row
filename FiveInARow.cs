using System;

namespace five_in_a_row
{
    public class FiveInARow
    {
        public static void Main(string[] args)
        {
            var game = new Game(11, 11);
            game.Play(5);
        }
    }
}