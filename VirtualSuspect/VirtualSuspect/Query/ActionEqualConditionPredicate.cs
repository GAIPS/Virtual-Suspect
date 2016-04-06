using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class ActionEqualConditionPredicate : IConditionPredicate{

        private string action;

        public ActionEqualConditionPredicate(string action){

            this.action = action;

        }

        public Predicate<EventNode> CreatePredicate() {
            return  
                delegate (EventNode node) {
                    return node.Action.Action == action;
                };
        }
    }
}
