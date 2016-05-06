using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Handler;
using VirtualSuspect.Query;

namespace VirtualSuspect {
    class StrategySelectorHandle : IPreHandler {

        private VirtualSuspectQuestionAnswer virtualSuspect;

        public StrategySelectorHandle(VirtualSuspectQuestionAnswer virtualSuspect) {

            this.virtualSuspect = virtualSuspect;
        }
        
        public QueryDto Modify(QueryDto query) {

            //Select Best Strategy
            switch(virtualSuspect.Strategy) {


            }
            
            return query;
        
        }
    
    }
}
