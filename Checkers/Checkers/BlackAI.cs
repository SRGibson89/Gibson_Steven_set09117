using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class BlackAI
    {
        public static Move GetMove(Checkers_Board currentBoard)
        {
            List<Move> avaliableMoves = GetAvaliableMoves(currentBoard);
            avaliableMoves.Shuffle();
            if (avaliableMoves.Count < 1)
                return null;
            return avaliableMoves[0];
        }

        private static List<Move> GetAvaliableMoves(Checkers_Board currentBoard)
        {
            List<Marker> currentMarkers = new List<Marker>();
            List<Move> avaliableMoves = new List<Move>();
            List<Move> jumpMoves = currentBoard.checkJumps("Black");
            if (jumpMoves.Count > 0)
            {
                return jumpMoves;
            }
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    if ((currentBoard.GetState(row, column) == 2) || (currentBoard.GetState(row, column) == 4))
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
            //Console.WriteLine("CurrentBoard.State: " + currentBoard.GetState(marker.Row - 1, marker.Column - 1));
            if (currentBoard.GetState(marker.Row, marker.Column) == 4)
            {

                if ((currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 1) || (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row + 1, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 1) || (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row + 1, marker.Column + 2)));
                }
                if ((currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 1) || (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row - 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 3, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 1) || (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row + 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 3, marker.Column + 2)));
                }
                if (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row, marker.Column + 1)));
                if (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 2, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 2, marker.Column + 1)));
            }
            else if (currentBoard.GetState(marker.Row, marker.Column) == 2)
            {
                Console.WriteLine("Current State: " + currentBoard.GetState((marker.Row + 1), (marker.Column - 1)));
                if ((currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 1) || (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row - 2, marker.Column - 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 3, marker.Column - 2)));
                }
                if ((currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 1) || (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 3))
                {
                    if (currentBoard.GetState(marker.Row - 2, marker.Column + 2) == 0)
                        moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 3, marker.Column + 2)));
                }

                if (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 2, marker.Column + 1)));
                Console.WriteLine("Current State:" + currentBoard.GetState(marker.Row - 1, marker.Column + 1) + "\n Row:" + (marker.Row - 1) + "\n Column: " + (marker.Column + 1));

                if (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row - 1, marker.Column), new Marker(marker.Row - 2, marker.Column - 1)));
            }
            return moves;
        }
    }
}
