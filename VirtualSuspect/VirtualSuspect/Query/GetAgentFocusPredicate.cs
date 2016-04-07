using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class GetAgentFocusPredicate : IFocusPredicate{

        public Func<EventNode, QueryResult.Result> CreateFunction() {
            return delegate (EventNode node) {    

                return new QueryResult.Result(node.Agent.Select(x => x.Value), node.Agent.Count , KnowledgeBase.DimentionsEnum.Agent);

            };
        }
    }
}
