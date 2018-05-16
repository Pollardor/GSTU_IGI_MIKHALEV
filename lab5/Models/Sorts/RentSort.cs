using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Sorts
{
    public class RentSort
    {
        public Model Models { get; set; }

        public RentSort()
        {
            Models = new Model(State.None);
        }

        public RentSort(State state)
        {
            Models = new Model(state);
        }

        public enum State
        {
            None,

            DateGetDesc,
            RentDateDesc,
            WorkerFIODesc,

            DateGetAsc,
            RentDateAsc,
            WorkerFIOAsc
        }

        public class Model
        {
            public State DateGetSort { get; set; }
            public State RentDateSort { get; set; }
            public State WorkerFIOSort { get; set; }

            public State CurrentSort { get; set; }

            public Model(State sortOrder)
            {
                DateGetSort = sortOrder == State.DateGetAsc ? State.DateGetDesc : State.DateGetAsc;
                RentDateSort = sortOrder == State.RentDateAsc ? State.RentDateDesc : State.RentDateAsc;
                WorkerFIOSort = sortOrder == State.WorkerFIOAsc ? State.WorkerFIODesc : State.WorkerFIOAsc;
                
                CurrentSort = sortOrder;
            }
        }

    }
}
