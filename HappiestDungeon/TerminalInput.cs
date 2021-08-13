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
            Choices.Add(choice);
        }

        public int GetChoice(string prompt)
        {
            if (Choices.Count==0)
            {
                throw new InvalidOperationException("There are no choices available.");
                //if we allowed this case to run do while loop would be infinite
            }
            Console.WriteLine(prompt);
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.WriteLine((i + 1)+ ") " + Choices[i].ReturnDescription());
            }
            int retval = -1;
            do
            {
                Console.WriteLine("Please enter a number of the possibility you have chosen: ");
                int.TryParse(Console.ReadLine(), out retval);
            }
            while (retval < 1 || retval > Choices.Count); //we use 1 based index so choices.count is last choice
                
            
            return retval-1; //user enters 1 - count, we return 0 based
        }

        public void ResetChoices() //removes old choices
        {
            Choices.Clear();
        }
    }
}
