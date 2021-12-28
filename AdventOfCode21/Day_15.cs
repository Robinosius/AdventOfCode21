using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode21
{
    class Day_15 : BaseDay
    {
        private List<string> input;

        public Day_15()
        {
            input = File.ReadAllLines(InputFilePath).ToList();
        }

        public override ValueTask<string> Solve_1()
        {
            int[,] matrix = new int[input[0].Length, input.Count];
            for(int i = 0; i < input.Count; i++)
            {
                for(int j = 0; j < input[0].Length; j++)
                {
                    matrix[i, j] = Int32.Parse(input[i][j].ToString());
                }
            }
            var graph = new Graph(matrix);
            return new(graph.GetShortestPathLenth().ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            return new("");
        }
    }


    class Graph
    {
        List<Node> nodes;
        Node targetNode;
        Tuple<int, int> gridSize;

        public Graph(int[,] matrix)
        {
            nodes = new();
            gridSize = new(matrix.GetLength(0), matrix.GetLength(1));
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    nodes.Add(new Node(i, j, matrix[i, j]));
                }
            }
            foreach(var node in nodes)
            {
                node.AddConnectedNodes(nodes.Where(n => (n.X == node.X && Math.Abs(n.Y - node.Y) == 1) ^ (n.Y == node.Y && Math.Abs(n.X - node.X) == 1)).ToList());
            }
            targetNode = nodes.Where(node => node.X == gridSize.Item1 - 1 && node.Y == gridSize.Item2 - 1).First();
        }

        public void FindShortestPath()
        {
            // start at 0,0
            var currentNode = nodes.Where(node => node.X == 0 && node.Y == 0).First();
            currentNode.SetCumulatedRisk(-currentNode.RiskFactor);
            while (true)
            {
                foreach(var node in currentNode.ConnectedNodes)
                {
                    node.SetCumulatedRisk(currentNode.CumulatedRisk);
                }
                currentNode.Visited = true;
                nodes.Remove(currentNode);
                currentNode = GetNextNode();
                if(currentNode == null || currentNode == targetNode)
                {
                    break;
                }
            }
        }

        public int GetShortestPathLenth()
        {
            FindShortestPath();
            return targetNode.CumulatedRisk;
        }

        public Node GetNextNode()
        {
            try
            {
                return nodes.OrderBy(n => n.CumulatedRisk).First();
            }
            catch (Exception)
            {
                return null;
            }            
        }
    }


    class Node
    {
        private List<Node> connectedNodes;
        private int x;
        private int y;
        private int riskFactor;
        private int cumulatedRisk = Int32.MaxValue;
        private bool visited = false;

        public Node(int x, int y, int riskFactor)
        {
            this.x = x;
            this.y = y;
            this.riskFactor = riskFactor;
        }

        public int CumulatedRisk { get => cumulatedRisk; }
        public bool Visited { get => visited; set => visited = value; }
        public int X { get => x; }
        public int Y { get => y; }
        public int RiskFactor { get => riskFactor; }
        internal List<Node> ConnectedNodes { get => connectedNodes; }

        public void AddConnectedNodes(List<Node> node)
        {
            connectedNodes = new(node);
        }

        public void SetCumulatedRisk(int preceding)
        {
            if (this.RiskFactor + preceding < this.cumulatedRisk)
            {
                this.cumulatedRisk = RiskFactor + preceding;
            }
        }
    }
}
