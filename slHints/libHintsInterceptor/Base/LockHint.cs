﻿using libHintsInterceptor.HintKinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor.Base
{
    public class LockHint : TableHint
    {
        public override bool CheckCompatibility(HintsCollection hints)
        {
            return !(from x in hints.OfType<LockHint>()
                     select x).Any();
        }
    }
}
