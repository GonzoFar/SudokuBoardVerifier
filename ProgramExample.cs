using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sudoku;

/// <summary>
/// Example use of the sudoku checker
/// </summary>
class ProgramExample
{
    static void Main(string[] args)
    {
        //This board will pass verification
        int[,] board1 = new int[,] {
        { 1, 2, 3,  4, 5, 6,  7, 8, 9 },
        { 4, 5, 6,  7, 8, 9,  1, 2, 3 },
        { 7, 8, 9,  1, 2, 3,  4, 5, 6 },
          
        { 2, 3, 4,  5, 6, 7,  8, 9, 1 },
        { 5, 6, 7,  8, 9, 1,  2, 3, 4 },
        { 8, 9, 1,  2, 3, 4,  5, 6, 7 },
          
        { 3, 4, 5,  6, 7, 8,  9, 1, 2 },
        { 6, 7, 8,  9, 1, 2,  3, 4, 5 },
        { 9, 1, 2,  3, 4, 5,  6, 7, 8 },
    };

        //This board will fail verification
        int[,] board2 = new int[,] {
        { 1, 2, 3,  4, 5, 6,  7, 8, 9 },
        { 4, 5, 6,  7, 8, 9,  1, 2, 3 },
        { 7, 8, 9,  1, 2, 3,  4, 5, 6 },
          
        { 2, 3, 4,  5, 6, 7,  8, 9, 1 },
        { 5, 6, 7,  8, 5, 1,  2, 6, 4 },
        { 8, 9, 1,  2, 3, 4,  5, 6, 7 },
          
        { 3, 4, 5,  6, 7, 8,  9, 1, 2 },
        { 6, 7, 8,  9, 1, 2,  3, 4, 5 },
        { 9, 1, 2,  3, 4, 5,  6, 7, 8 },
    };

        Console.WriteLine("Board IsValid = {0}", SudokuVerifier.IsValid(board1));
        Console.Read();
    }
}
