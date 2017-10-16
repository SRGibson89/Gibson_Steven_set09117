using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    // states a piece can be in:
    //  -1 is an invalid space
    //   0 is an empty space
    //   1 is a white piece
    //   2 is a black piece
    //   3 is white king
    //   4 is black king
    class Checkers_Board
    {
        public int[,] board = new int[8, 8];

        public Checkers_Board()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    board[row, column] = -1;
                    
                }
            }
            

        }

        //sets state of a piece
        public bool SetState(int row, int column, int state)
        {
            if ((state > 4) || (state < 1))
            {
                return false;

            }
            else
            {
                board[row, column] = state;
                return true;
            }
        }

        //gets the state of a piece
        public int GetState(int row, int column)
        {
            if ((row > 7) || (row < 0) || (column > 7) || (column < 0))
            {
                return -1;
            }
            else
            {
                return board[row, column];
            }

        }
        public List<Move> CheckJumps(string color)
        {
            List<Move> jumps = new List<Move>();

            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if (color == "White")
                    {
                        if (GetState(row, column) == 3)
                        {
                            if ((GetState(row - 2, column - 2) == 0) && ((GetState(row - 1, column - 1) == 2) || (GetState(row - 1, column - 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column - 2)));
                            }
                            if ((GetState(row - 2, column + 2) == 0) && ((GetState(row - 1, column + 1) == 2) || (GetState(row - 1, column + 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column + 2)));
                            }
                            if ((GetState(row + 2, column - 2) == 0) && ((GetState(row + 1, column - 1) == 2) || (GetState(row + 1, column - 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column - 2)));
                            }
                            if ((GetState(row + 2, column + 2) == 0) && ((GetState(row + 1, column + 1) == 2) || (GetState(row + 1, column + 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column + 2)));
                            }
                        }
                        if (GetState(row, column) == 1)
                        {
                            if ((GetState(row + 2, column - 2) == 0) && ((GetState(row + 1, column - 1) == 2) || (GetState(row + 1, column - 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column - 2)));
                            }
                            if ((GetState(row + 2, column + 2) == 0) && ((GetState(row + 1, column + 1) == 2) || (GetState(row + 1, column + 1) == 4)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column + 2)));
                            }
                        }
                    }
                    if (color == "Black")
                    {
                        if (GetState(row, column) == 4)
                        {
                            if ((GetState(row - 2, column - 2) == 0) && ((GetState(row - 1, column - 1) == 1) || (GetState(row - 1, column - 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column - 2)));
                            }
                            if ((GetState(row - 2, column + 2) == 0) && ((GetState(row - 1, column + 1) == 1) || (GetState(row - 1, column + 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column + 2)));
                            }
                            if ((GetState(row + 2, column - 2) == 0) && ((GetState(row + 1, column - 1) == 1) || (GetState(row + 1, column - 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column - 2)));
                            }
                            if ((GetState(row + 2, column + 2) == 0) && ((GetState(row + 1, column + 1) == 1) || (GetState(row + 1, column + 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row + 3, column + 2)));
                            }
                        }
                        if (GetState(row, column) == 2)
                        {
                            if ((GetState(row - 2, column - 2) == 0) && ((GetState(row - 1, column - 1) == 1) || (GetState(row - 1, column - 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column - 2)));
                            }
                            if ((GetState(row - 2, column + 2) == 0) && ((GetState(row - 1, column + 1) == 1) || (GetState(row - 1, column + 1) == 3)))
                            {
                                jumps.Add(new Move(new Marker(row + 1, column), new Marker(row - 1, column + 2)));
                            }
                        }
                    }
                }
            }
            return jumps;
        }
         
    }
}
