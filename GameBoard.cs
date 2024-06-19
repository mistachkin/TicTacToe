/*
 * GameBoard.cs --
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

namespace TicTacToe
{
    #region IGameBoard Interface
    internal interface IGameBoard
    {
        int Rows { get; }
        int Columns { get; }

        int Size { get; }

        bool IsFull(
            bool @default /* in */
        );

        bool Clear();
        bool Draw();

        bool IndexToRowAndColumn(
            int index,     /* in */
            ref int row,   /* out */
            ref int column /* out */
        );

        MarkType GetMark(
            int row,   /* in */
            int column /* in */
        );

        MarkType SetMark(
            int row,          /* in */
            int column,       /* in */
            MarkType markType /* in */
        );

        MarkType? GetWinner();
    }
    #endregion

    ///////////////////////////////////////////////////////////////////////////

    #region GameBoard Class
    internal sealed class GameBoard : IGameBoard
    {
        #region Private Constants
        //
        // NOTE: There are assumptions here:
        //
        //       1. Valid board indexes are between 0 and 8.
        //
        private const int DefaultColumns = 3;
        private const int DefaultRows = 3;
        private const int DefaultSize = DefaultColumns * DefaultRows;

        ///////////////////////////////////////////////////////////////////////

        //
        // NOTE: There are assumptions here:
        //
        //       1. Valid board indexes are between 0 and 8.
        //
        private const int TopMiddleSpace = 1;
        private const int BottomMiddleSpace = 7;
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Public Constants
        //
        // NOTE: There are assumptions here:
        //
        //       1. Valid board indexes are between 0 and 8.
        //
        public const int CenterSpace = 4;
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Data
        //
        // NOTE: There are assumptions here:
        //
        //       1. Valid board indexes are between 0 and 8.
        //
        //       2. All board spaces should initially be empty.
        //
        private readonly MarkType[] board = new MarkType[] {
            MarkType.None, MarkType.None, MarkType.None,
            MarkType.None, MarkType.None, MarkType.None,
            MarkType.None, MarkType.None, MarkType.None
        };
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Public Constructors
        public GameBoard()
        {
            // do nothing.
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region IGameBoard Members
        public int Rows
        {
            get { return Size / Columns; }
        }

        ///////////////////////////////////////////////////////////////////////

        public int Columns
        {
            get { return DefaultColumns; }
        }

        ///////////////////////////////////////////////////////////////////////

        public int Size
        {
            get { return (board != null) ? board.Length : 0; }
        }

        ///////////////////////////////////////////////////////////////////////

        public bool IsFull(
            bool @default /* in */
            )
        {
            return IsFull(board, @default);
        }

        ///////////////////////////////////////////////////////////////////////

        public bool Clear()
        {
            if (board == null)
                return false;

            int length = board.Length;

            for (int index = 0; index < length; index++)
                board[index] = MarkType.None;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        public bool Draw()
        {
            Console.Clear();
            Console.WriteLine();

            int rows = this.Rows;
            int columns = this.Columns;

            for (int row = 0; row < rows; row++)
            {
                /* IGNORED */
                Draw(row);

                for (int column = 0; column < columns; column++)
                {
                    int? index = GetIndex(row, column);

                    if (index == null)
                        return false;

                    /* IGNORED */
                    Draw((int)index, board[(int)index]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        public bool IndexToRowAndColumn(
            int index,     /* in */
            ref int row,   /* out */
            ref int column /* out */
            )
        {
            if (board == null)
                return false;

            int length = board.Length;

            if (length == 0)
                return false;

            if ((index < 0) || (index >= length))
                return false;

            int columns = this.Columns;

            if (columns == 0)
                return false;

            row = index / columns;
            column = index % columns;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        public MarkType GetMark(
            int row,   /* in */
            int column /* in */
            )
        {
            int? index;
            string message;
            string paramName;

            index = GetIndex(row, column, out message, out paramName);

            if (index == null)
                throw new ArgumentException(message, paramName);

            if (board == null)
                throw new InvalidOperationException();

            int length = board.Length;

            if (((int)index < 0) || ((int)index >= length))
                throw new ArgumentException("index is out-of-bounds");

            return board[(int)index];
        }

        ///////////////////////////////////////////////////////////////////////

        public MarkType SetMark(
            int row,          /* in */
            int column,       /* in */
            MarkType markType /* in */
            )
        {
            int? index;
            string message;
            string paramName;

            index = GetIndex(row, column, out message, out paramName);

            if (index == null)
                throw new ArgumentException(message, paramName);

            int length = board.Length;

            if (((int)index < 0) || ((int)index >= length))
                throw new ArgumentException("index is out-of-bounds");

            MarkType oldMarkType = board[(int)index];

            board[(int)index] = markType;

            Trace.WriteLine(String.Format(
                "{0} marked at ({1}, {2}).", markType, row, column));

            return oldMarkType;
        }

        ///////////////////////////////////////////////////////////////////////

        public MarkType? GetWinner()
        {
            return GetWinner(board);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Methods
        private static bool IsFull(
            MarkType[] board, /* in */
            bool @default     /* in */
            )
        {
            if (board == null)
                return @default;

            int length = board.Length;

            for (int index = 0; index < length; index++)
                if (board[index] == MarkType.None)
                    return false;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        private int? GetIndex(
            int row,   /* in */
            int column /* in */
            )
        {
            string message;
            string paramName;

            return GetIndex(row, column, out message, out paramName);
        }

        ///////////////////////////////////////////////////////////////////////

        private int? GetIndex(
            int row,             /* in */
            int column,          /* in */
            out string message,  /* out */
            out string paramName /* out */
            )
        {
            message = null;
            paramName = null;

            int rows = this.Rows;

            if ((row < 0) || (row >= rows))
            {
                message = String.Format(
                    "row {0} is out-of-bounds [{1}, {2}]",
                    row, 0, rows);

                paramName = "row";

                return null;
            }

            int columns = this.Columns;

            if ((column < 0) || (column >= columns))
            {
                message = String.Format(
                    "column {0} is out-of-bounds [{1}, {2}]",
                    column, 0, columns);

                paramName = "column";

                return null;
            }

            return (row * columns) + column;
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Static Methods
        private static void GetRowStrings(
            out char? horizontal, /* out */
            out char? cross       /* out */
            )
        {
            horizontal = null;
            cross = null;

            string boxCharacterSet = Characters.BoxCharacterSet;

            if (boxCharacterSet != null)
            {
                int length = boxCharacterSet.Length;
                int boxIndex1 = Characters.Horizontal;

                if ((boxIndex1 < 0) || (boxIndex1 >= length))
                    return;

                int boxIndex2 = Characters.Cross;

                if ((boxIndex2 < 0) || (boxIndex2 >= length))
                    return;

                horizontal = boxCharacterSet[boxIndex1];
                cross = boxCharacterSet[boxIndex2];
            }
        }

        ///////////////////////////////////////////////////////////////////////

        private static string GetMarkString(
            MarkType markType /* in */
            )
        {
            switch (markType)
            {
                case MarkType.None:
                    return Characters.None;
                case MarkType.X:
                    return Characters.X;
                case MarkType.O:
                    return Characters.O;
                default:
                    return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////

        private static char? GetWrapString(
            int index /* in */
            )
        {
            if ((index == TopMiddleSpace) ||
                (index == CenterSpace) ||
                (index == BottomMiddleSpace))
            {
                string boxCharacterSet = Characters.BoxCharacterSet;

                if (boxCharacterSet != null)
                {
                    int length = boxCharacterSet.Length;
                    int boxIndex = Characters.Vertical;

                    if ((boxIndex >= 0) && (boxIndex < length))
                        return boxCharacterSet[boxIndex];
                }
            }

            return null;
        }

        ///////////////////////////////////////////////////////////////////////

        private static bool Draw(
            int row /* in */
            )
        {
            //
            // NOTE: This assumes that the board is exactly
            //       three spaces across.
            //
            if (row > 0)
            {
                char? horizontal;
                char? cross;

                GetRowStrings(out horizontal, out cross);

                if ((horizontal != null) && (cross != null))
                {
                    Console.WriteLine(
                        "{0}{1}{2}{3}{4}", horizontal, cross,
                        horizontal, cross, horizontal);

                    return true;
                }
            }

            return false;
        }

        ///////////////////////////////////////////////////////////////////////

        private static bool Draw(
            int index,        /* in */
            MarkType markType /* in */
            )
        {
            string markString = GetMarkString(markType);

            if (markString == null)
                return false;

            char? wrap = GetWrapString(index);

            if (wrap != null)
                Console.Write(wrap);

            Console.Write(markString);

            if (wrap != null)
                Console.Write(wrap);

            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        private static bool CanBeWinner(
            MarkType? markType /* in */
            )
        {
            if (markType == null)
                return false;

            MarkType localMarkType = (MarkType)markType;

            if (localMarkType == MarkType.None)
                return false;

            return true;
        }

        ///////////////////////////////////////////////////////////////////////

        private static MarkType? GetWinner(
            MarkType[] board /* in */
            )
        {
            if (board == null)
                return null;

            int[][] winningBoards = BigData.GetWinningBoards();

            if (winningBoards != null)
            {
                foreach (int[] winningBoard in winningBoards)
                {
                    if (winningBoard == null)
                        continue;

                    MarkType? winner = null;

                    foreach (int index in winningBoard)
                    {
                        if (winner == null)
                        {
                            winner = board[index];
                        }
                        else if (winner != board[index])
                        {
                            winner = null;
                            break;
                        }
                    }

                    if (CanBeWinner(winner))
                        return winner;
                }
            }

            return IsFull(board, false) ?
                MarkType.None /* TIE */ :
                (MarkType?)null /* UNKNOWN */;
        }
        #endregion
    }
    #endregion
}
