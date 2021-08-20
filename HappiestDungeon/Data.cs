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
        // modifying this changes bosses behaviour
        public static readonly Action<Heroes, Heroes, Boss, Game> bossAI =
            (Heroes allies, Heroes enemies, Boss boss, Game game) =>
            {
                Random random = new Random();
                int abilityIndex = boss.Count() % boss.Abilities.Length; //boss has all abilities active
                Ability ability = boss.Abilities[abilityIndex];
                if (ability.TargetsEnemy) //enemy uses on enemy -> allies
                {
                    Hero target = allies.HeroList[random.Next(allies.HeroList.Count)];
                    game.ActionDescr = $"{boss.Name} used: {ability.ReturnDescription()}";
                    if (!target.TargetedBy(ability, boss)) //target did not survive(its enemy)
                    {
                        allies.RemoveHero(target);
                    }

                }
                else
                {
                    Hero target = enemies.HeroList[random.Next(enemies.HeroList.Count)];
                    game.ActionDescr = $"{boss.Name} used: {ability.ReturnDescription()}";
                    target.TargetedBy(ability, boss);
                }
                return;
            };

        
        public static readonly Heroes Allies = new Heroes
        (
            //TODO fix here
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
                    new Ability(125, true, new List<Tuple<StatusEffects, int>>
                    {
                        new Tuple<StatusEffects, int>(StatusEffects.Poisoned,3)
                    },"Love is in the air"),
                    new Ability(45, true, new List<Tuple<StatusEffects, int>>
                    {
                        
                    },"Wide smile"),
                    new Ability(15, true, new List<Tuple<StatusEffects, int>>
                    {
                        new Tuple<StatusEffects, int>(StatusEffects.Vurneable,3),new Tuple<StatusEffects, int>(StatusEffects.Weak,3)
                    },"Walking on sunshine"),
                    new Ability(35, false, new List<Tuple<StatusEffects, int>>
                    {
                        new Tuple<StatusEffects, int>(StatusEffects.Armored,2)
                    },"Nervous chuckle")

                }, "Ironchad")
            
        );

        public static readonly Ability[] abilities = new Ability[]
        {
            //TODO
            new Ability
            (
                30,true,new List<Tuple<StatusEffects, int>>{ new Tuple<StatusEffects, int> (StatusEffects.Vurneable,2) }, "Nightmare"
            ),
            new Ability
            (
                40,true,new List<Tuple<StatusEffects, int>>{}, "Wild strike" //list cant be null
            )
            ,
            new Ability
            (
                25, false, new List<Tuple<StatusEffects, int>>
                {new Tuple<StatusEffects, int> (StatusEffects.Armored,1),
                    new Tuple<StatusEffects, int> (StatusEffects.Inspired,2)
                }, "Dark embrace"
            ),
            new Ability
            (
                25, true, new List<Tuple<StatusEffects, int>>
                {new Tuple<StatusEffects, int> (StatusEffects.Weak,2),
                    new Tuple<StatusEffects, int> (StatusEffects.Poisoned,2)
                }, "Dirty trick"
            ),
            new Ability
            (
                45, true, new List<Tuple<StatusEffects, int>>
                {
                    new Tuple<StatusEffects, int> (StatusEffects.Vurneable,1)
                }, "Overwhelm"
            )

        };
        /*
        public static readonly Hero[] Enemies = new Hero[]
        {
            new Hero(true,0,150,150,null, "Stormy cloud")
        };
        */
        public static readonly HeroTemplate[] EnemyTemplates = new HeroTemplate[]
        {
            new HeroTemplate(true,0,150,new Ability[] {abilities[0], abilities[1] },"Mexican Joker"),/*,
            new HeroTemplate(true,2,175,new Ability[] {abilities[1], abilities[2] },"ManBearPig")*/ //self healing enemy takes long to kill
            new HeroTemplate(true,3,125,new Ability[] {abilities[0], abilities[1] },"Kairan")
        };
        //class enemy that overrides taketurn could be sol to ai

        public static readonly Hero Boss = new Boss
            (true,1, 200,200,
                new Ability[] { Data.abilities[3], Data.abilities[0], Data.abilities[4], Data.abilities[2], },
                "Professor Chaos",bossAI
            );


    }
}