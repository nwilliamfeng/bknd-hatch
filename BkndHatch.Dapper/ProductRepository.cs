using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;
using System.Configuration;

namespace BkndHatch.Data
{
    public sealed class ProductRepository :RepositoryBase
    {
        
      
        public const string PRODUCT_TABLE = "Products";


        public override string TableName => PRODUCT_TABLE;


        private static ProductRepository instance;



        public static ProductRepository Instance
        {
            get
            {
                if(instance==null)
                    instance=new ProductRepository();
                return instance;
            }
        }

 

        public void AddProduct(params Product[]  products)
        {
            using (var conn = CreateConnection())
            {
                conn.Execute($"insert into {PRODUCT_TABLE}(Name,CreateDate)  values(  @b,@c) ",products.Select(x=>
                {
                  return  new  {  b= x.Name,c=x.CreateDate };
                }));

            };
        }





        public IEnumerable<Product> Find(string productName)
        {
            using (var conn = CreateConnection())
            {
                conn.Query<Product>($"select top 1 from {PRODUCT_TABLE} where name like ")

            };
        }

        private ProductRepository()
        {
            
        }
    }
}
