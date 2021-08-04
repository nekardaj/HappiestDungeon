using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum StatusEffects {Weak, Vurneable, Inspired , Armored, Poisoned} //0.75 dmg out, 1.25 dmg in, 1,25 dmg out, 0.75 dmg in, Const dmg DOT
    class Ability
    {
        public Ability()
        {

        }
        bool TargetsEnemy
        {
            get;
        }
        int Dmg //if targets ally =healing amount
        {
            get;
        }



    }
}
