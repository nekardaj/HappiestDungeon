using System;

namespace HappiestDungeon
{
    class TerminalLogging : Igraphics
    {
        public virtual void Render()
        {
            //Console.Clear();
            //TODO debug only
            Console.WriteLine("\n-----Render-----\n");
            Console.WriteLine(Log);
        }

        public virtual void UpdateData(Game game)
        {
            Log = game.ActionDescr;
            //the method that requires to print smth takes care of the string prep
            //game is passed in case more data need to be read
        }
        protected string Log;

    }
}