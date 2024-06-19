/*
 * Program.cs --
 *
 * Copyright (c) 2007-2024 by Joe Mistachkin.  All rights reserved.
 *
 * See the file "license.terms" for information on usage and redistribution of
 * this file, and for a DISCLAIMER OF ALL WARRANTIES.
 *
 * RCS: @(#) $Id: $
 */

using System;
using System.Diagnostics;
using System.Threading;

namespace TicTacToe
{
    internal static class Program
    {
        #region Private Constants
        private const int GameDelay = 1000; /* milliseconds */
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Program Entry Point
        private static int Main(
            string[] args /* in: NOT USED */
            )
        {
            int? playerCount = Helpers.GetIntegerFromUser(
                "Please enter number of players (0 to 2): ", 0, 2);

            if (playerCount == null)
            {
                Console.WriteLine("Invalid number of players.");
                return 1;
            }

            int? player1Difficulty = null;
            int? player2Difficulty = null;

            if ((int)playerCount < 2)
            {
                player2Difficulty = Helpers.GetIntegerFromUser(
                    "Please enter player #2 difficulty (0 to 2): ",
                    0, 2);

                if ((int)playerCount < 1)
                {
                    player1Difficulty = Helpers.GetIntegerFromUser(
                        "Please enter player #1 difficulty (0 to 2): ",
                        0, 2);
                }
            }

            int gameId = 0;

            int[] wins = new int[(int)MarkType.Count];

            IGame game = new Game(
                (int)playerCount, player1Difficulty,
                player2Difficulty);

        again:

            Trace.WriteLine(String.Format(
                "New Game #{0}", ++gameId));

            game.Reset();

            do
            {
                /* IGNORED */
                game.Draw();

                MarkType? winner = game.GetWinner();

                if (winner != null)
                {
                    wins[(int)(MarkType)winner]++;

                    Console.WriteLine("The winner is {0}.", winner);

                    Console.WriteLine("X: {0}, O: {1}, Tie: {2}",
                        wins[(int)MarkType.X], wins[(int)MarkType.O],
                        wins[(int)MarkType.None]);

                    break;
                }

                int row;
                int column;

                if (!game.GetRowAndColumn(out row, out column))
                    continue;

                /* IGNORED */
                game.PlaceMark(row, column);
            } while (true);

            if (playerCount == 0)
            {
                Thread.Sleep(GameDelay);

#if DEBUG
                // Console.WriteLine("Press any key to continue...");
                // Console.ReadKey();
#endif

                goto again;
            }

            Console.ReadKey();
            return 0;
        }
        #endregion
    }
}
