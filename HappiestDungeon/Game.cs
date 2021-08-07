using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Game
    {
        public Node Node //all we need to remember is current node, we just need neighbours enumeration
        {
            get;
            private set;
        }

        public Heroes Allies //player controlled group of characters, enemies will be generated at entry time
        {
            get;
            private set;
        }
        Phase Phase;
        
        public void Run()
        {
            Phase = new Phase(Phasetype.Transit);
            while(Update())
            {
                //either process phase or maybe use dictionary<Phasetype,Func> and pass it in constr of Game
            }
        }
        string Outro; //after game is over it holds the prompt to be printed

        public bool Update() //executes one step of the game
        {

            return false;
        }
        public Game() //static data can we adressed directly
        {
            
        }
    }
}
