using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkndHatch.Tests
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"name:{FirstName}{LastName},birthday:{BirthDate}";
        }
    }
}
