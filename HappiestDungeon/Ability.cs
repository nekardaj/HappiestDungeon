using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    enum StatusEffects {Weak, Vurneable, Inspired , Armored, Poisoned} //0.75 dmg out, 1.25 dmg in, 1,25 dmg out, 0.75 dmg in, Const dmg DOT
    class Ability : ILogging
    {
        public Ability(int dmg, bool targetsEnemy,List<Tuple<StatusEffects,int>> statusEffects, string name)
        {
            TargetsEnemy = targetsEnemy;
            Dmg = dmg;
            Effects = statusEffects;
            Name = name;
        }
        public string Name
        {
            get;
        }
        public List<Tuple<StatusEffects,int>> Effects //list of status effects inflicted on target with duration
        {
            get;
            protected set;
        }

        bool TargetsEnemy
        {
            get;
        }
        int Dmg //if targets ally =healing amount
        {
            get;
        }

        public string ReturnDescription()
        {
            string statuses = "";
            foreach (Tuple<StatusEffects,int> effect in Effects) //assumes list of statuses is filled only with those really inflicted
            {
                statuses += effect.Item1.ToString() + ": " + effect.Item2 + ", ";
            }
            return Name + " dmg/heal: " + Dmg + " " + statuses;
        }
    }
}
