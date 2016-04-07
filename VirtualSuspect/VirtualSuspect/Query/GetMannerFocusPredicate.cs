using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class GetMannerFocusPredicate : IFocusPredicate{

        public Func<EventNode, QueryResult.Result> CreateFunction() {
            return delegate (EventNode node) {    

                return new QueryResult.Result(node.Manner.Select(x => x.Value), node.Manner.Count , KnowledgeBase.DimentionsEnum.Manner);

            };
        }
    }
}
