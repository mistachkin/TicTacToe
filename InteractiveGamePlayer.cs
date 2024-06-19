/*
 * InteractiveGamePlayer.cs --
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
    internal sealed class InteractiveGamePlayer : IGamePlayer
    {
        #region IGamePlayer Members
        public bool GetRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (gameBoard == null)
                return false;

            int rows = gameBoard.Rows;

            int? localRow = Helpers.GetIntegerFromUser(String.Format(
                "Please enter row to mark for \"{0}\" (1 to {1}): ",
                turn, rows), 1, rows);

            if (localRow == null)
                return false;

            row = (int)localRow - 1; /* ZERO BASED */

            int columns = gameBoard.Columns;

            int? localColumn = Helpers.GetIntegerFromUser(String.Format(
                "Please enter column to mark for \"{0}\" (1 to {1}): ",
                turn, columns), 1, columns);

            if (localColumn == null)
                return false;

            column = (int)localColumn - 1; /* ZERO BASED */

            return true;
        }
        #endregion
    }
}
