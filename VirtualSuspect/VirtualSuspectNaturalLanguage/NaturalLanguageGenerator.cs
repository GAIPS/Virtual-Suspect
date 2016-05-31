using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;

namespace VirtualSuspectNaturalLanguage
{
    public static class NaturalLanguageGenerator{

        public static string GenerateAnswer(QueryResult result) {

            string answer = "";
            
            //Prefilter the Answer According to the MetaData

            //If its a non-remember or denial (negative answer)
            if(result.MetaData.ContainsKey("negative-answer")) {

                switch(result.MetaData["negative-answer"]) {
                    case "non-remember":
                        answer += "I can't remember that.";
                        break;
                    case "denial":
                        answer += "I don't want to talk about it.";
                        break;
                }

            //If its a irrelevant (special answer)
            }else if(result.MetaData.ContainsKey("special-answer")) {
                
                switch(result.MetaData["special-answer"]) {
                    case "irrelevant":
                        answer += "I don't find that question relevant!";
                        break;
                }
        
            }

            return answer;

        }
    }
}
