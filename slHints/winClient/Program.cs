using libHintsInterceptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using libHintsInterceptor.Hints;

namespace winClient
{
    class Program
    {
        static void Main(string[] args)
        {
            libDados.NORTHEntities ct = new libDados.NORTHEntities();

            var res = (from x in ct.Customers.Include(x => x.Orders)
                       select x);

            var res2 = res.ToList();

            using (var rp = new ReadPast())            
            using (var rr = new RepeatableRead())
            using (var r = new Recompile() )
            {
                var res3 = res.ToList();
            }



            using (var rp = new ReadPast() { TableName = "Orders" })
            using (var rr = new RepeatableRead() { TableName = "Customers" })
            using (var r = new Recompile())
            {
                var res4 = res.ToList();
            }

            using (var rp = new ReadPast())
            using (var rr = new RepeatableRead() { TableName = "Customers" })
            using (var r = new Recompile())
            {
                var res5 = res.ToList();
            }

            using (var ofu = new Optimize_For_Unknown())
            {
                var res6 = res.ToList();
            }
        }
    }
}
