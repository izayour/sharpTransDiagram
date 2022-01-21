using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpTransDiagram.Models
{
    public enum SoState
    {
        Open,
        Fulfilled,
        PartialFulfilled,
        Shipped,
        PartialShipped,
        Voided
    }
}