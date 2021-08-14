using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Game
    {
        public Map Map //all we need to remember is current node, we just need neighbours enumeration
        {
            get;
        }

        public Heroes Allies //player controlled group of characters, enemies will be generated at entry time
        {
            get;
            private set;
        }
        Phase Phase;
        public Action LastAction //Terminal loging reads this to properly display output based on type of action
        {
            get;
            protected set;
        }
        public string ActionDescr
        {
            get;
            set; //combat needs to have acces and modify it
        }
        public void Run()
        {
            while(Update())
            {
                //either process phase or maybe use dictionary<Phasetype,Func> and pass it in constr of Game
            }

            ActionDescr = Outro;
            Graphics.UpdateData(this);
            Graphics.Render();
        }
        string Outro; //after game is over it holds the prompt to be printed

        public bool Update() //executes one step of the game
        {
            switch (Phase.Phasetype) //the execution of phase depends on its type
                //Even after extensions there should be reasonable number of phases
            {
                case Phasetype.Encounter:
                    return Encounter(); //reurns true if player survived
                case Phasetype.Looting:
                    Looting();
                    return true; //one cant die looting, changing spells or travelling to node
                case Phasetype.Setup:
                    Setup();
                    return true;
                case Phasetype.Transit:
                    Transition();
                    return true;
            }
            return false;
        }
        public IChoice Input
        {
            get;
        }
        public Igraphics Graphics
        {
            get;
        }
        public Game(Phase phase, Igraphics graphics, IChoice choice) //static data can be adressed directly
        {
            Phase = phase; //enables passing children of Phase
            Map = new Map();
            Map.GenerateMap();
            Graphics = graphics;
            Input = choice;
        }
        protected virtual void GenerateAllies()
        {

        }

        /// Batch of functions that process the Phases
        /// TODO add LastAction string modification
        protected virtual void Transition()
        {
            Input.ResetChoices();
            foreach (Node node in Map.GetCurrent().NextNodes) //foreach should iterate in the order 0 - lenght-1
            {
                Input.AddChoice(node);
            }
            Map.SetCurrent(Input.GetChoice("Choose the way to continue.")); //passed is number of link used(prevents travel in opposite dir)
            Node current = Map.GetCurrent();
            //info about arrival will be printed together with event/combat info 
        }
        protected virtual bool Encounter() //returns if player survived encounter
        {
            ActionDescr = "You arrived to the room the noise came from. You ready your blade or whatever.";
            Graphics.Render();
            Heroes GenerateEnemies()
            {
                //TODO
                Random r = new Random();
                HeroTemplate template = Data.EnemyTemplates[r.Next(Data.EnemyTemplates.Length)];
                Heroes enemies = new Heroes(new Hero[] {new Hero(template.Enemy,template.ID,template.MaxHP,template.MaxHP,template.Abilities,template.Name)}); //picks random template from Data and creates enemy
                return enemies; 
            }

            NodeType curr = Map.GetCurrent().Type;
            if (curr == NodeType.Boss)
            {
                Heroes enemies = new Heroes(new Hero[]{Data.Boss});
                if (Combat.Fight(Allies, enemies, this))
                {
                    Outro = "You have done it. The evil is banished for now. Well done.";
                    return false; //boss is down game ends anyway
                }
                Outro = "You were close. Really damn close. Your life and journey end here";
                return false;
            }
            if ( curr == NodeType.Combat)
            {
                Heroes enemies = GenerateEnemies();
                if (Combat.Fight(Allies, enemies, this))
                {
                    return true;
                }
                Outro = "You were slain. Some things were just not meant to be.";
                return false;
            }
            if (curr==NodeType.Event)
            {
                ActionDescr = "You arrive to the mysterious room.";
                Graphics.Render();
                return true;
            }
            return false;
        }
        protected virtual void Setup()
        {
            ActionDescr = "In case you want to use different abilities now is the time.";
            foreach (Hero hero in Allies.HeroList)
            {
                Input.ResetChoices();
                Input.AddChoice(new BoolChoice(true));
                Input.AddChoice(new BoolChoice(false));
                bool setspells = Input.GetChoice($"Do you want to reselect abilities of {hero.Name}?") == 0;
                if (setspells)
                {
                    hero.ReselectSpells(this);
                }

            }
        }
        protected virtual void Looting() //no loot for now
        {

        }
    }
}
