using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

    class Move
    {
        public Marker markerBefore { get; set; }
        public Marker markerAfter { get; set; }

        public Move()
        {
            this.markerBefore = null;
            this.markerAfter = null;
        }

        public Move(Marker marker1, Marker marker2)
        {
            this.markerBefore = marker1;
            this.markerAfter = marker2;
        }

        //vaild move check
        public bool ValidMove(string colour)
        {
            if (colour == "Black")
            {
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
            }
            if (colour == "White")
            {
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
            }
            if (colour == "BlackKing")
            {
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
            }
            if (colour == "WhiteKing")
            {
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row - 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column - 1 == markerAfter.Column))
                    return true;
                if ((markerBefore.Row + 1 == markerAfter.Row) && (markerBefore.Column + 1 == markerAfter.Column))
                    return true;
            }
            


            return false;
        }

        public Marker checkJumps(string colour)
        {
            if (colour == "Black")
            {
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row -1,markerBefore.Column - 1);
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column + 1);
            }
            if (colour == "White")
            {
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row + 1, markerBefore.Column - 1);
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row + 1, markerBefore.Column + 1);

            }
            if (colour == "BlackKing")
            {
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column - 1);
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column + 1);
                //if ((marker1.Row + 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                //    return new Marker(marker1.Row - 1, marker1.Column - 1);
                //if ((marker1.Row + 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                //    return new Marker(marker1.Row + 1, marker1.Column + 1);
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row + 1, markerBefore.Column - 1);
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row + 1, markerBefore.Column + 1);
            }
            if (colour == "WhiteKing")
            {
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column - 1);
                if ((markerBefore.Row - 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column + 1);
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column - 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row - 1, markerBefore.Column - 1);
                if ((markerBefore.Row + 2 == markerAfter.Row) && (markerBefore.Column + 2 == markerAfter.Column))
                    return new Marker(markerBefore.Row + 1, markerBefore.Column + 1);
            }
            return null;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Move move = obj as Move;
            if ((System.Object)move == null)
            {
                return false;
            }

            return markerBefore.Equals(move.markerBefore) && markerAfter.Equals(move.markerAfter);

        }
    }
}
