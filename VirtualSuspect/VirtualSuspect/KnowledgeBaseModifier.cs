using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspect {

    interface KnowledgeBaseModifier {

        void ModifyKnowledgeBase(KnowledgeBase kb, EventNode ev);

    }

}
