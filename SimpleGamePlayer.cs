/*
 * SimpleGamePlayer.cs --
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
    internal class SimpleGamePlayer : RandomGamePlayer
    {
        #region IGamePlayer Members
        public override bool GetRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (GetSimpleRowAndColumn(
                    gameBoard, turn, ref row, ref column))
            {
                return true;
            }

            if (GetCenterRowAndColumn(
                    gameBoard, turn, ref row, ref column))
            {
                return true;
            }

            return base.GetRowAndColumn(
                gameBoard, turn, ref row, ref column);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Protected Methods
        protected bool GetSimpleRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (GetWinningRowAndColumn(
                    gameBoard, turn, ref row, ref column))
            {
                return true;
            }

            MarkType otherTurn = Helpers.GetOtherMarkType(turn);

            if (GetWinningRowAndColumn(
                    gameBoard, otherTurn, ref row, ref column))
            {
                return true;
            }

            return false;
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Methods
        private bool GetWinningRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (gameBoard == null)
                return false;

            int[][] winningMoves = BigData.GetWinningMoves();

            if (winningMoves == null)
                return false;

            foreach (int[] winningMove in winningMoves)
            {
                if (winningMove == null)
                    continue;

                int length = winningMove.Length;
                int minusOne = length - 1;
                int index = 0;

                for (; index < minusOne; index++)
                {
                    if (!gameBoard.IndexToRowAndColumn(
                            winningMove[index],
                            ref row, ref column))
                    {
                        continue;
                    }

                    if (gameBoard.GetMark(
                            row, column) != turn)
                    {
                        break;
                    }
                }

                if (index == minusOne)
                {
                    if (!gameBoard.IndexToRowAndColumn(
                            winningMove[index],
                            ref row, ref column))
                    {
                        continue;
                    }

                    if (gameBoard.GetMark(
                            row, column) == MarkType.None)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        ///////////////////////////////////////////////////////////////////////

        private bool GetCenterRowAndColumn(
            IGameBoard gameBoard, /* in */
            MarkType turn,        /* in */
            ref int row,          /* out */
            ref int column        /* out */
            )
        {
            if (gameBoard == null)
                return false;

            if (!gameBoard.IndexToRowAndColumn(
                    GameBoard.CenterSpace, ref row, ref column))
            {
                return false;
            }

            if (gameBoard.GetMark(row, column) != MarkType.None)
                return false;

            return true;
        }
        #endregion
    }
}
