using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }

        //the board, each cell
        public Cell[,] theGrid {  get; set; }

        //constructor that fills the board and initiates each cell
        public Board(int s)
        {
            Size = s;

            //double array grid equal width and length
            theGrid = new Cell[Size, Size];

            //double loop to fill double array
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    //each cell is constructed, sending the row and column in
                    theGrid[i,j] = new Cell(i,j);
                }
            }
        }

        //every cell is made empty, unoccupied
        public void ClearBoard()
        {
            //double loop reaches every cell
            for (int i = 0;i < Size; i++)
            {
                for (int j = 0;j < Size; j++)
                {
                    theGrid[i, j].isCurrentlyOccupied = "";
                    theGrid[i, j].isLegalNextMove = false;
                }
            }
        }

       
        public void MarkNextLegalMove(Cell targetCell, string pieceType)
        {
            switch (pieceType)
            {
                case "King":
                    targetCell.isCurrentlyOccupied = "K";

                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            if (((targetCell.row + x) >= 0) && ((targetCell.row + x) < Size) && ((targetCell.col + y) >= 0) && ((targetCell.col + y) < Size))
                            {

                                theGrid[targetCell.row + x, targetCell.col + y].isLegalNextMove = true;



                            }
                        }
                    }

                    break;

                case "Queen":
                    targetCell.isCurrentlyOccupied = "Q";

                    int swap = 1;
                    int isRow = 1;
                    int isCol = 0;

                    //switches horizontal or vertical
                    for (int s = 0; s < 2; s++)
                    {
                        swap = 1;
                        //switches positive direction to opposite
                        for (int j = 0; j < 2; j++)
                        {
                            //checks all spots parrallel or perpindicular
                            for (int i = 1; i < Size; i++)
                            {
                                //check for whether horizontal or vertical, positive or negative direction
                                int changeRow = i * swap * isRow;
                                int changeCol = i * swap * isCol;
                                //check to make sure spot is valid

                                if (((targetCell.row + changeRow) <= (Size - 1)) && ((targetCell.row + changeRow) >= 0) && ((targetCell.col + changeCol) >= 0) && ((targetCell.col + changeCol) <= (Size - 1)))
                                {
                                    theGrid[targetCell.row + changeRow, targetCell.col + changeCol].isLegalNextMove = true;
                                }
                            }

                            swap = -1;
                        }
                        isRow = 0;
                        isCol = 1;
                    }
                    //Decides if positive or negative on row or col
                    int rowSide = 1;
                    int colSide = 1;
                    //switches row to negative
                    for (int y = 0; y < 2; y++)
                    {
                        //switches column to negative
                        for (int x = 0; x < 2; x++)
                        {
                            //tests every option
                            for (int i = 1; i < Size; i++)
                            {
                                int changeRowD = i * rowSide;
                                int changeColD = i * colSide;
                                if (((targetCell.row + changeRowD) <= (Size - 1)) && ((targetCell.row + changeRowD) >= 0) && ((targetCell.col + changeColD) >= 0) && ((targetCell.col + changeColD) <= (Size - 1)))
                                {
                                    theGrid[targetCell.row + changeRowD, targetCell.col + changeColD].isLegalNextMove = true;
                                }
                            }
                            colSide *= -1;
                        }
                        rowSide *= -1;
                    }

                    break;

                case "Knight":
                    targetCell.isCurrentlyOccupied = "N";

                    //every possible move of the Knight with checks
                    if ((targetCell.row +2 >= 0) && (targetCell.row + 2 <=  (Size -1)) && (targetCell.col + 1 >= 0) && (targetCell.col + 1 <= (Size - 1)))
                        theGrid[targetCell.row + 2, targetCell.col + 1].isLegalNextMove = true;

                    if ((targetCell.row  + 1 >= 0) && (targetCell.row + 1 <= (Size - 1)) && (targetCell.col + 2 >= 0) && (targetCell.col + 2 <= (Size - 1)))
                        theGrid[targetCell.row + 1, targetCell.col + 2].isLegalNextMove = true;

                    if ((targetCell.row - 2 >= 0) && (targetCell.row - 2<= (Size - 1)) && (targetCell.col + 1>= 0) && (targetCell.col + 1<= (Size - 1)))
                        theGrid[targetCell.row - 2, targetCell.col + 1].isLegalNextMove = true;

                    if ((targetCell.row - 1 >= 0) && (targetCell.row - 1 <= (Size - 1)) && (targetCell.col + 2 >= 0) && (targetCell.col + 2 <= (Size - 1)))
                        theGrid[targetCell.row - 1, targetCell.col + 2].isLegalNextMove = true;

                    if ((targetCell.row + 2 >= 0) && (targetCell.row + 2 <= (Size - 1)) && (targetCell.col - 1 >= 0) && (targetCell.col - 1 <= (Size - 1)))
                        theGrid[targetCell.row + 2, targetCell.col - 1].isLegalNextMove = true;

                    if ((targetCell.row + 1 >= 0) && (targetCell.row + 1 <= (Size - 1)) && (targetCell.col - 2 >= 0) && (targetCell.col - 2 <= (Size - 1)))
                        theGrid[targetCell.row + 1, targetCell.col - 2].isLegalNextMove = true;

                    if ((targetCell.row - 2 >= 0) && (targetCell.row - 2 <= (Size - 1)) && (targetCell.col - 1 >= 0) && (targetCell.col - 1 <= (Size - 1)))
                        theGrid[targetCell.row - 2, targetCell.col - 1].isLegalNextMove = true;

                    if ((targetCell.row - 1 >= 0) && (targetCell.row - 1 <= (Size - 1)  ) && (targetCell.col - 2 >= 0) && (targetCell.col - 2 <= (Size - 1)))
                        theGrid[targetCell.row - 1, targetCell.col - 2].isLegalNextMove = true;
                    break;

                case "Bishop":
                    targetCell.isCurrentlyOccupied = "B";

                    //decides if rows/cols go negative or positive direction
                    int rowSideB = 1;
                    int colSideB = 1;
                    //switches row negative
                    for (int y = 0; y < 2; y++)
                    {
                        //switches col negative
                        for (int x = 0; x < 2; x++)
                        {
                            //tests every spot
                            for (int i = 1; i < Size; i++)
                            {
                                int changeRowB = i * rowSideB;
                                int changeColB = i * colSideB;
                                if (((targetCell.row + changeRowB) <= (Size - 1)) && ((targetCell.row + changeRowB) >= 0) && ((targetCell.col + changeColB) >= 0) && ((targetCell.col + changeColB) <= (Size - 1)))
                                {
                                    theGrid[targetCell.row + changeRowB, targetCell.col + changeColB].isLegalNextMove = true;
                                }
                            }
                            colSideB *= -1;
                        }
                        rowSideB *= -1;
                    }
                    break;

                case "Pawn":
                    targetCell.isCurrentlyOccupied = "P";

                    //if pawn is in starting position, they can move two directly forward
                    if (targetCell.row == Size - 1)
                    {
                        for (int i = 1; i <= 2; i++)
                        {
                            if (theGrid[targetCell.row - i, targetCell.col].isCurrentlyOccupied == "")
                                theGrid[targetCell.row - i, targetCell.col].isLegalNextMove = true;
                        }
                    }
                    //else only one forward
                    else
                    {
                        if (theGrid[targetCell.row - 1, targetCell.col].isCurrentlyOccupied == "")
                            theGrid[targetCell.row - 1, targetCell.col].isLegalNextMove = true;
                    }

                    break;

                case "Rook":
                    targetCell.isCurrentlyOccupied = "R";

                    int swapR = 1;
                    int isRowR = 1;
                    int isColR = 0;

                    //switches horizontal or vertical
                    for (int s = 0; s < 2; s++)
                    {
                        swapR = 1;
                        //switches positive direction to opposite
                        for (int j = 0; j < 2; j++)
                        {
                            //checks all spots parrallel or perpindicular
                            for (int i = 1; i < Size; i++)
                            {
                                //check for whether horizontal or vertical, positive or negative direction
                                int changeRowR = i * swapR * isRowR;
                                int changeColR = i * swapR * isColR;
                                //check to make sure spot is valid

                                if (((targetCell.row + changeRowR) <= (Size-1)) && ((targetCell.row + changeRowR) >= 0) && ((targetCell.col + changeColR) >= 0) && ((targetCell.col + changeColR) <= (Size-1)))
                                {
                                    theGrid[targetCell.row + changeRowR, targetCell.col + changeColR].isLegalNextMove = true;
                                }
                            }

                            swapR = -1;
                        }
                        isRowR = 0;
                        isColR = 1;
                    }

                    break;

                default:
                    Console.WriteLine("Please input a valid piece.");
                    break;



            }




        }

    }
}
