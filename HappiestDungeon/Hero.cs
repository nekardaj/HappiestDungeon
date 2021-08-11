using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Hero : ILogging
    {
        public Hero(bool enemy, int id, int maxHp, int hp, Ability[] abilities, string name)
        {
            Status = new Dictionary<StatusEffects, int> { };
            foreach( StatusEffects effects in (StatusEffects[]) Enum.GetValues(typeof(StatusEffects)))
            {
                Status.Add(effects, 0); //creates dict entry for all statuses with default value(=not active)
            }
            Enemy = enemy;
            ID = id;
            MaxHP = maxHp;
            HP = hp;
            Abilities = abilities;
            Name = name;
        }

        public virtual bool TargetedBy(Ability ability, Hero caster) //Each character takes care of spells aimed at them after this gets called
        // if we want to add more complex abilities redefinition will be reqd
        //return value indicates wheter hero survived the ability (true = survived)
        {
            //TODO: calculate dmg based on statuses
            if(Enemy ^ caster.Enemy) //if caster and target are in different team it deals dmg else it heals
            {

            }
            //Status.TryGetValue(StatusEffects.Armored, out int armored);
            return HP > 0;
        }

        public virtual void TakeTurn(Game game, Heroes allies, Heroes enemies) //maybe just a pointer to the input class, can be redefined for smarter ability choice
        {
            if (Enemy)
            {
                Random random = new Random();
                int ability = random.Next(0, Abilities.Length); //we consider that enemy spells dont have cd
                //choose a target depending on TargetsEnemy
            }
        }
        public bool Enemy
        {
            get;
        }
        public string Name
        {
            get;
        }
        public int Maxactive //maybe static max
        {
            get;
        }
        public int ID
        {
            get;
        }
        public int MaxHP
        {
            get;
        }
        public int HP
        {
            get;
        }
        public Ability[] Abilities //list of all abilities
        {
            get;
        }
        public Ability[] Actives //active abilities
        {
            get;
        }
        public Dictionary<StatusEffects, int> Status
        {
            get;
            private set;
        }

        protected virtual bool AbilityReady(Ability ability) //this could be used to implement cooldowns
        {
            return true;
        }

        public virtual string ReturnDescription() //player will need to choose who to cast spell on
        {
            string statuses="";
            foreach (KeyValuePair<StatusEffects,int> status in Status)
            {
                if (status.Value != 0)
                {
                    statuses += String.Format("\nThe {0} is {1} for {2} turns", Enemy? "enemy":"ally",status.Key.ToString(),status.Value);
                }
            }

            return $"{Name} HP: {HP} {statuses}";
        }
    }
}
