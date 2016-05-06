using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualSuspect.Query;

namespace VirtualSuspect {

    interface IPreHandler {

        QueryDto Modify(QueryDto query);

    }

}
