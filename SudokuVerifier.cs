using System;
using System.Collections.Generic;

namespace Sudoku
{
    /// <summary>
    /// Class containing methods to determine the validity of Sudoku boards
    /// </summary>
    public class SudokuVerifier
    {
        /**
         * Each row has numbers between 1 and 9, no repeats.
         * Each col has numbers between 1 and 9, no repeats.
         * Each 3x3 has numbers between 1 and 9, no repeats.
         */
        const int NUM_COLUMNS = 9;
        const int NUM_ROWS = NUM_COLUMNS;

        /// <summary>
        /// Verifies that a "line" ie column/row has unique numbers 1-9
        /// </summary>
        /// <param name="board">2D array representing board to check</param>
        /// <param name="key1">Key to go by first (0) for rows, (1) for cols</param>
        /// <param name="key2">Key to check secondly.  Ie key1=0, key2=1 checks columns by row.  Key1=1, key2=0 checks rows by column</param>
        /// <returns>true if the "line" has unique numbers 1-9</returns>
        static bool CheckLine(int[,] board, int key1, int key2)
        {
            //Fill up lists to use for checking valid entries in each row
            List<int> NumberCheckerTemplate = new List<int>(NUM_COLUMNS);
            for (int i = 1; i <= NUM_COLUMNS; i++)
            {
                NumberCheckerTemplate.Add(i);
            }

            //Check the rows for unique numbers 1-9
            for (int i = 0; i < board.GetLength(key1); i++)
            {
                //Reset the tracker for a new row
                List<int> NumberTracker = new List<int>(NumberCheckerTemplate);

                for (int j = 0; j < board.GetLength(key2); j++)
                {
                    if (board[i, j] < 1 || board[i, j] > NUM_COLUMNS)
                    {
                        Console.WriteLine("Board not valid: value in row " + i + ", column " + j + " is " + board[i, j] + ", which is outside 1-9");
                        return false;
                    }

                    if (NumberTracker.Contains(board[i, j]))
                    {
                        NumberTracker.Remove(board[i, j]);
                    }
                    else
                    {
                        Console.WriteLine("Board not valid: value " + board[i, j] + " occurs more than once in row " + i + ".");
                        return false;
                    }

                }

                if (NumberTracker.Count > 0)
                {
                    Console.WriteLine("Board not valid: Some numbers in row " + i + " were not used.  Unused numbers:\n");
                    foreach (int unused in NumberTracker)
                    {
                        Console.WriteLine(unused);
                    }
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verifies that each cell in the sudoku board has unique numbers 1-9
        /// </summary>
        /// <param name="board">Board to check</param>
        /// <returns>True if the cells are all filled with unique nums 1-9</returns>
        static bool CheckCells(int[,] board)
        {
            List<int[,]> allCells = GetCells(board, 3, 3);

            Console.WriteLine("Total number of cells: " + allCells.Count);

            for (int i = 0; i < allCells.Count; i++)
            {
                if (!CheckCell(allCells[i]))
                {
                    Console.WriteLine("Cell at index " + i + " failed.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks an individual sudoku cell
        /// </summary>
        /// <param name="cell">2D array representing one cell</param>
        /// <returns>True if the cell is well-formed with unique nums 1-9</returns>
        static bool CheckCell(int[,] cell)
        {
            if (cell == null)
                return false;

            //Fill up list to use for checking valid in the cell
            List<int> NumberChecker = new List<int>(NUM_COLUMNS);
            for (int i = 1; i <= NUM_COLUMNS; i++)
            {
                NumberChecker.Add(i);
            }

            for (int row = 0; row < cell.GetLength(0); row++)
            {
                for (int col = 0; col < cell.GetLength(1); col++)
                {
                    if (cell[row, col] > 9 || cell[row, col] < 1)
                    {
                        Console.WriteLine("Cell uses number " + cell[row, col] + ", which is not 1-9.");
                        return false;
                    }

                    if (NumberChecker.Contains(cell[row, col]))
                    {
                        NumberChecker.Remove(cell[row, col]);
                    }
                    else
                    {
                        Console.WriteLine("Cell uses number " + cell[row, col] + " more than once.");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Gets a list of all the cells in the sudoku board
        /// </summary>
        /// <param name="board">Whole sudoku board</param>
        /// <param name="cellWidth">Width of each cell</param>
        /// <param name="cellHeight">Height of each cell</param>
        /// <returns>A list of 2D int arrays.  Each 2D int array is a [row][column] representation of a cell</returns>
        static List<int[,]> GetCells(int[,] board, int cellWidth, int cellHeight)
        {
            #region Sanity checks for input

            if (board.GetLength(0) < cellHeight)
            {
                Console.WriteLine("Original board must have at least as many rows as the cell height");
                return null;
            }

            if (board.GetLength(1) < cellWidth)
            {
                Console.WriteLine("Original board must have at least as many columns as the cell width");
                return null;
            }

            if (board.GetLength(0) % cellHeight != 0)
            {
                Console.WriteLine("Number of rows in original board must be a multiple of cell height (" + cellHeight + ").  " +
                    "Given board has " + board.GetLength(0) + " rows");
                return null;
            }

            if (board.GetLength(1) % cellWidth != 0)
            {
                Console.WriteLine("Number of columns in original board must be a multiple of cell width (" + cellWidth + ").  " +
                    "Given board has " + board.GetLength(1) + " columns");
                return null;
            }

            #endregion Sanity checks for input

            //Find out how many cells horizontally and vertically we'll have
            int cellsVertically = board.GetLength(0) / cellHeight;
            int cellsHorizontally = board.GetLength(1) / cellWidth;
            int totalNumCells = cellsVertically * cellsHorizontally;

            //initialize answer object
            List<int[,]> allCells = new List<int[,]>(totalNumCells);
            for (int i = 0; i < totalNumCells; i++)
            {
                allCells.Add(new int[cellHeight, cellWidth]);
            }

            for (int r = 0; r < board.GetLength(0); r++)
            {
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    int whichCell = ((r / cellHeight) * cellWidth) + (c / cellWidth);
                    int cellRow = r % cellHeight;
                    int cellCol = c % cellWidth;
                    allCells[whichCell][cellRow, cellCol] = board[r, c];
                    //allCells[whichCell, cellRow, cellCol] = board[r, c];
                }
            }
            return allCells;
        }

        /// <summary>
        /// Determines if a sudoku board is properly formed
        /// </summary>
        /// <param name="board">2D array representation of the board to test</param>
        /// <returns>True if the board is properly formed</returns>
        public static bool IsValid(int[,] board)
        {
            //Sanity check
            if (board == null)
            {
                Console.WriteLine("Board not valid: board is null");
                return false;
            }

            //Check correct number of rows in the board
            if (board.GetLength(0) != NUM_ROWS)
            {
                Console.WriteLine("Board not valid: Board has incorrect number of rows.  Has " + board.GetLength(0) + " rows.");
                return false;
            }

            //Check correct number of col in the board
            if (board.GetLength(1) != NUM_ROWS)
            {
                Console.WriteLine("Board not valid: Board has incorrect number of cols.  Has " + board.GetLength(1) + " cols.");
                return false;
            }

            if (!CheckCells(board))
            {
                Console.WriteLine("Cells check failed");
                return false;
            }

            //Check the columns for proper numbers
            if (!CheckLine(board, 1, 0))
                return false;

            //Check the rows for proper numbers
            if (!CheckLine(board, 0, 1))
                return false;

            return true;
        }
    }
}