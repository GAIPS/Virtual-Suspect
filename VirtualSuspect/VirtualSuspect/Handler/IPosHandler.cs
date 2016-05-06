using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;

namespace VirtualSuspect.Handlers {

    interface IPosHandler {

        QueryResult Modify(QueryResult result);

    }

}
