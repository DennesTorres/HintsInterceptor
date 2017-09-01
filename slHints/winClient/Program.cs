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

            HintsInterceptor.Add(new ReadPast());
            HintsInterceptor.Add(new RepeatableRead());
            HintsInterceptor.Add(new Recompile());

            var res3 = res.ToList();

            HintsInterceptor.Clear();

            HintsInterceptor.Add(new ReadPast() {TableName = "Orders" } );
            HintsInterceptor.Add(new RepeatableRead() {TableName = "Customers" });
            HintsInterceptor.Add(new Recompile());

            var res4 = res.ToList();

            HintsInterceptor.Clear();

            HintsInterceptor.Add(new ReadPast());
            HintsInterceptor.Add(new RepeatableRead() { TableName = "Customers" });
            HintsInterceptor.Add(new Recompile());

            var res5 = res.ToList();

            HintsInterceptor.Clear();
        }
    }
}
