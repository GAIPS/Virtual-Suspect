using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;

namespace VirtualSuspectNaturalLanguage.Component {
    public static class ReasonNaturalLanguageGenerator {

        public static string Generate(QueryResult result, Dictionary<KnowledgeBaseManager.DimentionsEnum, List<QueryResult.Result>> resultsByDimension) {

            string answer = " ";

            Dictionary<EntityNode, int> mergedLocations = MergeAndSumReasonsCardinality(resultsByDimension[KnowledgeBaseManager.DimentionsEnum.Reason]);

            answer += CombineValues("and", mergedLocations.Select(x=>x.Key.Speech));
                
            return answer;
        }

        #region Utility Methods

        private static string CombineValues(string term, IEnumerable<string> values) {

            string combinedValues = "";

            for (int i = 0; i < values.Count(); i++) {

                combinedValues += values.ElementAt(i);

                if (i == values.Count() - 2) {
                    combinedValues += " " + term + " ";
                }
                else if (i < values.Count() - 1) {
                    combinedValues += ", ";
                }
            }

            return combinedValues;

        }

        private static Dictionary<EntityNode, int> MergeAndSumReasonsCardinality(List<QueryResult.Result> reasons) {

            Dictionary<EntityNode, int> reasonsWithCardinality = new Dictionary<EntityNode, int>();

            foreach (QueryResult.Result reasonResult in reasons) {

                foreach (EntityNode locationNode in reasonResult.values) {

                    if (!reasonsWithCardinality.ContainsKey(locationNode)) {

                        reasonsWithCardinality.Add(locationNode, reasonResult.cardinality);

                    }
                    else {

                        reasonsWithCardinality[locationNode] += reasonResult.cardinality;
                    }
                }
            }

            return reasonsWithCardinality;
        }
        #endregion   
    }
}
