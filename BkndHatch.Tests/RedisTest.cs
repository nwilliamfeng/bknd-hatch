using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Linq.Expressions;
using BkndHatch.Data.Redis;

namespace BkndHatch.Tests
{
    public class RedisTest
    {
        private ConnectionMultiplexer connection;
        private static RedisTest instance; 


        private RedisTest()
        {
            connection = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            
        }

        private IDatabase Database
        {
            get
            {
                return connection.GetDatabase(0);
            }
        }

        public static RedisTest Instance
        {
            get
            {
                if (instance == null)
                    instance = new RedisTest();
                return instance;
            }
        }

        public void Clear()
        {
            connection.GetServer(connection.Configuration).Keys(0).ToList().ForEach(x => this.Database.KeyDelete(x));
        }

        public void StringTest()
        {
            var key = "string_test_1";
            Console.WriteLine("1.please enter string key value:");
            var str =Console.ReadLine();
            this.Database.StringSet(key,str);
            Console.WriteLine("after set: "+this.Database.StringGet(key));
            Console.WriteLine("2.please enter string increment key value:");
            var increment =long.Parse( Console.ReadLine());
            this.Database.StringIncrement(key, increment);
            Console.WriteLine("after set increment: " + this.Database.StringGet(key));
            Console.WriteLine("3.please enter string decrement key value:");
            var decrement =long.Parse( Console.ReadLine());
            this.Database.StringDecrement(key,decrement);
            Console.WriteLine("after set decrement: " + this.Database.StringGet(key));
      
            Console.ReadLine();
        }

        public void String_JSON_Test()
        {
            var key = "string_test_json";
            var user = new User { FirstName = "william", LastName = "feng", BirthDate = new DateTime(1980, 2, 10) };
            this.Database.StringSet(key, JsonConvert.SerializeObject(user));
            var str = this.Database.StringGet(key);
            Console.WriteLine($"{key}:{JsonConvert.DeserializeObject<User>(str)}");
        }

        public void Hash_User_Test()
        {

            var user = new User { Id = 1004, FirstName = "william", LastName = "feng", BirthDate = new DateTime(1980, 2, 10) };
            var id = $"hash_user_{user.Id}";
            this.Database.HashSetWithEntity(user, id);
            this.Database.HashGetAll(id).ToList().ForEach(x => Console.WriteLine($"{x.Name}:{x.Value}"));

        }

        public void List_UserIdQueue_Test()
        {
            var key = "List_Key_Push";
            Console.WriteLine("input keys span with spacewhite key:");
            var strs = Console.ReadLine();
            var ids= strs.Split(' ').Select(x=>(RedisValue)x).ToArray() ;
            this.Database.ListLeftPush(key, ids);
            this.Database.ListTrim(key,0,5);
            
        }
        
    }
}
