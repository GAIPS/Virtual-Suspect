using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Handler;
using VirtualSuspect.KnowledgeBase;
using VirtualSuspect.Query;

namespace VirtualSuspect {
    class StrategySelectorHandler : IPreHandler {

        private VirtualSuspectQuestionAnswer virtualSuspect;

        private Dictionary<VirtualSuspectQuestionAnswer.LieStrategy, float> strategyComulativeFrequency;

        /// <summary>
        /// Creates a new Handler to select the Lie Strategy used by the virtual suspect according to the strategy distribution
        /// </summary>
        /// <param name="virtualSuspect">The target virtual suspect u</param>
        /// <param name="strategyDistribution">The distribution of the Lie Strategies to be used by the selector. This dictionary should not contain duplicates</param>
        public StrategySelectorHandler(VirtualSuspectQuestionAnswer virtualSuspect, Dictionary<VirtualSuspectQuestionAnswer.LieStrategy, float> strategyDistribution) {

            this.virtualSuspect = virtualSuspect;

            //Normalize Distribution 
            float totalProb = 0;

            foreach(float prob in strategyDistribution.Values) {

                totalProb += prob;

            }

            List<VirtualSuspectQuestionAnswer.LieStrategy> strategies = new List<VirtualSuspectQuestionAnswer.LieStrategy>(strategyDistribution.Keys);

            foreach (VirtualSuspectQuestionAnswer.LieStrategy strategy in strategies) {
            
                strategyDistribution[strategy] /= totalProb;

            }

            for(int i = 1; i< strategies.Count; i++) {

                strategyDistribution[strategies[i]] += strategyDistribution[strategies[i - 1]];

            }

            this.strategyComulativeFrequency = strategyDistribution;

        }
        
        public QueryDto Modify(QueryDto query) {

            //Check if the information related to the events in the query is incriminatory
            List<EventNode> events = virtualSuspect.FilterEvents(query.QueryConditions);
            float maximumIncriminatory = 0;

            foreach(EventNode node in events) {
                maximumIncriminatory = Math.Max(maximumIncriminatory, node.Incriminatory);
            }

            VirtualSuspectQuestionAnswer.LieStrategy selectedStrategy = VirtualSuspectQuestionAnswer.LieStrategy.None;

            if (maximumIncriminatory == 0) { //If the information is not incriminatory no need to lie

                selectedStrategy = VirtualSuspectQuestionAnswer.LieStrategy.None;

            } else { //If the information is incriminatory select best approach

                // Evaluate the best strategy considering the ToM and Incriminatory of the relevant events
                selectedStrategy = EvaluateStrategy(events);

            }

            virtualSuspect.Strategy = selectedStrategy;

            return query;
        
        }

        //TODO: Improve this Heuristic
        private VirtualSuspectQuestionAnswer.LieStrategy EvaluateStrategy(List<EventNode> events) {

            Random randomizer = new Random(DateTime.Now.Millisecond * DateTime.Now.Hour);

            Double randomDouble = randomizer.NextDouble();

            foreach (KeyValuePair<VirtualSuspectQuestionAnswer.LieStrategy, float> strategyPair in strategyComulativeFrequency) {

                if (strategyPair.Value >= randomDouble) {
                    return strategyPair.Key;
                }

            }

            return VirtualSuspectQuestionAnswer.LieStrategy.None;

        }
    
    }
}
