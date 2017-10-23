using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 //* Design Pattern: Singleton
namespace Checkers
{
    class SingletonLists
    {
        //makes a new list for games to be recorded to be stored but will only make it once.
        private static SingletonLists instance;
        public List<History> GameList = new List<History>();

        private SingletonLists()
        {

        }

        public static SingletonLists Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonLists();
                }
                return instance;
            }
        }
    }
}
