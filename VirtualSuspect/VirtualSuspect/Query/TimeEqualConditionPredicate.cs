using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class TimeEqualConditionPredicate : IConditionPredicate{

        private string time;

        public TimeEqualConditionPredicate(string time){

            //TODO: transform to datetime
            this.time = time;

        }

        public Predicate<EventNode> CreatePredicate() {
            return  
                delegate (EventNode node) {
                    //TODO: perform data transformation
                    return node.Action.Action == time;
                };
        }
    }
}
