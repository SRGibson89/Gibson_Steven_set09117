using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    // A stack of previous moves
    // used for an undo feature
    // a list will be used in replays
    class History
    {
        private int id;
        private string name;
        public Queue turns = new Queue();
        public Stack Taken = new Stack();
        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID
        {
            get { return id; }
            //referance number will be auto generated
            set
            { id = value; }

        }
        public History()
        {

        }
        public History(int I)
        {
            id = I;
        }
        public History (int I,string N)
        {
            id = I;
            name = N;
        }

        public History(int I, string N, Queue t, Stack r)
        {
            id = I;
            name = N;
            turns = t;
            Taken = r;
        }

        public History(History h)
        {
            
            this.id = h.ID;
            this.name = h.Name;
            this.turns = h.turns;
            this.Taken = h.Taken;
        }
    }
}
