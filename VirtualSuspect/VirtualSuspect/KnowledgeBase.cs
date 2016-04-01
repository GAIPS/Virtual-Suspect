using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect
{
    public class KnowledgeBase : IKnowledgeBase
    {
        public EntityNode CreateNewEntity(EntityDto en) {

            //Test if the Dto is valid

            //Create the Dto according to the type
            EntityNode = new EntityNode();
        }

        public EventNode CreateNewEvent(EventDto ev) {
            throw new NotImplementedException();
        }
    }
}
