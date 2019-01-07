using libHintsInterceptor.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor.HintKinds
{
    public abstract class QueryHint : HintBase
    {
        public static string BuildQuery(string Query, string hints)
        {
            return Query + " OPTION (" + hints + ")";
        }
    }
}
