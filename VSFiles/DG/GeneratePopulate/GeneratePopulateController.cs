using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DG
{
   class GeneratePopulateController
   {
      //Is this stored twice?
      public DataTable DTable { get; set; }

      //This is really a Controller Class.
      public GeneratePopulateController(ObtainDataDefinitions obtainDataDefinitions)
      {

         //This still might prove to be more useful as an Object, but will that just be storing the same information yet again?

         //One dimension per Column. All of same 'Length'.
         List<dynamic>[] preLoadedFieldData = new List<dynamic>[obtainDataDefinitions.TableDefinition.ColumnDefinitions.Count()];
         
         //This pre - loads appropriate data based on "LoadDataMimicMethod":{FILE, INCREMENTAL}
         GeneratePreLoadData pld = new GeneratePreLoadData(obtainDataDefinitions, preLoadedFieldData);
  
         //This populates records based on {FILE, INCREMENTAL, RANDOM} in conjunction with 'StartWith' and 'EndWith'
         PopulateData populateData = new PopulateData(obtainDataDefinitions, preLoadedFieldData);

         DTable = populateData.DTable;


      }

   }
}
