using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;
using VirtualSuspect.KnowledgeBase;

namespace VirtualSuspect.Handler {

    class ModifyKnowledgeBaseHandler : IPreHandler {

        private VirtualSuspectQuestionAnswer virtualSuspect;

        public ModifyKnowledgeBaseHandler(VirtualSuspectQuestionAnswer virtualSuspect) {

            this.virtualSuspect = virtualSuspect;
        
        }

        public QueryDto Modify(QueryDto query) {

            return query;

        }

        private EventNode DuplicateEventKnownData(EventNode old, uint newID) {

            bool isTimeKnwon = old.IsKnown(old.Time);

            bool isLocationKnwon = old.IsKnown(old.Location);

            EventNode eventCopy = new EventNode(newID, old.Incriminatory, old.Action, isTimeKnwon ? old.Time : null, isLocationKnwon ? old.Location : null);

            return eventCopy;

        }
    
    }
}
