/*
 * Helpers.cs --
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
    internal static class Helpers
    {
        #region Public Methods
        public static IGamePlayer CreateGamePlayer(
            int? difficulty /* in */
            )
        {
            if (difficulty == null)
                return new InteractiveGamePlayer();

            switch ((int)difficulty)
            {
                case 1:
                    {
                        return new SimpleGamePlayer();
                    }
                case 2:
                    {
                        return new AdvancedGamePlayer();
                    }
                default:
                    {
                        return new RandomGamePlayer();
                    }
            }
        }

        ///////////////////////////////////////////////////////////////////////

        public static int? GetIntegerFromUser(
            string prompt, /* in */
            int? minimum,  /* in */
            int? maximum   /* in */
            )
        {
            if (!String.IsNullOrEmpty(prompt))
                Console.Write(prompt);

            string line = Console.ReadLine();

            if (String.IsNullOrEmpty(line))
                return null;

            line = line.Trim();

            if (String.IsNullOrEmpty(line))
                return null;

            int value;

            if (!int.TryParse(line, out value))
                return null;

            if ((minimum != null) && (value < (int)minimum))
                return null;

            if ((maximum != null) && (value > (int)maximum))
                return null;

            return value;
        }

        ///////////////////////////////////////////////////////////////////////

        public static MarkType GetOtherMarkType(
            MarkType markType /* in */
            )
        {
            switch (markType)
            {
                case MarkType.X:
                    {
                        return MarkType.O;
                    }
                case MarkType.O:
                    {
                        return MarkType.X;
                    }
                default:
                    {
                        return MarkType.None;
                    }
            }
        }
        #endregion
    }
}
