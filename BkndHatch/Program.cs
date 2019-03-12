using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BkndHatch.Data;
using BkndHatch.Tests;

namespace BkndHatch
{
    class Program
    {
        static void Main(string[] args)
        {
             var repository = Ioc.Resolve<IQuestionRepository>();
            //Console.WriteLine( repository.LoadById(100));
            //var items = repository.Load(DateTime.Now.Date.AddDays(-1), DateTime.Now);
            //items.ToList().ForEach(x => Console.WriteLine(x));
            var test = new QuestionRepositoryTest(repository);
            test.Execute();
            Console.ReadLine();
        }


    }
}
