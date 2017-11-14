using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Replay
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
        public Replay()
        {

        }
        public Replay(int I)
        {
            id = I;
        }
        public Replay(int I, string N)
        {
            id = I;
            name = N;
        }

        public Replay(int I, string N, Queue t, Stack r)
        {
            id = I;
            name = N;
            turns = t;
            Taken = r;
        }

        public static implicit operator Replay(History v)
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(Replay)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(v).Cast<PropertyDescriptor>();

            var convert = new Replay();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(v), convertProperty.PropertyType));
                }
            }
            return convert;
        }
    }
}

