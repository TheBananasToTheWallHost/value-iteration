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
        private static char[] WHITESPACE = new char[] { ' ' };

        static void Main(string[] args)
        {
            while (true)
            {
                int numStates;

                List<DirectedEdge> edges = ReadInput(out numStates);

                Console.WriteLine("Enter number of time steps to calculate and the discount factor: ");
                string[] vals = Console.ReadLine().Split(WHITESPACE, StringSplitOptions.None);

                int timeSteps;
                double discountFactor;

                try
                {
                    timeSteps = Convert.ToInt32(vals[0]);
                    discountFactor = Convert.ToDouble(vals[1]);
                }
                catch (Exception e)
                {

                    throw new Exception("Invalid input. " + e.Message);
                }

                ValueIterationCalculator calc = new ValueIterationCalculator(edges, numStates);
                double[,] arr = calc.Calculate(timeSteps, discountFactor);

                for (int row = 0; row < arr.GetLength(0); row++)
                {
                    for (int col = 0; col < arr.GetLength(1); col++)
                    {
                        Console.Write("| {0} |", arr[row, col]);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("Do you wish to calculate the value iteration of another MDP? y/n");
                string cont = Console.ReadLine();

                if (cont.Equals("n"))
                    break;
            }
        }

        /// <summary>
        /// ****************TODO****************
        /// Add file reading capabilities maybe. Also break function down into smaller pieces.
        /// </summary>
        /// <returns></returns>
        static List<DirectedEdge> ReadInput(out int stateCount)
        {
            bool continueInput = true;
            bool useFile = false;

            while (continueInput)
            {
                GetInputMethod(ref continueInput, ref useFile);
            }

            Dictionary<string, State> states = new Dictionary<string, State> { };
            List<DirectedEdge> edges = new List<DirectedEdge> { };

            if (useFile)
            {
                Console.WriteLine("Enter file path: ");
                string path = Console.ReadLine();
                string[] fileLines;

                try
                {
                    fileLines = System.IO.File.ReadAllLines(@path);
                }
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    stateCount = states.Count;
                    return edges;
                }
            }
            else
            {
                

                Console.WriteLine("Enter information: ");

                string line = Console.ReadLine();
                int numStates = Convert.ToInt32(line);

                for (int i = 0; i < numStates; i++)
                {
                    line = Console.ReadLine();
                    string[] state = line.Split(WHITESPACE, StringSplitOptions.None);

                    string name;
                    int stateNum;
                    int initVal;

                    try
                    {
                        name = state[0];
                        stateNum = Convert.ToInt32(state[1]);
                        initVal = Convert.ToInt32(state[2]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input. " + e.Message);
                        stateCount = states.Count;
                        return edges;
                    }
                    states.Add(name, new State(stateNum, initVal));
                    
                }

                do
                {
                    line = Console.ReadLine();
                    string[] edge = line.Split(WHITESPACE, StringSplitOptions.None);

                    if (edge.Length == 1 && edge[0].Equals("end"))
                    {
                        break;
                    }

                    string state1;
                    string state2;
                    double probability;

                    try
                    {
                        state1 = edge[0];
                        state2 = edge[1];
                        probability = Convert.ToDouble(edge[2]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid input. " + e.Message);
                        stateCount = states.Count;
                        return edges;
                    }

                    edges.Add(new DirectedEdge(states[state1], states[state2], probability));


                } while (true);
            }
            stateCount = states.Count;
            return edges;
        }

        static void GetInputMethod(ref bool continueInput, ref bool useFile)
        {
            Console.WriteLine("Input the method you'd like to use for your information: 'file' or 'console' ");
            string line = Console.ReadLine().ToLower();

            if (line.Equals("file"))
            {
                useFile = true;
                continueInput = false;
            }
            else if (line.Equals("console"))
            {
                continueInput = false;
            }
            else if (line.Equals("quit") || line.Equals("q"))
            {
                Environment.Exit(0);
            }
        }
    }
}
