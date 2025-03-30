using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    //Cell Model class for each spot on board
    public class Cell
    {
        //positioning on board
        public int row { get; set; }
        public int col { get; set; }

        //What piece if any, is on it
        public String isCurrentlyOccupied { get; set; }

        //is this cell an option for a piece
        public bool isLegalNextMove { get; set; }

        //constructor
        public Cell(int r, int c)
        {
            this.row = r;
            this.col = c;
            isLegalNextMove = false;
            isCurrentlyOccupied = "";
        }


    }
}
