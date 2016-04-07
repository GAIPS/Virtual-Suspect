using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect.Query
{
    public class GetThemeFocusPredicate : IFocusPredicate{

        public Func<EventNode, QueryResult.Result> CreateFunction() {
            return delegate (EventNode node) {    

                return new QueryResult.Result(node.Theme.Select(x => x.Value), node.Theme.Count , KnowledgeBase.DimentionsEnum.Theme);

            };
        }
    }
}
