using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor.Base
{
    public abstract class HintBase
    {
        public virtual bool CheckCompatibility(HintsCollection hints)
        {
            return true;
        }


        /// <summary>
        /// Returns the hint as a string
        /// that will be included in the query
        /// </summary>
        /// <returns></returns>
        public virtual string Hint()
        {
            return this.GetType().Name.Replace('_', ' ').ToUpper();
        }

    }
}
