using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BkndHatch.Data;

namespace BkndHatch
{
    public static class ProductRepositoryTest
    {

        static ProductRepositoryTest()
        {
            var db = ProductRepository.Instance;
            db.ClearTable();
            for (var i = 0; i < 34; i++)
            {
                db.AddProduct(new Product { CreateDate = DateTime.Now.Date.AddDays(-i), Name = "prod" + i.ToString() });
            }
        }

        

        public static void QueryProductName()
        {
            var db = ProductRepository.Instance;
            while (true)
            {
                Console.WriteLine("please input productname:");
                var name = Console.ReadLine();

                foreach (var prod in db.Find(name))
                    Console.WriteLine("find: "+prod.Name);
            }
            
        }

        


        public static void QueryProductByPage()
        {

            var db = ProductRepository.Instance;
            while (true)
            {
                Console.WriteLine("please input pageIndex:");
                var idx =int.Parse( Console.ReadLine());
                int total = 0;
                var lst = db.Find("pro", idx, 10, ref total);
              
                foreach (var prod in lst)
                    Console.WriteLine("find: " + prod.Name);
            }
        }

        public static void QueryProductByDate()
        {

            var db = ProductRepository.Instance;
            while (true)
            {
                Console.WriteLine("please input day:");
                var idx = int.Parse(Console.ReadLine());
                var start = DateTime.Now.Date.AddDays(-idx);
                var end = DateTime.Now.Date ;
                int total = 0;
                var lst = db.Find(start,end, 1, 100, ref total);
                Console.WriteLine("total:" + total);
                foreach (var prod in lst)
                    Console.WriteLine("find: " + prod.Name);
            }
        }

    }
}
