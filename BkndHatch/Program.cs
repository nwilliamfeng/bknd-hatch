using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BkndHatch.Data;
using BkndHatch.Tests;
using MongoDB.Driver;

namespace BkndHatch
{
    class Program
    {
        static void Main(string[] args)
        {
            new RegexTest().MatchIp();
           // var database = new MongoClient("mongodb://zhangting:zhangting@10.205.248.113:22001,10.205.109.154:22001/QATrain?replicaSet=shard0;minPoolSize=10;maxPoolSize=100") ;
           // database.ListDatabaseNames().ToList().ForEach(x=>Console.WriteLine("DataBaseName:"+x));
           
            // var repository = Ioc.Resolve<IQuestionRepository>();
            //Console.WriteLine( repository.LoadById(100));
            //var items = repository.Load(DateTime.Now.Date.AddDays(-1), DateTime.Now);
            //items.ToList().ForEach(x => Console.WriteLine(x));
           // var test = new QuestionRepositoryTest(repository);
          //  test.Execute();
            Console.ReadLine();
        }


    }
}
