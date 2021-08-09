using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    interface IChoice
    {
        /// <summary>
        /// Asks player to choose from options that were added after last reset.
        /// </summary>
        /// <returns>
        /// zero based number of possibility user choosed
        /// ordered from first added choice to last
        /// </returns>
        int GetChoice(string promt);
        /// <summary>
        /// Adds a new choice
        /// Ilogging is used to extract message for user
        /// </summary>
        /// <param name="choice"></param>
        void AddChoice(ILogging choice);
        /// <summary>
        /// empties list of posibilities
        /// </summary>
        void ResetChoices();
        
    }
}
