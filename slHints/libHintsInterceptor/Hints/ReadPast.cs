using libHintsInterceptor.Base;
using libHintsInterceptor.HintKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor.Hints
{
    public class ReadPast : TableHint
    {
        public override bool CheckCompatibility(HintsCollection hints)
        {
            return true;
        }
    }
}
