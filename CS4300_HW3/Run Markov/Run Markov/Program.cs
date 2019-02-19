using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses;
using VIC;

namespace Run_Markov
{
    class Program
    {
        static void Main(string[] args)
        {
            State asst = new State(0, 20);
            State assc = new State(1, 60);
            State full = new State(2, 400);
            State hl = new State(3, 10);
            State dead = new State(4, 0);

            DirectedEdge asst1 = new DirectedEdge(asst, asst, 0.6);
            DirectedEdge asst2 = new DirectedEdge(asst, hl, 0.2);
            DirectedEdge asst3 = new DirectedEdge(asst, assc, 0.2);

            DirectedEdge assc1 = new DirectedEdge(assc, assc, 0.6);
            DirectedEdge assc2 = new DirectedEdge(assc, full, 0.2);
            DirectedEdge assc3 = new DirectedEdge(assc, hl, 0.2);

            DirectedEdge full1 = new DirectedEdge(full, full, .7);
            DirectedEdge full2 = new DirectedEdge(full, dead, 0.3);

            DirectedEdge hl1 = new DirectedEdge(hl, hl, 0.7);
            DirectedEdge hl2 = new DirectedEdge(hl, dead, 0.3);

            DirectedEdge dead1 = new DirectedEdge(dead, dead, 1.0);

            List<DirectedEdge> edges = new List<DirectedEdge> {asst1, asst2, asst3, assc1, assc2, assc3, full1, full2, hl1, hl2, dead1};

            ValueIterationCalculator calc = new ValueIterationCalculator(edges, 5);
            double[,] arr = calc.Calculate(5, .5);

            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    Console.Write("| {0} |", arr[row, col]);
                }

                Console.WriteLine();
            }

            Console.Read();
        }
    }
}
