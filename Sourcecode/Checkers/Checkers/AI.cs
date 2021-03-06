﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Checkers
{
    class AI
    {
        public static Move GetMove(Checkers_Board currentBoard)
        {
            List<Move> avaliableMoves = GetAvaliableMoves(currentBoard);
            //get random postion in the list
            avaliableMoves.Shuffle();
            if (avaliableMoves.Count < 1)
                return null;
            return avaliableMoves[0];
        }

        private static List<Move> GetAvaliableMoves(Checkers_Board currentBoard)
        {
            List<Marker> currentMarkers = new List<Marker>();
            List<Move> avaliableMoves = new List<Move>();
            List<Move> jumpMoves = currentBoard.checkJumps("White");
            if (jumpMoves.Count > 0)
            {
                return jumpMoves;
            }
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if ((currentBoard.GetState(row, column) == 1) || (currentBoard.GetState(row, column) == 3))
                    {
                        currentMarkers.Add(new Marker(row, column));
                        
                    }
                }
            }
            foreach (Marker m in currentMarkers)
            {
                avaliableMoves.AddRange(CheckForMoves(m, currentBoard));
            }
            return avaliableMoves;
        }

        private static List<Move> CheckForMoves(Marker marker, Checkers_Board currentBoard)
        {
            List<Move> moves = new List<Move>();
            //if the marker is a white King
            if (currentBoard.GetState(marker.Row, marker.Column) == 3)
            {
                //if the marker next to it is black
                if ((currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 2) || (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row - 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row - 1, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 2) || (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row - 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row - 1, marker.Column + 2)));
                }
                if ((currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 2) || (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 3, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 2) || (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 3, marker.Column + 2)));
                }
                //if the squares next to it are empty
                if (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row, marker.Column + 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column + 1)));
            }
            //if the marker is a white standard
            else if (currentBoard.GetState(marker.Row, marker.Column) == 1)
            {
                Console.WriteLine("Current State: "+currentBoard.GetState((marker.Row + 1), (marker.Column - 1)));
                if ((currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 2) || (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 3, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 2) || (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 4))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 3, marker.Column + 2)));
                }
                //if the squares next to it are empty
                if (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column + 1)));
                Console.WriteLine("Current State:"+currentBoard.GetState(marker.Row + 1, marker.Column + 1) + "\n Row:"+(marker.Row+1)+ "\n Column: "+(marker.Column + 1));

                if (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column - 1)));
            }
            return moves;
        }

    }
       

}
