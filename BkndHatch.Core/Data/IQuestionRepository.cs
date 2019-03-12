using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BkndHatch.Data
{
    public interface IQuestionRepository
    {
        Question LoadById(int questionId);

        IEnumerable<Question> Load(DateTime startTime,DateTime endTime);

        void Add(Question question);

    }
}
