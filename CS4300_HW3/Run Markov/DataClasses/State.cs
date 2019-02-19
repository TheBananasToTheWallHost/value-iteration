using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class State
    {
        public int StateNum { get; private set; }
        public double InitialValue { get; private set; }

        public State(int stateNum, double initialValue)
        {
            StateNum = stateNum;
            InitialValue = initialValue;
        }


    }
}
