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
            while(Update())
            {
                //either process phase or maybe use dictionary<Phasetype,Func> and pass it in constr of Game
            }
        }
        string Outro; //after game is over it holds the prompt to be printed

        public bool Update() //executes one step of the game
        {
            switch (Phase.Phasetype) //the execution of phase depends on its type
                //Even after extensions there should be reasonable number of phases
            {
                case Phasetype.Encounter:
                    break;
                default:
                    break;
            }
            return false;
        }
        public Game(Phase phase) //static data can be adressed directly
        {
            Phase = phase; //enables passing children of Phase
        }

        /// Batch of functions that process the Phases
        protected virtual void Transition() //is called when the phase
        {

        }
        protected virtual void Encounter()
        {

        }
        protected virtual void Setup()
        {

        }
        protected virtual void Looting()
        {

        }
    }
}
