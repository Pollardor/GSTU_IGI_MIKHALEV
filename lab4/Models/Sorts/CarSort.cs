using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Sorts
{
    public class CarSort
    {
        public Model Models { get; set; }

        public CarSort()
        {
            Models = new Model(State.None);
        }

        public CarSort(State state)
        {
            Models = new Model(state);
        }

        public enum State
        {
            None,

            AgeDesc,
            MarkDesc,
            PriceDesc,
            DayPriceDesc,
            DateTODesc,
            MileageDesc,

            AgeAsc,
            MarkAsc,
            PriceAsc,
            DayPriceAsc,
            DateTOAsc,
            MileageAsc
        }

        public class Model
        {
            public State AgeSort { get; set; }
            public State MarkSort { get; set; }
            public State PriceSort { get; set; }
            public State DayPriceSort { get; set; }
            public State DateTOSort { get; set; }
            public State MileageSort { get; set; }

            public State CurrentSort { get; set; }

            public Model(State sortOrder)
            {
                AgeSort = sortOrder == State.AgeAsc ? State.AgeDesc : State.AgeAsc;
                MarkSort = sortOrder == State.MarkAsc ? State.MarkDesc : State.MarkAsc;
                PriceSort = sortOrder == State.PriceAsc ? State.PriceDesc : State.PriceAsc;
                DayPriceSort = sortOrder == State.DayPriceAsc ? State.DayPriceDesc : State.DayPriceAsc;
                DateTOSort = sortOrder == State.DateTOAsc ? State.DateTODesc : State.DateTOAsc;
                MileageSort = sortOrder == State.MileageAsc ? State.MileageDesc : State.MileageAsc;

                CurrentSort = sortOrder;
            }
        }
    }
}
