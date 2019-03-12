using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BkndHatch.Data;

namespace BkndHatch.Tests
{
    public  class QuestionRepositoryTest
    {
        private IQuestionRepository _repository;

        public QuestionRepositoryTest(IQuestionRepository repository)
        {
            this._repository = repository;
        }


        public void Execute()
        {
            this.TestQueryById();
            this.TestQueryByTime();
            this.TestAdd();
        }

        public  void TestQueryById()
        {
           Console.WriteLine($"query by id,result->: {this._repository.LoadById(100)}");
        } 

        public void TestQueryByTime()
        {

        }

        public void TestAdd()
        {
            
            var question = new Question { Content = "question1", QuestionId = 102, CreateTime = DateTime.Now };
            Console.WriteLine("before insert :"+question);
            this._repository.Add(question);
            Console.WriteLine("after insert :" + question);
        }
    }
}
