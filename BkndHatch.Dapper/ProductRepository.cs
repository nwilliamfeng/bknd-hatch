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


        public IEnumerable<Product> Find(string productName, int pageIdx,int pageSize,ref int totalCount)
        {
            using (var conn = CreateConnection())
            {
                var sql1 = $"select * from {PRODUCT_TABLE} where name like @a limit {pageSize} offset {pageSize * (pageIdx > 0 ? pageIdx - 1 : 0)}";
                var sql2 = $"select count(*) from {PRODUCT_TABLE} where name like @a ";
                var sql = string.Join(";", sql1, sql2);

                var reader = conn.QueryMultiple(sql, new { a = productName + "%" });
                var result =reader.Read<Product>();
                  totalCount = reader.Read<int>().FirstOrDefault();
                reader.Dispose();

                return result;
          
            };
        }

         public IEnumerable<Product> Find(DateTime start ,DateTime end, int pageIdx,int pageSize,ref int totalCount)
        {
            using (var conn = CreateConnection())
            {
                var sql1 = $"select * from {PRODUCT_TABLE} where createdate >= @a and createdate<=@b limit {pageSize} offset {pageSize * (pageIdx > 0 ? pageIdx - 1 : 0)}";
                var sql2 = $"select count(*) from {PRODUCT_TABLE} where createdate >= @a and createdate<=@b";
                var sql = string.Join(";", sql1, sql2);

                var reader = conn.QueryMultiple(sql, new { a = start.ToString("yyyy-MM-dd HH:mm:ss") , b = end.ToString("yyyy-MM-dd HH:mm:ss") });
                var result =reader.Read<Product>();
                  totalCount = reader.Read<int>().FirstOrDefault();
                reader.Dispose();

                return result;
          
            };
        }


        public IEnumerable<Product> Find(string productName)
        {
            using (var conn = CreateConnection())
            {
               return  conn.Query<Product>($"select * from {PRODUCT_TABLE} where name like @a limit 5",new { a=productName+"%"}) ;

            };
        }

        private ProductRepository()
        {
            
        }
    }
}
