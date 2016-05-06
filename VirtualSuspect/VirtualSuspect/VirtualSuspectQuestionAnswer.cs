using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;

namespace VirtualSuspect {

    public class VirtualSuspectQuestionAnswer : IQuestionAnswerSystem {

        private KnowledgeBase knowledgeBase;

        public KnowledgeBase KnowledgeBase {

            get {
                    return knowledgeBase;
            }

        }

        private List<IPreHandler> preHandlers;

        private List<IPosHandler> posHandlers;

        public VirtualSuspectQuestionAnswer(KnowledgeBase kb) {

            knowledgeBase = kb;

            //Setup Handlers
            preHandlers = new List<IPreHandler>();
            posHandlers = new List<IPosHandler>();

            //Setup Theory of Mind to Handle the query received
            preHandlers.Add(new ReceiverTheoryofMindHandler(this));

            //Setup Theory of Mind to Handle the results of the query
            //   We assume that the answer is known by the user
            posHandlers.Add(new SenderTheoryOfMindHandler(this, true));

        }

        public QueryResult Query(QueryDto query) {

            //Run Pre Handler
            foreach(IPreHandler handler in preHandlers) {

                handler.Modify(query);
            }

            //Perform Query
            QueryResult result = new QueryResult(query);

            if (query.QueryType == QueryDto.QueryTypeEnum.YesOrNo) { //Test yes or no

                List<EventNode> queryEvents = knowledgeBase.Story;
                //Select entities from the dimension
                foreach (IConditionPredicate predicate in query.QueryConditions) {

                    queryEvents = queryEvents.FindAll(predicate.CreatePredicate());
                }

                result.AddBooleanResult(queryEvents.Count != 0);

            }else if (query.QueryType == QueryDto.QueryTypeEnum.GetInformation) { //Test get information

                List<EventNode> queryEvents = knowledgeBase.Story;

                //Iterate all the conditions (Disjuctive filtering)
                foreach (IConditionPredicate predicate in query.QueryConditions) {

                    queryEvents = queryEvents.FindAll(predicate.CreatePredicate());
                }

                //Select entities from the dimension
                foreach (IFocusPredicate focus in query.QueryFocus) {

                    result.AddResults(queryEvents.Select(focus.CreateFunction()));

                }

                //Count Cardinality
                result.CountResult();

            }

            //Run Pos Handler
            foreach (IPosHandler handler in posHandlers) {

                handler.Modify(result);

            }

            return result;

        }
    }
}
