using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BkndHatch.Data;
  

namespace BkndHatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = Ioc.Resolve<IQuestionRepository>();
            Console.WriteLine( repository.LoadById(100));
         
        }
    }
}
