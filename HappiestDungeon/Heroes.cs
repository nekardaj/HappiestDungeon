using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Heroes
    {
        public int NextID; //holds the lowest possible unique hero ID, removed heroes are iqnored here
        public int GetHeroCount()
        {
            return HeroList.Count;
        }

        public List<Hero> HeroList
        {
            get;
        }
        public Heroes (Hero[] heroes)
        {
            this.HeroList.AddRange(heroes);
            NextID = heroes.Length;
        }
        public void AddHero(Hero hero)
        {
            HeroList.Add(hero);
            NextID += 1;
        }
        public void RemoveHero(Hero hero)
        {
            HeroList.Remove(hero);
        }
        public Hero GetHeroFromID(int ID)
        {
            foreach(Hero hero in this.HeroList)
            {
                if(hero.ID==ID)
                {
                    return hero;
                }
            }
            return null;
        }

    }
}
