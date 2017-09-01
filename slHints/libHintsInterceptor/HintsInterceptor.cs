using libHintsInterceptor.Base;
using libHintsInterceptor.HintKinds;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHintsInterceptor
{
    public class HintsInterceptor : DbCommandInterceptor
    {
        [ThreadStatic]
        private static HintsCollection _hints = new HintsCollection();

        public static HintsCollection Hints
        {
            get
            {
                return _hints;
            }
        }

        public override void ScalarExecuting(DbCommand command,
                        DbCommandInterceptionContext<object> interceptionContext)
        {
            command.CommandText=BuildQuery(command.CommandText);
        }


        public override void ReaderExecuting(DbCommand command, 
            DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            command.CommandText = BuildQuery(command.CommandText);
        }


        private string BuildQuery(string Query)
        {
            if (Hints.Count != 0)
            {
                string sQHints = "";

                sQHints = Hints.GenerateString<QueryHint>();

                var tHints = GetTableHints();              

                if (sQHints != string.Empty)
                {
                    Query = QueryHint.BuildQuery(Query, sQHints);
                }

                if (tHints.Count >0)
                {
                    Query = BuildTableQuery(tHints, Query);
                }

            }

            return Query;
        }

        #region Process TableHints

        private Dictionary<string,string> GetTableHints()
        {
            var res = (from x in Hints.OfType<TableHint>()
                       group x by x.TableName into g
                       select new { TableName = g.Key, Hints = g.ToList() });

            Dictionary<string, string> tHints = new Dictionary<string, string>();

            res.ToList().ForEach(x =>
            {
                string hints = "";
                x.Hints.ForEach(y => hints += (hints == string.Empty ? "" : ", ") + y.Hint());
                var key = string.IsNullOrEmpty(x.TableName) ? "Empty" : x.TableName;
                tHints.Add(key, hints);
            });

            return tHints;
        }

        private string BuildTableQuery(Dictionary<string, string> tHints,string Query)
        {
            if (tHints.ContainsKey("Empty"))
            {
                Query=TableHint.BuildQuery(Query, tHints["Empty"]);
            }

            foreach (var item in tHints)
            {
                if (item.Key != "Empty")
                {
                    Query=TableHint.BuildQuery(Query, item.Value, item.Key);
                }
            }

            return Query;
        }

        #endregion

        #region Collection
        public static void Add(HintBase hint)
        {
            Hints.Add(hint);
        }

        public static void Clear()
        {
            Hints.Clear();
        }

        #endregion

    }
}
