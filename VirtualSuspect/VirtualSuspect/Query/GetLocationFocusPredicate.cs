using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class GetLocationFocusPredicate : IFocusPredicate{

        public Func<EventNode, QueryResult.Result> CreateFunction() {
            return delegate (EventNode node) {    

                return new QueryResult.Result(node.Location.Value, 1 , KnowledgeBase.DimentionsEnum.Location);

            };
        }

        public string GetSemanticRole() {
            return "Location";
        }
    }
}
