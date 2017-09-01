using libHintsInterceptor.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace libHintsInterceptor.HintKinds
{
    public abstract class TableHint : HintBase
    {

        public string TableName
        { get; set; }

        private static readonly Regex TableHRegex =
    new Regex(@"(?<table>AS \[Extent\d+\])",
        RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static string BuildQuery(string Query, string hints)
        {
            return TableHRegex.Replace(Query, string.Format("${{table}} WITH ({0})", hints));
        }


        public static string BuildQuery(string Query, string hints, string TableName)
        {
            string exp = @"(?<all>\[" + TableName + @"\] AS \[Extent\d+\] WITH \()(?<hints>[\w,]*)\)";
            Regex NameTableRegEx = new Regex(exp,
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

            var mat = NameTableRegEx.Matches(Query);

            if (mat.Count == 0)
            {
                string exp2 = @"(?<all>\[" + TableName + @"\] AS \[Extent\d+\]\)";
                Regex TableClean = new Regex(exp2, RegexOptions.Multiline | RegexOptions.IgnoreCase);
                return TableClean.Replace(Query, string.Format("${{all}} WITH ({0})", hints));
            }
            else
            {
                var sHints = mat[0].Groups[2].Value;
                hints = sHints + "," + hints;
                return NameTableRegEx.Replace(Query, string.Format("${{all}} {0})", hints));
            }
        }

    }
}
