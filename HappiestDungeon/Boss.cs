using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class Boss : Hero
    {
        public Boss(bool enemy, int ID, int maxHP, int hp, Ability[] abilities, string name, Action<Heroes,Heroes, Boss, Game> ai) : base(enemy, ID, maxHP, hp,abilities, name)
        {
            AI = ai;
        }
        Action<Heroes, Heroes, Boss, Game> AI;
        int Counter=0; //can be used for counting what to cast
        public int Count()
        {
            return Counter++;
        }
        public override void TakeTurn(Game game, Heroes allies, Heroes enemies)
        {
            if (!ProcessStatuses(allies, enemies)) { return; };
            AI(allies, enemies, this, game); //everything except processing statuses has to be done here
        }
    }
}
