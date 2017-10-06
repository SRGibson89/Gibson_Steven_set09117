using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Marker
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Marker(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Marker marker = obj as Marker;
            if ((System.Object)marker ==null)
            {
                return false;
            }

            return Row == marker.Row && Column == marker.Column;
        }


    }
}
