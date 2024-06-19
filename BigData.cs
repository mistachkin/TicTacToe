/*
 * BigData.cs --
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
    internal static class BigData
    {
        #region Private Constants
        //
        // NOTE: There are assumptions here:
        //
        //       1. Valid board indexes are between 0 and 8.
        //
        //       2. The ordering of the marked spaces does not
        //          matter, e.g. [0, 1, 2] is treated the same
        //          as [2, 1, 0].
        //
        //       3. It takes exactly three marked spaces to win.
        //
        //       4. The three marked spaces must be in a horizontal
        //          row, a vertical row, or a diagonal row.
        //
        private static readonly int[][] WinningBoards = {
            new int[] { 0, 1, 2 }, /* top horizontal row    */
            new int[] { 3, 4, 5 }, /* middle horizontal row */
            new int[] { 6, 7, 8 }, /* bottom horizontal row */
            new int[] { 0, 3, 6 }, /* left vertical row     */
            new int[] { 1, 4, 7 }, /* middle vertical row   */
            new int[] { 2, 5, 8 }, /* right vertical row    */
            new int[] { 0, 4, 8 }, /* left diagonal row     */
            new int[] { 2, 4, 6 }  /* right diagonal row    */
        };

        ///////////////////////////////////////////////////////////////////////

        private static readonly int[][] WinningMoves = {
            new int[] { 0, 1, 2 }, /* top horizontal row    */
            new int[] { 0, 2, 1 }, /* top horizontal row    */
            new int[] { 0, 3, 6 }, /* left vertical row     */
            new int[] { 0, 4, 8 }, /* left diagonal row     */
            new int[] { 0, 6, 3 }, /* left vertical row     */
            new int[] { 0, 8, 4 }, /* left diagonal row     */
            new int[] { 1, 0, 2 }, /* top horizontal row    */
            new int[] { 1, 2, 0 }, /* top horizontal row    */
            new int[] { 1, 4, 7 }, /* middle vertical row   */
            new int[] { 1, 7, 4 }, /* middle vertical row   */
            new int[] { 2, 0, 1 }, /* top horizontal row    */
            new int[] { 2, 1, 0 }, /* top horizontal row    */
            new int[] { 2, 4, 6 }, /* right diagonal row    */
            new int[] { 2, 5, 8 }, /* right vertical row    */
            new int[] { 2, 6, 4 }, /* right diagonal row    */
            new int[] { 2, 8, 5 }, /* right vertical row    */
            new int[] { 3, 0, 6 }, /* left vertical row     */
            new int[] { 3, 4, 5 }, /* middle horizontal row */
            new int[] { 3, 5, 4 }, /* middle horizontal row */
            new int[] { 3, 6, 0 }, /* left vertical row     */
            new int[] { 4, 0, 8 }, /* left diagonal row     */
            new int[] { 4, 1, 7 }, /* middle vertical row   */
            new int[] { 4, 2, 6 }, /* right diagonal row    */
            new int[] { 4, 3, 5 }, /* middle horizontal row */
            new int[] { 4, 5, 3 }, /* middle horizontal row */
            new int[] { 4, 6, 2 }, /* right diagonal row    */
            new int[] { 4, 7, 1 }, /* middle vertical row   */
            new int[] { 4, 8, 0 }, /* left diagonal row     */
            new int[] { 5, 2, 8 }, /* right vertical row    */
            new int[] { 5, 3, 4 }, /* middle horizontal row */
            new int[] { 5, 4, 3 }, /* middle horizontal row */
            new int[] { 5, 8, 2 }, /* right vertical row    */
            new int[] { 6, 0, 3 }, /* left vertical row     */
            new int[] { 6, 2, 4 }, /* right diagonal row    */
            new int[] { 6, 3, 0 }, /* left vertical row     */
            new int[] { 6, 4, 2 }, /* right diagonal row    */
            new int[] { 6, 7, 8 }, /* bottom horizontal row */
            new int[] { 6, 8, 7 }, /* bottom horizontal row */
            new int[] { 7, 1, 4 }, /* middle vertical row   */
            new int[] { 7, 4, 1 }, /* middle vertical row   */
            new int[] { 7, 6, 8 }, /* bottom horizontal row */
            new int[] { 7, 8, 6 }, /* bottom horizontal row */
            new int[] { 8, 0, 4 }, /* left diagonal row     */
            new int[] { 8, 2, 5 }, /* right vertical row    */
            new int[] { 8, 4, 0 }, /* left diagonal row     */
            new int[] { 8, 5, 2 }, /* right vertical row    */
            new int[] { 8, 6, 7 }, /* bottom horizontal row */
            new int[] { 8, 7, 6 }  /* bottom horizontal row */
        };
        #endregion

        ///////////////////////////////////////////////////////////////////////

        #region Public Methods
        public static int[][] GetWinningBoards()
        {
            return WinningBoards;
        }

        ///////////////////////////////////////////////////////////////////////

        public static int[][] GetWinningMoves()
        {
            return WinningMoves;
        }
        #endregion
    }
}
