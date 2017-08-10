using libHintsInterceptor.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor
{
    /// <summary>
    /// Collection of hints used by the interceptor
    /// to ensure a valid collection of hints
    /// </summary>
    public class HintsCollection : Collection<HintBase>
    {
        /// <summary>
        /// Method to validate a new hint
        /// against the existing ones
        /// </summary>
        /// <param name="newhint"></param>
        /// <returns></returns>
        private bool ValidateHint(HintBase newhint)
        {
            // Dispara a validação da compatibilidade do novo 
            // hint com os já existentes na coleção     
            return newhint.CheckCompatibility(this);
        }

        /// <summary>
        /// Intercepts the insert to call the validation
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, HintBase item)
        {
            if (ValidateHint(item))
                base.InsertItem(index, item);
            else
                throw new ApplicationException("hint incompatível");
        }


        /// <summary>
        /// Intercepts the SetItem to call the validation
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void SetItem(int index, HintBase item)
        {
            HintBase itemOld = this[index];
            this.RemoveAt(index);
            if (ValidateHint(item))
                base.InsertItem(index, item);
            else
            {
                base.InsertItem(index, itemOld);
                throw new ApplicationException("hint incompatível");
            }
        }

        /// <summary>
        /// Generate a string with all the hints of a kind in the collection
        /// This string will be used in the query
        /// </summary>
        /// <returns></returns>
        public string GenerateString<T>() where T:HintBase 
        {

            var res = (from x in this.OfType<T>()
                       select x.Hint()).ToList();

            var sList = string.Join(",", res);

            return sList;
        }
    }
}
