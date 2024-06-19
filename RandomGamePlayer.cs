/*
 * RandomGamePlayer.cs --
 *
 * Copyright (c) 2007-2024 by Joe Mistachkin.  All rights reserved.
 *
 * See the file "license.terms" for information on usage and redistribution of
 * this file, and for a DISCLAIMER OF ALL WARRANTIES.
 *
 * RCS: @(#) $Id: $
 */

using System;

namespace TicTacToe
{
    internal class RandomGamePlayer : IGamePlayer
    {
        #region Private Data
        //
        // NOTE: This is the random number generator used to pick
        //       an initial search location for computer selected
        //       moves.
        //
        private Random random = new Random(Environment.TickCount);
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region IGamePlayer Members
        public virtual bool GetRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (gameBoard == null)
                return false;

            int rows = gameBoard.Rows;
            int columns = gameBoard.Columns;

            while (true)
            {
                if (gameBoard.IsFull(true))
                    break;

                row = random.Next(0, rows - 1);

                for (; row < rows; row++)
                {
                    column = random.Next(0, columns - 1);

                    for (; column < columns; column++)
                    {
                        if (gameBoard.GetMark(
                                row, column) == MarkType.None)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        #endregion
    }
}
