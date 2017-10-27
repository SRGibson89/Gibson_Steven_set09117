using System;
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
            avaliableMoves.Shuffle();
            if (avaliableMoves.Count < 1)
                return null;
            return avaliableMoves[0];
        }

        private static List<Move> GetAvaliableMoves(Checkers_Board currentBoard)
        {
            List<Marker> currentMarkers = new List<Marker>();
            List<Move> avaliableMoves = new List<Move>();
            List<Move> jumpMoves = currentBoard.CheckJumps("White");
            if (jumpMoves.Count > 0)
            {
                return jumpMoves;
            }
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if ((currentBoard.GetState(r, c) == 1) || (currentBoard.GetState(r, c) == 3))
                    {
                        currentMarkers.Add(new Marker(r, c));
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
            if (currentBoard.GetState(marker.Row, marker.Column) == 3)
            {
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
                if (currentBoard.GetState(marker.Row - 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row - 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row, marker.Column + 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column + 1)));
            }
            else if (currentBoard.GetState(marker.Row, marker.Column) == 1)
            {
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
                if (currentBoard.GetState(marker.Row + 1, marker.Column - 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column - 1)));
                if (currentBoard.GetState(marker.Row + 1, marker.Column + 1) == 0)
                    moves.Add(new Move(new Marker(marker.Row + 1, marker.Column), new Marker(marker.Row + 2, marker.Column + 1)));
            }
            return moves;
        }

    }
}
