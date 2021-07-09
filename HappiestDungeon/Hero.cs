using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Hero
    {
        public void TargetedBy(Ability ability) //Each character takes care of spells aimed at them after this gets called
        {
            
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
        public int DMGmod //damage modifier
        {
            get { return DMGmod; }
        }

    }
}
