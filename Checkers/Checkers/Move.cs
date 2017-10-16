using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{

    class Move
    {
        public Marker marker1 { get; set; }
        public Marker marker2 { get; set; }

        public Move()
        {
            this.marker1 = null;
            this.marker2 = null;
        }

        public Move(Marker marker1, Marker marker2)
        {
            this.marker1 = marker1;
            this.marker2 = marker2;
        }

        //vaild move check
        public bool ValidMove(string colour)
        {
            if (colour == "Black")
            {
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
            }
            if (colour == "White")
            {
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
            }
            if (colour == "BlackKing")
            {
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
            }
            if (colour == "WhiteKing")
            {
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row - 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
                    return true;
                if ((marker1.Row + 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
                    return true;
            }
            //if (colour == "King")
            //{
            //    if ((marker1.Row - 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
            //        return true;
            //    if ((marker1.Row - 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
            //        return true;
            //    if ((marker1.Row + 1 == marker2.Row) && (marker1.Column - 1 == marker2.Column))
            //        return true;
            //    if ((marker1.Row + 1 == marker2.Row) && (marker1.Column + 1 == marker2.Column))
            //        return true;
            //}


            return false;
        }

        public Marker checkJumps(string colour)
        {
            if (colour == "Black")
            {
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row -1,marker1.Column - 1);
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column + 1);
            }
            if (colour == "White")
            {
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row + 1, marker1.Column - 1);
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row + 1, marker1.Column + 1);
            }
            if (colour == "BlackKing")
            {
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column - 1);
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column + 1);
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column - 1);
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row + 1, marker1.Column + 1);
            }
            if (colour == "WhiteKing")
            {
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column - 1);
                if ((marker1.Row - 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column + 1);
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column - 2 == marker2.Column))
                    return new Marker(marker1.Row - 1, marker1.Column - 1);
                if ((marker1.Row + 2 == marker2.Row) && (marker1.Column + 2 == marker2.Column))
                    return new Marker(marker1.Row + 1, marker1.Column + 1);
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

            return marker1.Equals(move.marker1) && marker2.Equals(move.marker2);

        }
    }
}
