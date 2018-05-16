using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Sorts
{
    public class ClientSort
    {
        public Model Models { get; set; }

        public ClientSort()
        {
            Models = new Model(State.None);
        }

        public ClientSort(State state)
        {
            Models = new Model(state);
        }

        public enum State
        {
            None,

            FIODesc,

            FIOAsc
        }

        public class Model
        {
            public State FIOSort { get; set; }

            public State CurrentSort { get; set; }

            public Model(State sortOrder)
            {
                FIOSort = sortOrder == State.FIOAsc ? State.FIODesc : State.FIOAsc;
            
                CurrentSort = sortOrder;
            }
        }
    }
}
