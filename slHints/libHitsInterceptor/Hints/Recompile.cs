using libHitsInterceptor.Base;
using libHitsInterceptor.HintKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHitsInterceptor.Hints
{
    public class Recompile : QueryHint
    {
        public override bool CheckCompatibility(HintsCollection hints)
        {
            return true;
        }
    }
}
