using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum NodeType {Combat, Boss, Event }; //for now we just use Combat and Boss
    class Node : ILogging 
    {
        const int MaxDeg=4; // maximum out(v) of a vertex (the maze is planned to be ascension so there will be around 3 ways to higher floor)
        //const is also static
        private int links; //holds number of links from node
        public void AddLink(Node node)
        {
            NextNodes[links++] = node; //NextN[links] is the place where the next element goes
        }
        readonly static Dictionary<NodeType, string> Description = new Dictionary<NodeType, string>
        {
            {NodeType.Combat, "Judging from the noise there definitly is someone. And that someone does not sound friendly at all." },
            {NodeType.Event, "You dont hear anything coming from this direction. A flickering light suggests someone is there" },
            {NodeType.Boss, "This is it. You can hear a big creature screaming from afar. Brace yourself" }
        };
        public string ReturnDescription()
        {
            return Description[Type];
        }

        public NodeType Type
        {
            get;
        }
        public Node(NodeType type)
        {
            Type = type;
            NextNodes = new Node[MaxDeg]; //initalizes array of neighbours filled with null 
        }
        public Node[] NextNodes { get; }

    }
}
