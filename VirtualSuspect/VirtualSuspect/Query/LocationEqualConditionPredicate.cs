using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class LocationEqualConditionPredicate : IConditionPredicate{

        private string location;

        public LocationEqualConditionPredicate(string location){

            this.location = location;

        }

        public Predicate<EventNode> CreatePredicate() {
            return  
                delegate (EventNode node) {
                    return node.Location.Value == location;
                };
        }
    }
}
