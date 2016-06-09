using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;
using VirtualSuspectNaturalLanguage.Component;

namespace VirtualSuspectNaturalLanguage
{
    public static class NaturalLanguageGenerator{

        public static string GenerateAnswer(QueryResult result) {

            string answer = "";
            
            if (result.YesNoResult) { //Yes or no Question



            } else { //Get Information Question

                //Create the answer introduction

                //Write the query result
                foreach (QueryResult.Result queryResult in result.Results) {
                    
                    switch(queryResult.dimension) {

                        case VirtualSuspect.KnowledgeBase.KnowledgeBaseManager.DimentionsEnum.Time:

                            //Add Preposition
                            answer += NewSentencePossible(answer) ? " On " : "on ";

                            List<string> values = new List<string>();
                            
                            foreach(string value in queryResult.values.Select(x => x.Value)) {

                                INaturalLanguageGenerationComponent component = new NaturalLanguageTimeComponent(value);

                                values.Add(component.GenerateNaturalLanguage());
                            }

                            answer += CombineValues("and", values);

                            answer += ".";

                            break;

                        case VirtualSuspect.KnowledgeBase.KnowledgeBaseManager.DimentionsEnum.Location:

                            //Add Preposition
                            break;

                    } 
                }
            }

            return answer;

        }
      
        private static bool NewSentencePossible(string sentence) {

            return sentence == "" || sentence.Last() == '.';

        }
        
        private static string CombineValues(string term, List<string> values) {

            string combinedValues = "";

            for(int i = 0; i < values.Count(); i++) {

                combinedValues += values[i];

                if(i == values.Count() - 2) {
                    combinedValues += " " + term + " ";
                }else if(i < values.Count() - 1){
                    combinedValues += ", ";   
                }
            }

            return combinedValues;

        }
    }
}
