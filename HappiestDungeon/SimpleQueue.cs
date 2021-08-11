using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class SimpleQueue : ITurnOrder
    {
        Queue<Hero> HeroQueue = new Queue<Hero> { };
        public void AddHero(Hero hero)
        {
            HeroQueue.Enqueue(hero);
        }
        public void AddHeroes(Heroes heroes)
        {
            foreach (Hero hero in heroes.HeroList)
            {
                HeroQueue.Enqueue(hero);
            }
        }

        public Hero GetNext()
        {
            Hero next = HeroQueue.Dequeue();
            while (next.HP <= 0)
            {
                next = HeroQueue.Dequeue();
            }
            return next;
        }

        public void Reset()
        {
            HeroQueue.Clear();
        }
    }
}
