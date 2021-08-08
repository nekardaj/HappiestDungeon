using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum Phasetype {Encounter, Looting, Setup, Transit}; //type of Phase, setup = changing spells etc., details about encounter is read from node
    //there is currently no looting
    class Phase
    {
        protected static readonly Dictionary<Phasetype, Phasetype> Transitions = new Dictionary<Phasetype, Phasetype> 
        { 
            { Phasetype.Encounter, Phasetype.Looting }, { Phasetype.Looting, Phasetype.Setup }, { Phasetype.Setup, Phasetype.Transit},
            { Phasetype.Transit, Phasetype.Encounter }, { Phasetype.Encounter, Phasetype.Setup },  { Phasetype.Encounter, Phasetype.Encounter },  
        }; //the transitions should not be changed during game
        public Phasetype Phasetype
        {
            get;
            private set;
        }
        public virtual void NextPhase() //after the phase was executed this switches to the next Phase, virtual in case of complex transitioning
        {
            Phasetype = Transitions[Phasetype];
        }
        
        public Phase(Phasetype phasetype)
        {
            Phasetype = phasetype;
        }
    }
}
