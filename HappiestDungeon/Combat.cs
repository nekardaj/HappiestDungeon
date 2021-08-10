using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    static class Combat
    {
        public static bool Fight(Heroes heroes, Heroes enemies, Game game) //processes combat and returns true if player won the fight(=game goes on)
        {
            foreach (Hero hero in heroes.HeroList)
            {
                TurnOrder.Enqueue(hero);
            }
            foreach (Hero hero in enemies.HeroList)
            {
                TurnOrder.Enqueue(hero);
            }
            //Targeted by should return whether hero survived or not and the we can remove him from list, if its empty fight is over
            //adds all combatants into queue
            return true;//player won
        }

        static Queue<Hero> TurnOrder = new Queue<Hero>();
        
    }
}
