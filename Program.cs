using System;

namespace FiveInARow
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(11, 11);
            game.Play(5);
        }
    }
}