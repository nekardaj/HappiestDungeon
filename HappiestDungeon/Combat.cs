﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    static class Combat
    {
        public static bool Fight(Heroes allies, Heroes enemies, Game game) //processes combat and returns true if player won the fight(=game goes on)
        {
            foreach (Hero hero in allies.HeroList)
            {
                TurnOrder.Enqueue(hero);
            }
            foreach (Hero hero in enemies.HeroList)
            {
                TurnOrder.Enqueue(hero);
            }
            //adds all combatants into queue

            while (allies.GetHeroCount() > 0 && enemies.GetHeroCount() > 0)
            {
                Hero actingHero = TurnOrder.Dequeue();
                if (actingHero.HP > 0) //combat needs to end as soon as one side is completely dead -> hero targeted by ability should return if he survived and get removed in case they died
                //otherwise player could be able to cast spells before game found out all enemies are dead
                {
                    actingHero.TakeTurn(game, allies, enemies);
                    TurnOrder.Enqueue(actingHero); 
                }
                /*
                else {allies.RemoveHero(actingHero);} //now is taken care of in take turn
                */
            }
            //Targeted by should return whether hero survived or not and the we can remove him from list, if its empty fight is over
            if(allies.GetHeroCount()==0)
            {
                return false;
            }
            return true;//player won
        }

        static Queue<Hero> TurnOrder = new Queue<Hero>();
        //we could use simpler data structures(cyclic array/list) but we would need to solve some special cases
    }
}
