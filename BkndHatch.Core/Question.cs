using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkndHatch
{
    public class Question:BsonEntity
    {
        
        public int QuestionId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public override string ToString()
        {
            return $"QuestionId: {QuestionId},Content:{Content},CreateTime:{CreateTime}";
        }
    }
}
