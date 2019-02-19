using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;

namespace VIC
{
    public class ValueIterationCalculator
    {
        List<DirectedEdge> edges;
        int numStates;
        Dictionary<int, List<int>> graph;
        Dictionary<int, State> states;
        Dictionary<Tuple<int, int>, double> probabilties;


        public ValueIterationCalculator(List<DirectedEdge> edges, int numStates)
        {
            this.edges = edges;
            this.numStates = numStates;

            graph = new Dictionary<int, List<int>> { };
            states = new Dictionary<int, State> { };
            probabilties = new Dictionary<Tuple<int, int>, double> { };

            SetGraph();
        }

        private void SetGraph()
        {
            foreach (DirectedEdge edge in edges)
            {
                if (!graph.ContainsKey(edge.StartState.StateNum))
                    graph.Add(edge.StartState.StateNum, new List<int> { edge.EndState.StateNum });
                else
                    graph[edge.StartState.StateNum].Add(edge.EndState.StateNum);

                if (!states.ContainsKey(edge.StartState.StateNum))
                {
                    if (edge.StartState.StateNum > numStates)
                        throw new Exception("State number larger than the number of states");

                    states.Add(edge.StartState.StateNum, edge.StartState);
                }

                if (!probabilties.ContainsKey(Tuple.Create(edge.StartState.StateNum, edge.EndState.StateNum)))
                {
                    probabilties.Add(Tuple.Create(edge.StartState.StateNum, edge.EndState.StateNum), edge.Probability);
                }
            }
        }

        public double[,] Calculate(int time, double discountFactor)
        {
            double[,] calculations = new double[time + 1, numStates];

            for (int col = 0; col < calculations.GetLength(1); col++)
            {
                calculations[1, col] = states[col].InitialValue; 
            }

            for (int row = 1; row < calculations.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < calculations.GetLength(1); col++)
                {
                    double sum = 0;
                    foreach (int endState in graph[col])
                    {
                        sum += calculations[row, endState] * probabilties[Tuple.Create(col, endState)];
                    }
                    calculations[row + 1, col] = calculations[1, col] + discountFactor * sum;
                }
            }

            return calculations;
        }


    }
}
