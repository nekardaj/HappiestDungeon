using System;
using System.Collections.Generic;
using System.Text;

namespace HappiestDungeon
{
    class TerminalInput : IChoice
    {
        List<ILogging> Choices = new List<ILogging> { };
        public void AddChoice(ILogging choice)
        {
            throw new NotImplementedException();
        }

        public int GetChoice(string prompt)
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.WriteLine((i + 1)+ ") " + Choices[i].ReturnDescription());
            }
            int retval = -1;
            do
            {
                Console.WriteLine(prompt);
                int.TryParse(Console.ReadLine(), out retval);
            }
            while (retval < 1 || retval > Choices.Count + 1);
                
            
            return retval;
        }

        public void ResetChoices() //removes old choices
        {
            Choices.Clear();
        }
    }
}
