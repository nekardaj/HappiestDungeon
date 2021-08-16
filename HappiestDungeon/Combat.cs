using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    static class Combat
    {
        public static bool Fight(Heroes allies, Heroes enemies, Game game) //processes combat and returns true if player won the fight(=game goes on)
        {
            TurnOrder.Reset();
            foreach (Hero hero in allies.HeroList)
            {
                hero.CombatStart(); //at start of combat allies regain some of their hp
                TurnOrder.AddHero(hero);
            }
            TurnOrder.AddHeroes(enemies);
            //adds all combatants into queue

            while (allies.GetHeroCount() > 0 && enemies.GetHeroCount() > 0)
            {
                Hero actingHero = TurnOrder.GetNext();
                Console.WriteLine($"{actingHero.Name}´s turn\n"); //TODO debug only
                actingHero.TakeTurn(game, allies, enemies); //hero wont act when dead, we just need to not enqueue them again(would remove self twice)
                if (actingHero.HP > 0)
                //combat needs to end as soon as one side is completely dead -> hero targeted by ability should return if he survived and get removed in case they died
                //otherwise player could be able to cast spells before game found out all enemies are dead -> implemented inside hero
                {
                    TurnOrder.AddHero(actingHero);
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
        static ITurnOrder TurnOrder = new SimpleQueue();
        //we could use simpler data structures(cyclic array/list) but we would need to solve some special cases
    }
}
