using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    interface ITurnOrder
    {
        /// <summary>
        /// Should take care of dead heroes
        /// </summary>
        /// <returns>Returns the hero that should take their turn now</returns>
        Hero GetNext();
        void AddHero(Hero hero);
        void AddHeroes(Heroes heroes);
        void Reset();
    }
}
