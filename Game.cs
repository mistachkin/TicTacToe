/*
 * Game.cs --
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
    #region IGame Interface
    internal interface IGame
    {
        bool GetRowAndColumn(
            out int row,   /* out */
            out int column /* out */
        );

        bool Reset();

        bool Draw();

        MarkType? PlaceMark(
            int row,   /* in */
            int column /* in */
        );

        MarkType? GetWinner();
    }
    #endregion

    ///////////////////////////////////////////////////////////////////////////

    #region Game Class
    internal sealed class Game : IGame
    {
        #region Private Data
        //
        // NOTE: This is used to interact with the game board.
        //
        private IGameBoard gameBoard;

        ///////////////////////////////////////////////////////////////////////

        //
        // NOTE: These represent the users playing the game.
        //
        private IGamePlayer player1;
        private IGamePlayer player2;

        ///////////////////////////////////////////////////////////////////////

        //
        // NOTE: There are assumptions here:
        //
        //       1. The player marking "X" always goes first.
        //
        private MarkType turn = MarkType.X;
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Constructors
        private Game()
        {
            this.gameBoard = new GameBoard();
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Public Constructors
        public Game(
            int playerCount,        /* in */
            int? player1Difficulty, /* in */
            int? player2Difficulty  /* in */
            )
            : this()
        {
            switch (playerCount)
            {
                case 0:
                    {
                        player1 = Helpers.CreateGamePlayer(player1Difficulty);
                        player2 = Helpers.CreateGamePlayer(player2Difficulty);
                        break;
                    }
                case 1:
                    {
                        player1 = Helpers.CreateGamePlayer(null);
                        player2 = Helpers.CreateGamePlayer(player2Difficulty);
                        break;
                    }
                case 2:
                    {
                        player1 = Helpers.CreateGamePlayer(null);
                        player2 = Helpers.CreateGamePlayer(null);
                        break;
                    }
            }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region IGame Members
        public bool GetRowAndColumn(
            out int row,   /* out */
            out int column /* out */
            )
        {
            row = 0;
            column = 0;

            switch (turn)
            {
                case MarkType.None:
                    {
                        return false;
                    }
                case MarkType.X:
                    {
                        if (player1 == null)
                            return false;

                        return player1.GetRowAndColumn(
                            gameBoard, turn, ref row, ref column);
                    }
                case MarkType.O:
                    {
                        if (player2 == null)
                            return false;

                        return player2.GetRowAndColumn(
                            gameBoard, turn, ref row, ref column);
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        ///////////////////////////////////////////////////////////////////////

        public bool Reset()
        {
            turn = MarkType.X;

            if (gameBoard == null)
                return false;

            return gameBoard.Clear();
        }

        ///////////////////////////////////////////////////////////////////////

        public bool Draw()
        {
            if (gameBoard == null)
                return false;

            return gameBoard.Draw();
        }

        ///////////////////////////////////////////////////////////////////////

        public MarkType? PlaceMark(
            int row,   /* in */
            int column /* in */
            )
        {
            if (gameBoard == null)
                return null;

            MarkType markType = gameBoard.GetMark(row, column);

            if (markType != MarkType.None)
                return null;

            return gameBoard.SetMark(row, column, NextTurn());
        }

        ///////////////////////////////////////////////////////////////////////

        public MarkType? GetWinner()
        {
            if (gameBoard == null)
                return null;

            return gameBoard.GetWinner();
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Private Methods
        private MarkType NextTurn()
        {
            MarkType oldTurn = turn;

            turn = Helpers.GetOtherMarkType(oldTurn);

            return oldTurn;
        }
        #endregion
    }
    #endregion
}
