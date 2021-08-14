using System;
using System.Collections.Generic;

namespace HappiestDungeon
{
    class Map
    {
        private Node Current;

        public Map()
        {
            Nodes = new List<Node>();
        }
        public virtual void GenerateMap() //generates nodes from static data
        {
            Console.WriteLine(Data.Map.Length);
            int size = Data.Map.Length;
            for (int i = 0; i < size; i++)
            {
                Nodes.Add(new Node(NodeType.Combat));
            }
            Nodes.Add(new Node(NodeType.Boss)); //implicit boss
            for (int i = 0; i < Data.Map.Length; i++)
            {
                foreach (int ID in Data.Map[i]) //data.map[i] holds list of int(order in List Nodes)
                {
                    Nodes[i].AddLink(Nodes[ID]);       
                }
            }

            foreach (List<int> Connections in Data.ToBeGenerated) //chooses one random node from list of potential connections
            {
                Node node = Nodes[Connections[0]];
                Random r = new Random();
                int ID = r.Next(1, Connections.Count);
                node.AddLink(Nodes[Connections[ID]]);
            }
        }

        public void PrintMap() //prints list of links between nodes
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                foreach (Node node in Nodes[i].NextNodes)
                {
                    if (node != null)
                    {
                        Console.WriteLine(i + "-->" + Nodes.IndexOf(node));       
                    }
                }
            }
        }

        public Node GetCurrent()
        {
            return Current;
        }
        public void SetCurrent(int link) //this allows only movement in desired direction
        {
            Current = Current.NextNodes[link];
        }

        private List<Node> Nodes;
    }
}