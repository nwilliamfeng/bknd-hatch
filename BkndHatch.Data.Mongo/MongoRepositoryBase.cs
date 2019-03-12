using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace BkndHatch.Data.Mongo
{
    public abstract class MongoRepositoryBase<T>
        where T:class
    {
        const string CONN_STR = "mongodb://localhost:27017";
   
        protected IMongoDatabase Database { get; private set; }

        protected MongoRepositoryBase()
        {
            Database = new MongoClient(CONN_STR).GetDatabase(DataBaseName);
        }

        protected IMongoCollection<T> Collection(MongoCollectionSettings settings=null)
        {
            return this.Database.GetCollection<T>(this.TableName,settings);
        }

       

        protected abstract string TableName { get; }

        protected virtual string DataBaseName
        {
            get { return "Faq"; }
        }
    }
}
