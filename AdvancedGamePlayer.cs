/*
 * AdvancedGamePlayer.cs --
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
    internal sealed class AdvancedGamePlayer : SimpleGamePlayer
    {
        #region IGamePlayer Members
        public override bool GetRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (base.GetSimpleRowAndColumn(
                    gameBoard, turn, ref row, ref column))
            {
                return true;
            }

            if (GetBestRowAndColumn(
                    gameBoard, turn, ref row, ref column))
            {
                return true;
            }

            return base.GetRowAndColumn(
                gameBoard, turn, ref row, ref column);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Methods
        private void UpdateStatistics(
            IGameBoard gameBoard, /* in */
            int[] winningBoard,   /* in */
            int[] noneCounts,     /* in */
            int[] turnCounts,     /* in */
            bool hasNone,         /* in */
            bool hasTurn,         /* in */
            ref int? maximumIndex /* in, out */
            )
        {
            if (gameBoard == null)
                return;

            if (winningBoard == null)
                return;

            int length = winningBoard.Length;

            if (noneCounts == null)
                return;

            if (turnCounts == null)
                return;

            int size = noneCounts.Length;

            if (turnCounts.Length != size)
                return;

            for (int index = 0; index < length; index++)
            {
                int winningIndex = winningBoard[index];

                if ((winningIndex >= 0) &&
                    (winningIndex < size))
                {
                    if (hasNone)
                        noneCounts[winningIndex]++;

                    if (hasTurn)
                        turnCounts[winningIndex]++;
                }
            }

            for (int index = 0; index < size; index++)
            {
                int maximumCount = 0;

                if (maximumIndex != null)
                {
                    maximumCount += noneCounts[(int)maximumIndex];
                    maximumCount += turnCounts[(int)maximumIndex];
                }

                int count = noneCounts[index];

                count += turnCounts[index];

                if (count > maximumCount)
                {
                    int row = 0;
                    int column = 0;

                    if (gameBoard.IndexToRowAndColumn(
                            index, ref row, ref column) &&
                        (gameBoard.GetMark(
                            row, column) == MarkType.None))
                    {
                        maximumIndex = index;
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////

        private bool GetBestRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (gameBoard == null)
                return false;

            int size = gameBoard.Size;

            if (size == 0)
                return false;

            int[][] winningBoards = BigData.GetWinningBoards();

            if (winningBoards == null)
                return false;

            int? maximumIndex = null;

            int[] noneCounts = new int[size];
            int[] turnCounts = new int[size];

            foreach (int[] winningBoard in winningBoards)
            {
                if (winningBoard == null)
                    continue;

                bool hasTurn = false;
                bool hasNone = false;

                int length = winningBoard.Length;

                for (int index = 0; index < length; index++)
                {
                    if (!gameBoard.IndexToRowAndColumn(
                            winningBoard[index],
                            ref row, ref column))
                    {
                        break;
                    }

                    MarkType markType = gameBoard.GetMark(
                        row, column);

                    if (markType == MarkType.None)
                    {
                        hasNone = true;
                        continue;
                    }

                    if (markType == turn)
                    {
                        hasTurn = true;
                        continue;
                    }

                    hasNone = false;
                    hasTurn = false;

                    break;
                }

                if (hasNone || hasTurn)
                {
                    UpdateStatistics(gameBoard,
                        winningBoard, noneCounts, turnCounts,
                        hasNone, hasTurn, ref maximumIndex);
                }
            }

            if (maximumIndex == null)
                return false;

            if (!gameBoard.IndexToRowAndColumn(
                    (int)maximumIndex, ref row, ref column))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
