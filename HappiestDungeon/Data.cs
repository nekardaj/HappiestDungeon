using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HappiestDungeon
{
    struct HeroTemplate //holds data for hero constructor, allows creating multiple instances of same enemy easily
    {
        public readonly bool Enemy;
        public readonly int ID;
        public readonly int MaxHP;
        public readonly Ability[] Abilities;
        public readonly string Name;

        public HeroTemplate(bool enemy, int id, int maxHp, Ability[] abilities, string name)
        {
            Enemy = enemy;
            ID = id;
            MaxHP = maxHp;
            Abilities = abilities;
            Name = name;
        }
    }
    static class Data
    {
        public static readonly List<int>[] Map = new List<int>[] //static layout, some nodes miss links that can be generated
        {
            new List<int>(){1,2}, new List<int>(){3}, new List<int>(){4,5}, new List<int>(){6,7},
            new List<int>(){},new List<int>(){8},new List<int>(){9},new List<int>(){9},
            new List<int>(){9} //boss node is implicitly created in map generation
        };
        public static readonly List<int>[] ToBeGenerated = new List<int>[] //holds nodes that should have some nodes added(node number,poential links)
        {
            new List<int>(){4,7,8},
        };
        //private static readonly Action<Game> p = (Game game) => {  };
        /*
        public static readonly Dictionary<Phasetype, Action<Game>> PhaseProcessors = new Dictionary<Phasetype, Action<Game>>
            {
            {Phasetype.Encounter, (Game game) =>
                {
                    Console.WriteLine("Action has been made");
                }
            }
        };
        */
        public static readonly Heroes Allies = new Heroes(new Hero[]
        {
            new Hero(false, 0, 200, 200, 
                new Ability[]
                {
                    new Ability(20, true, new List<Tuple<StatusEffects, int>>
                    { 
                        new Tuple<StatusEffects, int>(StatusEffects.Vurneable,2),new Tuple<StatusEffects, int>(StatusEffects.Weak,2)
                    },"Grin"),
                    new Ability(20, false, new List<Tuple<StatusEffects, int>>
                    {
                        new Tuple<StatusEffects, int>(StatusEffects.Armored,3),new Tuple<StatusEffects, int>(StatusEffects.Inspired,3)
                    },"Heartwarming hug"),
                    new Ability(25, false, new List<Tuple<StatusEffects, int>>
                    {
                        new Tuple<StatusEffects, int>(StatusEffects.Poisoned,3)
                    },"Love is in the air"),
                    new Ability(45, true, new List<Tuple<StatusEffects, int>>
                    {
                        
                    },"Wide smile"),

                }, "Ironchad")
        });
        public static readonly Ability[] abilities = new Ability[]
        {
            //TODO
        };

        public static readonly Hero[] Enemies = new Hero[]
        {
            new Hero(true,0,150,150,null, "Stormy cloud")
        };

        public static readonly HeroTemplate[] EnemyTemplates = new HeroTemplate[]
        {
            new HeroTemplate(true,0,150,null,"Stormy cloud")
        };
            //class enemy that overrides taketurn could be sol to ai

        public static readonly Hero Boss = null;


    }
}