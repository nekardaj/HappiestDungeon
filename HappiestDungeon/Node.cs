using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum NodeType {Combat, Boss, Event }; //for now we just use Combat and Boss
    class Node //we dont need graph to wrap methods around nodes as we dont do any pathfinding
    {
        const int MaxDeg=4; // maximum out(v) of a vertex (the maze is planned to be ascension so there will be around 3 ways to higher floor)
        //const is also static
        private int links; //holds number of links from node
        public void AddLink(Node node)
        {
            NextNodes[links++] = node; //NextN[links] is the place where the next element goes
        }

        private NodeType Type;
        public Node(NodeType type)
        {
            Type = type;
            NextNodes = new Node[MaxDeg]; //initalizes array of neighbours filled with null 
        }
        public Node[] NextNodes { get; }

    }
}
