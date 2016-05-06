using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;

namespace VirtualSuspect {

    internal class LieModule : IUpdateHandler {

        internal enum LieStrategy{None, Hide, RandomEventReplacement, SmartEventReplacement};

        internal KnowledgeBaseModifier modifier;

        internal LieModule(LieStrategy strategy) {
        

        }

        public void Update(KnowledgeBase kb, QueryDto query) {

            List<EventNode> queryEvents = kb.Events;

            //Get all the events that match the query conditions
            foreach (IConditionPredicate condition in query.QueryConditions) {

                queryEvents = queryEvents.FindAll(condition.CreatePredicate());

            }

            //Lie Process Decision Making 
            
            //For each event
            foreach(EventNode eventNode in queryEvents) {

                //Is the event incriminatory
                int incriminatory = eventNode.Incriminatory;


            }


        }

        private EventNode DuplicateEventKnownData(EventNode old, uint newID) {

            bool isTimeKnwon = old.IsKnown(old.Time);

            bool isLocationKnwon = old.IsKnown(old.Location);

            EventNode eventCopy = new EventNode(newID, old.Incriminatory, old.Action, isTimeKnwon ? old.Time : null, isLocationKnwon ? old.Location : null);



            return eventCopy;

        }

    }

}
