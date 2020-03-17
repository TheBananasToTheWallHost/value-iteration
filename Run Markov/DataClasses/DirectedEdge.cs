using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class DirectedEdge
    {
        public State StartState { get; private set; }
        public State EndState { get; private set; }
        public double Probability{ get; private set; }

        public DirectedEdge(State s1, State s2, double probability)
        {
            StartState = s1;
            EndState = s2;
            Probability = probability;
        }
    }
}
