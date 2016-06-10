using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;
using VirtualSuspectNaturalLanguage.Component;

namespace VirtualSuspectNaturalLanguage
{
    public static class NaturalLanguageGenerator{

        public static string GenerateAnswer(QueryResult result) {

            string answer = "";
            
            if (result.YesNoResult) { //Yes or no Question

            
            } else { //Get Information Question (We assume that the answer only has 1 type of dimension of answer)

                //==============================
                //Create the answer introduction

                //Consider Number of Agents associated with the events
                int numberAgents = GetNumberAgents(result.Query);

                answer += (numberAgents == 1) ? "I am " : "we were ";


                //==============================
                //Get all the answers by dimension
                Dictionary<KnowledgeBaseManager.DimentionsEnum, List<QueryResult.Result>> resultsByDimension = new Dictionary<KnowledgeBaseManager.DimentionsEnum, List<QueryResult.Result>>();
                foreach(QueryResult.Result queryResult in result.Results) {

                    if (!resultsByDimension.ContainsKey(queryResult.dimension)) {
                        resultsByDimension[queryResult.dimension] = new List<QueryResult.Result>();
                    }
                    resultsByDimension[queryResult.dimension].Add(queryResult);
                }

                //==============================
                //Is there any entity with dimension Time
                if (resultsByDimension.ContainsKey(KnowledgeBaseManager.DimentionsEnum.Time)) {

                    //Ignore Cardinality(the same time period only appears once)

                    //Get all the TimeDate and Parse them
                    List<KeyValuePair<DateTime, DateTime>> dateTimeList = new List<KeyValuePair<DateTime, DateTime>>();

                    foreach (QueryResult.Result value in resultsByDimension[KnowledgeBaseManager.DimentionsEnum.Time]) {

                        DateTime firstDate = DateTime.ParseExact(value.values.ElementAt(0).Value.Split('>')[0], "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);
                        DateTime secondDate = DateTime.ParseExact(value.values.ElementAt(0).Value.Split('>')[1], "dd/MM/yyyyTHH:mm:ss", CultureInfo.InvariantCulture);

                        dateTimeList.Add(new KeyValuePair<DateTime, DateTime>(firstDate, secondDate));

                    }

                    //Merge sequence
                    dateTimeList = SortAndMergeSequenceDateTime(dateTimeList);

                    //Group by Day
                    Dictionary<DateTime, List<KeyValuePair<DateTime,DateTime>>> dateTimeGroupByDay = GroupDateTimeByDay(dateTimeList);

                    answer += TimeNaturalLanguageGenerator.Generate(dateTimeGroupByDay);
                    
                }else if(resultsByDimension.ContainsKey(KnowledgeBaseManager.DimentionsEnum.Location)) {

                    //Group by entities type

                    //Merge all entities and sum cardinality
                    List<EntityNode> mergedLocations = MergeAndSumLocationsCardinality(resultsByDimension[KnowledgeBaseManager.DimentionsEnum.Location]);
                    //Group by Type
                    Dictionary<string, List<EntityNode>> locationGroupByType = GroupLocationByType(mergedLocations);

                    answer += LocationNaturalLanguageGenerator.Generate(locationGroupByType);                  
                }
            }

            //Capitalize the answer if needed
            answer = UppercaseFirst(answer);

            return answer;

        }

        private static string UppercaseFirst(string s) {
            // Check for empty string.
            if (string.IsNullOrEmpty(s)) {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private static bool NewSentencePossible(string sentence) {

            return sentence == "" || sentence.TrimEnd().Last() == '.';

        }
    
        private static string NaturalLanguageGetFrequency(int number) {

            string frequencyWord = "";

            if(number == 0) {
                frequencyWord = "never";
            }
            else if (number == 1) {
                frequencyWord = "once";
            }
            else if (number == 2) {
                frequencyWord = "twice";
            }
            else if (number >= 3 && number <= 6) {
                frequencyWord = number + " times";
            }
            else {
                Random rng = new Random();
                int randomNumber = rng.Next(2);
                if (randomNumber == 0)
                    frequencyWord = "many";
                else if (randomNumber == 1)
                    frequencyWord = "several";
            }

            return frequencyWord;
        }
        
        private static List<KeyValuePair<DateTime, DateTime>> SortAndMergeSequenceDateTime(List<KeyValuePair<DateTime, DateTime>> sequence) {

            List<KeyValuePair<DateTime,DateTime>> sequenceMerged = new List<KeyValuePair<DateTime, DateTime>>();

            sequence.Sort((a, b) => a.Key.CompareTo(b.Value));
            
            for(int i = 0; i < sequence.Count; i++) {

                if(i == 0) {
                    sequenceMerged.Add(sequence.ElementAt(0));
                }else {

                    if(sequenceMerged.Last().Value == sequence[i].Key) { //the end of the last is equal to the begin of current Date
                        DateTime beginInterval = sequenceMerged.Last().Key;
                        sequenceMerged.RemoveAt(sequenceMerged.Count - 1);
                        sequenceMerged.Add(new KeyValuePair<DateTime, DateTime>(beginInterval, sequence[i].Value));
                    }else{
                        sequenceMerged.Add(sequence.ElementAt(i));
                    }

                }
                    
            }
                
            return sequenceMerged;    

        }

        private static Dictionary<DateTime, List<KeyValuePair<DateTime,DateTime>>> GroupDateTimeByDay(List<KeyValuePair<DateTime, DateTime>> dateTimeList) {
        
            dateTimeList.Sort((a, b) => a.Key.CompareTo(b.Value));

            IEnumerable<IGrouping<DateTime, KeyValuePair<DateTime, DateTime>>> groupedResult = dateTimeList.GroupBy(x => new DateTime(x.Key.Year, x.Key.Month, x.Key.Day), x => x);

            return groupedResult.ToDictionary(x=>x.Key, x=>x.ToList());
        }

        private static List<EntityNode> MergeAndSumLocationsCardinality(List<QueryResult.Result> locations) {

            throw new NotImplementedException();
            return new List<EntityNode>();
        }

        private static Dictionary<string, List<EntityNode>> GroupLocationByType(List<EntityNode> locations) {

            throw new NotImplementedException();
            return new Dictionary<string, List<EntityNode>>();
        }
        private static int GetNumberAgents(QueryDto query) {

            return query.QueryConditions.Count(x => x.GetSemanticRole() == KnowledgeBaseManager.DimentionsEnum.Agent);   
        }
    }   
}
