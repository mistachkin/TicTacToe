/*
 * GamePlayer.cs --
 *
 * Copyright (c) 2007-2024 by Joe Mistachkin.  All rights reserved.
 *
 * See the file "license.terms" for information on usage and redistribution of
 * this file, and for a DISCLAIMER OF ALL WARRANTIES.
 *
 * RCS: @(#) $Id: $
 */

namespace TicTacToe
{
    internal interface IGamePlayer
    {
        bool GetRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
        );
    }
}
