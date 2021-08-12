using System;

namespace HappiestDungeon
{
    enum LastAction {Transit,CombatStart, ActionMade, CombatOver, Looting, SetupStart, SetupChoiceMade };
    public class TerminalLogging : Igraphics
    {
        public void Render()
        {
            Console.Clear();
        }

        public virtual void UpdateData()
        {
            //TODO Save LastAction and its context into some Game property   
        }
    }
}