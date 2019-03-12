using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BkndHatch.Data.Mongo
{
    public class MongoQuestionRepository:IQuestionRepository
    {
        const string CONN_STR = "mongodb://localhost:27017";
        const string DATABASE_NAME = "Faq";
        const string TABLE_NAME = "questions";
        private static IMongoDatabase database;

        public MongoQuestionRepository()
        {
            if (database == null)
                database = new MongoClient(CONN_STR).GetDatabase(DATABASE_NAME);
        }

        public IEnumerable<Question> Load(DateTime startTime, DateTime endTime)
        {
            return null;
        }

        public Question LoadById(int id)
        {
            return database.GetCollection<Question>(TABLE_NAME).Find(x => x.QuestionId == id).FirstOrDefault();
        }

       
    }
}
