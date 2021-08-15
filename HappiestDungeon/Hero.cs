using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappiestDungeon
{
    class Hero : ILogging
    {
        static readonly int PoisonDmg = 9; //when changing make sure raw dmg spell doesnt deal more than poison based ones
        public Hero(bool enemy, int id, int maxHp, int hp, Ability[] abilities, string name)
        {
            Status = new Dictionary<StatusEffects, int> { };
            foreach( StatusEffects effects in (StatusEffects[]) Enum.GetValues(typeof(StatusEffects)))
            {
                Status.Add(effects, 0); //creates dict entry for all statuses with default value(=not active)
            }
            Enemy = enemy;
            ID = id;
            MaxHP = maxHp;
            HP = hp;
            Abilities = abilities;
            Name = name;
            Maxactive = 4;
            Actives = abilities.Take(Maxactive).ToArray(); //by default we take first spells as the starting ones
        }

        protected static readonly Tuple<StatusEffects, float>[] CasterMultipliers = new Tuple<StatusEffects, float>[] {new Tuple<StatusEffects, float>(StatusEffects.Inspired,1.25f),
            new Tuple<StatusEffects, float>(StatusEffects.Weak,0.75f)};
        protected static readonly Tuple<StatusEffects, float>[] TargetMultipliers = new Tuple<StatusEffects, float>[] {new Tuple<StatusEffects, float>(StatusEffects.Vurneable,1.25f),
            new Tuple<StatusEffects, float>(StatusEffects.Armored,0.75f)};
        public virtual bool TargetedBy(Ability ability, Hero caster) //Each character takes care of spells aimed at them after this gets called
        // if we want to add more complex abilities redefinition will be reqd
        //return value indicates wheter hero survived the ability (true = survived)
        {
            //TODO: calculate dmg based on statuses
            float multiplier = 1f; //no need to use double as we round it anyway
            foreach (Tuple<StatusEffects,float> mul in CasterMultipliers)
            {
                caster.Status.TryGetValue(mul.Item1, out int duration);
                if (duration != 0) //this enables "permanent buffs" represented with negative duration
                {
                    multiplier *= mul.Item2; //(de)buff is active - use multiplier
                }
            }
            foreach (Tuple<StatusEffects,float> mul in TargetMultipliers) //takes care of effects on target(these are different than those on caster so we use two iterations)
            {
                caster.Status.TryGetValue(mul.Item1, out int duration);
                if (duration != 0) //this enables "permanent buffs" represented with negative duration
                {
                    multiplier *= mul.Item2; //(de)buff is active - use multiplier
                }
            }
            if(Enemy ^ caster.Enemy) //if caster and target are in different team it deals dmg else it heals
            {
                HP -= (int)Math.Round(multiplier * ability.Dmg, MidpointRounding.ToPositiveInfinity);
                //dmg/heal is rounded to closest number and up at midpoint
            }
            else //caster is friendly - heal
            {
                HP = Math.Min((int)Math.Round(multiplier * ability.Dmg, MidpointRounding.ToPositiveInfinity) + HP, MaxHP); //can heal only to MaxHP
            }
            //Status.TryGetValue(StatusEffects.Armored, out int armored);
            foreach (Tuple<StatusEffects,int> effect in ability.Effects)
            {
                int duration = Status[effect.Item1];
                if (duration >= 0) //if we used permabuffs as neg numbers this adding could break it
                {
                    Status[effect.Item1] += effect.Item2; //increases duration
                }
            }
            return HP > 0;
        }
        public virtual void ReselectSpells(Game game) //we pass pointer to game in case of redefinition
        {
            //TODO
            for (int i = 0; i < Maxactive; i++)
            {
                game.Input.ResetChoices();
                game.Input.AddChoice(new BoolChoice(true));
                game.Input.AddChoice(new BoolChoice(false));
                bool reset = game.Input.GetChoice($"Do you want to choose different spell for this slot?\n Currently: {Actives[i].ReturnDescription()}")==0;
                if (reset)
                {
                    game.Input.ResetChoices();
                    foreach (var ability in Abilities)
                    {
                        game.Input.AddChoice(ability);    
                    }

                    Ability newAbility = Abilities[game.Input.GetChoice("Choose the ability to replace the old one with.")]; //if the user chooses spell multiple times its their problem
                    Actives[i] = newAbility;
                }
            }
        }
        protected virtual bool ProcessStatuses(Heroes allies, Heroes enemies)
        {
            //poison does smth on decrease
            if (Status[StatusEffects.Poisoned] > 0)
            {
                HP -= PoisonDmg;
            }
            if (HP <= 0) //hero could die after poison tick, we need to remove him from combact
            {
                if (Enemy)
                {
                    enemies.RemoveHero(this);
                }
                else
                {
                    allies.RemoveHero(this);
                }
                return false; //hero cant take turn they died
            }
            foreach (StatusEffects effects in (StatusEffects[])Enum.GetValues(typeof(StatusEffects))) //foreach on keyvaluepairs throws exep when modified so we iterate with all effects
            {
                if (Status[effects] > 0)
                {
                    Status[effects] = Status[effects] - 1;
                }
            }
            return true; //hero survived
            //decrease duration of statuses - done
        }
        public virtual void TakeTurn(Game game, Heroes allies, Heroes enemies) //maybe just a pointer to the input class, can be redefined for smarter ability choice
        {
            if (!ProcessStatuses(allies, enemies)) { return; };
            game.ActionDescr=$"It is {Name}´s turn.\n";
            if (!Enemy)
            {
                string combatants = "Currently standing: ";
                foreach (Hero hero in allies.HeroList)
                {
                    combatants += hero.ReturnDescription();
                }
                foreach (Hero hero in enemies.HeroList)
                {
                    combatants += hero.ReturnDescription();
                }
                game.ActionDescr += combatants; //displays the info
                game.Graphics.Render();
                game.Input.ResetChoices();
                foreach (var ability in Actives)
                {
                    game.Input.AddChoice(ability);
                }

                Ability casted = Actives[game.Input.GetChoice("Choose the ability to cast.")];
                game.Input.ResetChoices();
                //TODO choosing target throws exeception
                //ally targets enemy -> enemies
                foreach (var hero in casted.TargetsEnemy ? enemies.HeroList : allies.HeroList)
                {
                    game.Input.AddChoice(hero);
                }
                int targetIndex = game.Input.GetChoice("Choose the target.");
                Hero target = casted.TargetsEnemy ? enemies.HeroList[targetIndex] : allies.HeroList[targetIndex];
                if (!target.TargetedBy(casted, this)) //target did not survive(its enemy)
                {
                        enemies.RemoveHero(target);
                }
            }
            else
            {
                Random random = new Random();
                int abilityIndex = random.Next(0, Abilities.Length); //we consider that enemy spells dont have cd
                                                                     //choose a target depending on TargetsEnemy
                Ability ability = Abilities[abilityIndex];
                if(ability.TargetsEnemy) //enemy uses on enemy -> allies
                {
                    Hero target = allies.HeroList[random.Next(allies.HeroList.Count)];
                    if (!target.TargetedBy(ability, this)) //target did not survive(its enemy)
                    {
                        allies.RemoveHero(target);
                    }
                    
                }
                else
                {
                    Hero target = enemies.HeroList[random.Next(enemies.HeroList.Count)];
                    target.TargetedBy(ability, this);
                }
                return;
            }
        }
        public virtual void CombatStart() //things to be done at start of combat, for now just heal if not full
        {
            HP = Math.Min(MaxHP, HP + (MaxHP >> 2)); //heals for 1/4 of maxhp
        }
        public bool Enemy
        {
            get;
        }
        public string Name
        {
            get;
        }
        public int Maxactive //maybe static max
        {
            get;
        }
        public int ID
        {
            get;
        }
        public int MaxHP
        {
            get;
        }
        public int HP
        {
            get;
            protected set;
        }
        public Ability[] Abilities //list of all abilities
        {
            get;
        }
        public Ability[] Actives //active abilities
        {
            get;
        }
        public Dictionary<StatusEffects, int> Status
        {
            get;
            private set;
        }

        protected virtual bool AbilityReady(Ability ability) //this could be used to implement cooldowns
        {
            return true;
        }

        public virtual string ReturnDescription() //player will need to choose who to cast spell on
        {
            string statuses="";
            foreach (KeyValuePair<StatusEffects,int> status in Status)
            {
                if (status.Value != 0)
                {
                    statuses += String.Format("\nThe {0} is {1} for {2} turns", Enemy? "enemy":"ally",status.Key.ToString(),status.Value);
                }
            }

            return $"{Name} HP: {HP} {statuses}";
        }
    }
}
