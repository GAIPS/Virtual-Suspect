using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSuspectUI {

    interface ConditionBox {

        string Dimension { get; }

        string Value { get; }

        bool Known { get; }

        bool Focus { get; }

    }
}
