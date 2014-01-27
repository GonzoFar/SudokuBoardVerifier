SudokuBoardVerifier
===================

Validates that a Sudoku board is well formed.

Project includes a class SudokuVerifier with method 

static bool CheckCells(int[,] board);

This method takes as input a sudoku board represented as a 2D array.  
The method uses private methods in the SodukuVerifier class to verify:

-The board is square

-The board uses integers 1-9 uniquely in each row and column

-The board uses integers 1-9 uniquely in each Sudoku cell within the board

