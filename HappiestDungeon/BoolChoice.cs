using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class BoolChoice : ILogging
    {
        bool Agree; //positive answer
        public BoolChoice(bool agree)
        {
            Agree = agree;
        }
        public string ReturnDescription()
        {
            return Agree? "Hell yeah!" : "Nah";
        }
    }
}
