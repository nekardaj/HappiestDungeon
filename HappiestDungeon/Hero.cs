using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Hero
    {
        public Hero()
        {
            Status = new Dictionary<StatusEffects, int> { };
            foreach( StatusEffects effects in (StatusEffects[]) Enum.GetValues(typeof(StatusEffects)))
            {
                Status.Add(effects, 0); //creates dict entry for all statuses with default value(=not active)
            }

        }
        public virtual void TargetedBy(Ability ability) //Each character takes care of spells aimed at them after this gets called
        // if we want to add more complex abilities redefinition will be reqd
        {
            //Status.TryGetValue(StatusEffects.Armored, out int armored);
        }

        public void TakeTurn(Game game) //maybe just a pointer to the input class
        {

        }
        bool Enemy
        {
            get;
        }
        public int Maxactive //maybe static max
        {
            get { return Maxactive; }
        }
        public int ID
        {
            get {return ID;}
        }
        public int MaxHP
        {
            get { return MaxHP; }
        }
        public int HP
        {
            get { return HP; }
        }
        public Ability[] Abilities //list of all abilities
        {
            get { return Abilities; }
        }
        public Ability[] Actives //active abilities
        {
            get { return Actives; }
        }
        Dictionary<StatusEffects, int> Status;

    }
}
