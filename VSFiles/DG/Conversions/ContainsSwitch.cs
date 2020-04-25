using System;
using System.Collections.Generic;

namespace DG
{
   public class ContainsSwitch
   {

      public bool SingleCaseExecution { get; private set; }

      public string Value { get; private set; }
      public Action Action { get; private set; }

      private readonly List<ContainsSwitch> actionList = new List<ContainsSwitch>();


      public string IdentifyDelimiterPresentInString(string stringToSearch, bool returnFirstFound, List<string> delimiterSet)
      {

         //https://stackoverflow.com/questions/7175580/use-string-contains-with-switch
         string matched = "";

         SingleCaseExecution = returnFirstFound;


         foreach (string c in delimiterSet)
         {
            AddCase(c, delegate () { matched = c; });
         }


         Perform(stringToSearch);
         return matched;

      }

  


      private void Perform(string target)
      {
         foreach (ContainsSwitch act in actionList)
         {
            if (target.Contains(act.Value))
            {
               act.Action();
               if (SingleCaseExecution)
                  break;
            }
         }
      }
      private void AddCase(string value, Action act)
      {
         actionList.Add(new ContainsSwitch() { Action = act, Value = value });
      }
   }

}
