using System;
using System.Collections.Generic; //TODO

namespace HappiestDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<StatusEffects, int> dict= new Dictionary<StatusEffects, int> { };
            Console.WriteLine("You died and failed your quest!");
            foreach (StatusEffects effects in (StatusEffects[])Enum.GetValues(typeof(StatusEffects)))
            {
                Console.WriteLine(effects);    //creates dict entry for all statuses
                dict.Add(effects, 5);
            }
            dict.TryGetValue(StatusEffects.Armored, out int val);
            Map map = new Map();
            map.GenerateMap();
            map.PrintMap();
            Console.WriteLine(val);
            
        }
    }
}
