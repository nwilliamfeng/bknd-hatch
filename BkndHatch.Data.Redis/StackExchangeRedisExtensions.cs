using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using StackExchange.Redis;

namespace BkndHatch.Data.Redis
{
    public static class StackExchangeRedisExtensions
    {
        public static void HashSetWithEntity(this IDatabase database,object entity,string id,params string[] propertyNames)
        {
            if (entity == null || string.IsNullOrEmpty(id))
                return ;
            var hes = entity.GetType().GetProperties()
                .Where(x => propertyNames.Length==0 ? true : propertyNames.Contains(x.Name))
                .Select(x => new HashEntry(x.Name,x.GetValue(entity,null)?.ToString()))
                .ToArray();
            database.HashSet(id, hes);      
        }
  

       

    }
}
