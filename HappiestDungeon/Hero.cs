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

        protected static readonly Tuple<StatusEffects, float>[] CasterMultipliers = new Tuple<StatusEffects, float>[] {new Tuple<StatusEffects, float>(StatusEffects.Inspired,1.25f),
            new Tuple<StatusEffects, float>(StatusEffects.Weak,0.75f)};
        protected static readonly Tuple<StatusEffects, float>[] TargetMultipliers = new Tuple<StatusEffects, float>[] {new Tuple<StatusEffects, float>(StatusEffects.Vurneable,1.25f),
            new Tuple<StatusEffects, float>(StatusEffects.Armored,0.75f)};
        public virtual bool TargetedBy(Ability ability, Hero caster) //Each character takes care of spells aimed at them after this gets called
        // if we want to add more complex abilities redefinition will be reqd
        //return value indicates wheter hero survived the ability (true = survived)
        {
            //TODO: calculate dmg based on statuses
            float multiplier = 1f; //no need to use double as we round it anyway
            foreach (Tuple<StatusEffects,float> mul in CasterMultipliers)
            {
                caster.Status.TryGetValue(mul.Item1, out int duration);
                if (duration != 0) //this enables "permanent buffs" represented with negative duration
                {
                    multiplier *= mul.Item2; //(de)buff is active - use multiplier
                }
            }
            foreach (Tuple<StatusEffects,float> mul in TargetMultipliers) //takes care of effects on target(these are different than those on caster so we use two iterations)
            {
                caster.Status.TryGetValue(mul.Item1, out int duration);
                if (duration != 0) //this enables "permanent buffs" represented with negative duration
                {
                    multiplier *= mul.Item2; //(de)buff is active - use multiplier
                }
            }
            if(Enemy ^ caster.Enemy) //if caster and target are in different team it deals dmg else it heals
            {
                HP -= (int)Math.Round(multiplier * ability.Dmg, MidpointRounding.ToPositiveInfinity);
                //dmg/heal is rounded to closest number and up at midpoint
            }
            else //caster is friendly - heal
            {
                HP = Math.Min((int)Math.Round(multiplier * ability.Dmg, MidpointRounding.ToPositiveInfinity), MaxHP); //can heal only to MaxHP
            }
            //Status.TryGetValue(StatusEffects.Armored, out int armored);
            foreach (Tuple<StatusEffects,int> effect in ability.Effects)
            {
                int duration = Status[effect.Item1];
                if (duration >= 0) //if we used permabuffs as neg numbers this adding could break it
                {
                    Status[effect.Item1] += effect.Item2; //increases duration
                }
            }
            return HP > 0;
        }
        public virtual void ReselectSpells(Game game)
        {
            //TODO
        }
        public virtual void TakeTurn(Game game, Heroes allies, Heroes enemies) //maybe just a pointer to the input class, can be redefined for smarter ability choice
        {
            //TODO decrease duration of statuses
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
            protected set;
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
