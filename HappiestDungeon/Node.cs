using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum NodeType {Combat, Boss, Event }; //for now we just use Combat and Boss
    class Node
    {
        static int MaxDeg=4; // maximum out(v) of a vertex (the maze is planned to be ascension so there will be around 3 ways to higher floor)
        
        public Node(Node[] Next)
        {
            NextNodes = Next; //for now the max out is unused as the graph will be small anyway
        }
        Node[] NextNodes { get; }

    }
}
