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
        }

        public Heroes Allies //player controlled group of characters, enemies will be generated at entry time
        {
            get;
        }
        

        public Game() //static data can we adressed directly
        {
            
        }
    }
}
