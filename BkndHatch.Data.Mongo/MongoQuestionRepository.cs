using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
 

namespace BkndHatch.Data.Mongo
{
    public class MongoQuestionRepository:MongoRepositoryBase<Question>, IQuestionRepository
    {
      
         
        public IEnumerable<Question> Load(DateTime startTime, DateTime endTime)
        {
            return this.Collection().Find(x=>x.CreateTime>=startTime && x.CreateTime<=endTime).ToList();
        }

        public Question LoadById(int questionId)
        {
            return this.Collection().Find(x => x.QuestionId == questionId).FirstOrDefault();
        }

        public void Add(Question question)
        {
            this.Collection().InsertOne(question);
        }

        protected override string TableName => "questions";

    }
}
