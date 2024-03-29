﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum NodeType {Combat, Boss, Event }; //for now we just use Combat and Boss
    class Node : ILogging 
    {
        const int MaxDeg=4; // maximum out(v) of a vertex (the maze is planned to be ascension so there will be around 3 ways to higher floor)
        //const is also static
        protected int links; //holds number of links from node
        public void AddLink(Node node)
        {
            NextNodes[links++] = node; //NextN[links] is the place where the next element goes
        }
        //when player chooses the next room between more nodes of same type descriptions should differ a bit
        readonly static Dictionary<NodeType, string[]> Description = new Dictionary<NodeType, string[]>
        {
            {NodeType.Combat,new string[]
                {
                    "Judging from the sound there definitly is someone. And that someone does not sound friendly at all.",
                    "The noise coming from there sounds like an angry orc. After what you saw here this might not be the worst possibility.",
                    "A deafening shriek breaks the silence. At least you can be sure you didnt go the wrong way.",
                    "You hear a scream of triumph and sound of breaking bones. You better make sure not to end up the same."
                } 
            },
            {NodeType.Event, new string[]
                {
                    "You dont hear anything coming from this direction. A flickering light suggests someone might be there.",
                    "Surprisingly cold wind comes from this room, but apart from that the room seems calm.",
                    "Purple glow around the walls seems to be work of some twisted magic, but if you are lucky the person might already be gone."
                } 
            },
            {NodeType.Boss,new string[] { "This is it. You can hear a big creature screaming from afar. Brace yourself." } }
        };

        public string ReturnDescription()
        {
            string[] desc = Description[Type];
            Random r = new Random();
            return desc[r.Next(desc.Length)];
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
