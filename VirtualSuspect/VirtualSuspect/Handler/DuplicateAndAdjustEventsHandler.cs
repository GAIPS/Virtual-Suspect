using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;
using VirtualSuspect.KnowledgeBase;

namespace VirtualSuspect.Handler {

    class DuplicateAndAdjustEventsHandler : IPosHandler {

        private VirtualSuspectQuestionAnswer virtualSuspect;

        public DuplicateAndAdjustEventsHandler(VirtualSuspectQuestionAnswer virtualSuspect) {

            this.virtualSuspect = virtualSuspect;
        
        }
        
        public QueryResult Modify(QueryResult result) {

            
            List<EventNode> queryEvents = virtualSuspect.KnowledgeBase.Events;

            //Get all the events that match the query conditions
            foreach (IConditionPredicate condition in result.Query.QueryConditions) {

                queryEvents = queryEvents.FindAll(condition.CreatePredicate());

            }

            //Lie Process Decision Making 

            //For each event
            foreach (EventNode eventNode in queryEvents) {

                //Is the event incriminatory
                int incriminatory = eventNode.Incriminatory;


            }

            return result;

        }

        private EventNode DuplicateEventKnownData(EventNode old, uint newID) {

            bool isTimeKnwon = old.IsKnown(old.Time);

            bool isLocationKnwon = old.IsKnown(old.Location);

            EventNode eventCopy = new EventNode(newID, old.Incriminatory, old.Action, isTimeKnwon ? old.Time : null, isLocationKnwon ? old.Location : null);

            return eventCopy;

        }
    
    }
}
