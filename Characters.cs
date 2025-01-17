/*
 * Characters.cs --
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
    internal static class Characters
    {
        //
        // NOTE: Used to mark the play spaces of the board.
        //
        public const string None = " ";
        public const string X = "X";
        public const string O = "O";

        //
        // NOTE: Unicode characters for double line box (8):
        //
        //       "╔" = U+2554, "╦" = U+2566, "╗" = U+2557
        //       "═" = U+2550, "║" = U+2551, "╠" = U+2560
        //       "╬" = U+256C, "╣" = U+2563, "╚" = U+255A
        //       "╩" = U+2569, "╝" = U+255D, " " = U+0020
        //
        public const string BoxCharacterSet = "╔╦╗═║╠╬╣╚╩╝ ";

        //
        // NOTE: These are the character indexes, within the
        //       box character set above, that correspond to
        //       the needed board space elements.
        //
        public const int Horizontal = 3;
        public const int Vertical = 4;
        public const int Cross = 6;
    }
}
