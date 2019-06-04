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
            var str = Console.ReadLine();
            this.Database.StringSet(key, str);
            Console.WriteLine("after set: " + this.Database.StringGet(key));
            Console.WriteLine("2.please enter string increment key value:");
            var increment = long.Parse(Console.ReadLine());
            this.Database.StringIncrement(key, increment);
            Console.WriteLine("after set increment: " + this.Database.StringGet(key));
            Console.WriteLine("3.please enter string decrement key value:");
            var decrement = long.Parse(Console.ReadLine());
            this.Database.StringDecrement(key, decrement);
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
            var key = "List_Left_Push";
            Console.WriteLine("input keys span with spacewhite key:");
            this.Database.KeyDelete(key);
            var strs = Console.ReadLine();
            var ids = strs.Split(' ').Select(x => (RedisValue)x).ToArray();
            var count = this.Database.ListLeftPush(key, ids);
            Console.WriteLine("after left push the idx is: " + count);
            var currIds = this.Database.ListRange(key, 0).Select(x => x.ToString()).ToArray();
            Console.WriteLine("execute range: " + string.Join(",", currIds.Select(x => x.ToString())));

            //  Console.WriteLine("the idx 1's value is: "+ this.Database.ListGetByIndex(key, 1));
            //  this.Database.ListSetByIndex(key,1,10002);
            Console.WriteLine("after set,the idx 1's value is: " + this.Database.ListGetByIndex(key, 1));
            Console.WriteLine("after left pop,the value is: " + this.Database.ListLeftPop(key));
            Console.WriteLine("execute range: " + string.Join(",", this.Database.ListRange(key, 0).Select(x => x.ToString())));
            //this.Database.ListTrim(key,0,5);

        }

        public void List_Trim_Test()
        {
            var key = "List_Trim_Push";
            this.Database.KeyDelete(key);
            while (true)
            {
                Console.WriteLine("input keys span with spacewhite key:");

                var strs = Console.ReadLine();
                var ids = strs.Split(' ').Select(x => (RedisValue)x).ToArray();
                var count = this.Database.ListLeftPush(key, ids);
                Console.WriteLine("execute range: " + string.Join(",", this.Database.ListRange(key, 0).Select(x => x.ToString())));
                this.Database.ListTrim(key, 0, 3);
                Console.WriteLine("execute trim: " + string.Join(",", this.Database.ListRange(key, 0).Select(x => x.ToString())));
            }


        }



        public void Set_Add()
        {
            var key = "Set_Add";
        
            this.Database.KeyDelete(key);
            while (true)
            {
                Console.WriteLine("input keys span with spacewhite key:");
                var strs = Console.ReadLine();
                var ids = strs.Split(' ').Select(x => (RedisValue)x).ToArray();
                var count = this.Database.SetAdd(key, ids);
                Console.WriteLine("execute range: " + string.Join(",", this.Database.SetMembers(key).Select(x => x.ToString())));
                Console.WriteLine("enter key which need to remove...");
                var deleteKeys = Console.ReadLine().Split(' ').Select(x => (RedisValue)x).ToArray();
                var idx =this.Database.SetRemove(key, deleteKeys);
                Console.WriteLine("after delete,execute range: " + string.Join(",", this.Database.SetMembers(key).Select(x => x.ToString()))+" ->idx:"+idx);
            }

        }


        public void Set_Move()
        {
            var key1 = "Set_Move_1";
            var key2 = "Set_Move_2";
            this.Database.SetAdd(key1, new RedisValue[] { 1, 2, 3, 4 });
            this.Database.SetAdd(key2, new RedisValue[] { 10, 20, 30, 40 });
            Action dump = () =>
            {
                Console.WriteLine("execute range set 1: " + string.Join(",", this.Database.SetMembers(key1).Select(x => x.ToString())));
                Console.WriteLine("execute range set 2: " + string.Join(",", this.Database.SetMembers(key2).Select(x => x.ToString())));
            };
            dump();
            while (true)
            {
              
                Console.WriteLine("input key which need to move from set 1:");
                var mkey1 = Console.ReadLine();
                this.Database.SetMove(key1,key2,mkey1);
                dump();
                Console.WriteLine("input key which need to move from set 2:");
                var mkey2 = Console.ReadLine();
                this.Database.SetMove(key2, key1, mkey2);
                dump();
            }
        }


        public void SortedSet_Sort()
        {
            var redisKey = "SortedSet_Sort";
            var time = DateTime.Now;
            this.Database.KeyDelete(redisKey);
            Action dumpByRank = () => Console.WriteLine("execute range zset by rank: " + string.Join(",", this.Database.SortedSetRangeByRank(redisKey).Select(x => x.ToString())));
            Action dumpByScore = () => Console.WriteLine("execute range zset by score:" +string.Join(",",this.Database.SortedSetRangeByScore(redisKey).Select(x=>x.ToString())));
            while (true)
            {
                Console.WriteLine("input new key :");
                var key = Console.ReadLine();
                this.Database.SortedSetAdd(redisKey, key, this.GetUnixTimestamp(DateTime.Now));
              
                dumpByRank();
                dumpByScore();
            }

       
        }

        public void SortedSet_Increment()
        {
            var redisKey = "SortedSet_Increment";
            var time = DateTime.Now;
            // this.Database.KeyDelete(redisKey);
            //var tran = this.Database.CreateTransaction();
            //var getResult = tran.StringGetAsync(key);
            //tran.KeyDeleteAsync(key);
           // tran.Execute();

            for (int i = 0; i < 3; i++)
            {
                this.Database.SortedSetIncrement(redisKey, "key", (i+1) * 100 );
            }


        }

        public void SortedSet_Mutli_Sort()
        {
            var redisTimeKey = "SortedSet_Sort_Time";
            var redisCoinKey = "SortedSet_Sort_Coin";
            
            this.Database.KeyDelete(redisTimeKey);
            this.Database.KeyDelete(redisCoinKey);
        //   this.Database.SortedSetRangeByRank(redisTimeKey,0,or);
            Action dumpByRank = () => Console.WriteLine("execute range zset by rank: " + string.Join(",", this.Database.SortedSetRangeByRank(redisTimeKey).Select(x => x.ToString())));
            Action dumpByScore = () => Console.WriteLine("execute range zset by score:" + string.Join(",", this.Database.SortedSetRangeByScore(redisTimeKey).Select(x => x.ToString())));
            while (true)
            {
                Console.WriteLine("input new key :");
                var key = Console.ReadLine();
                this.Database.SortedSetAdd(redisTimeKey, key, this.GetUnixTimestamp(DateTime.Now));
                
                dumpByRank();
                dumpByScore();
            }
        }

        public long GetUnixTimestamp(DateTime time)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳
            
            long a = (time.ToUniversalTime().Ticks - timeStamp.Ticks) / 10000; //注意这里有时区问题，用now就要减掉8个小时
            return a;
        }
 

    }
}
