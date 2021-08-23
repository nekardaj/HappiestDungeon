using System;
using System.Collections.Generic; //TODO

namespace HappiestDungeon
{
    class Program
    {
        static void Main(string[] args)
        {          
            Phase phase = new Phase(Phasetype.Setup); //player can reselect spells at start of game
            Game game = new Game(phase, new TerminalLogging(), new TerminalInput());
            game.Run();
        }
    }
}
