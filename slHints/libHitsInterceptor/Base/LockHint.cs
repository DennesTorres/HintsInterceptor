using libHitsInterceptor.HintKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHitsInterceptor.Base
{
    public class LockHint : TableHint
    {
        public override bool CheckCompatibility(HintsCollection hints)
        {
            if ((from x in hints.OfType<LockHint>()
                    select x).Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
