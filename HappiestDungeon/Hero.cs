using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Hero
    {
        public Hero(bool enemy, int id, int maxHp, int hp, Ability[] abilities)
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
        }

        public virtual void TargetedBy(Ability ability) //Each character takes care of spells aimed at them after this gets called
        // if we want to add more complex abilities redefinition will be reqd
        {
            //Status.TryGetValue(StatusEffects.Armored, out int armored);
        }

        public virtual void TakeTurn(Game game, Heroes heroes) //maybe just a pointer to the input class, can be redefined for smarter ability choice
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
        Dictionary<StatusEffects, int> Status;

        private bool AbilityReady(Ability ability) //this could be used to implement cooldowns
        {
            return true;
        }

    }
}
