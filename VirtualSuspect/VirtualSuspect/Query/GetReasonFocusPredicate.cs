using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class GetReasonFocusPredicate : IFocusPredicate{

        public Func<EventNode, QueryResult.Result> CreateFunction() {
            return delegate (EventNode node) {    

                return new QueryResult.Result(node.Reason.Select(x => x.Value), node.Reason.Count , KnowledgeBase.DimentionsEnum.Reason);

            };
        }
    }
}
