using Core.Common;
using Core.Services.Interfaces;
using Database.Commands;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class QuizService : IQuizService
    {
        public async Task<List<Answer>> GetAnswers(int idQuestion)
        {
            List<Answer> answers = new List<Answer>();
            Answer tmp = new Answer()
            {
                IdQuestion = idQuestion
            };
            DbCommand<Answer> command = new SelectWithAttributeValuesCommand<Answer>(new string[] { "IdQuestion" });
            answers = await ServiceHelper<Answer>.ExecuteSelectCommand(command, tmp);
            return answers;
        }

        public async Task<List<Question>> GetRandomQuestions(int num)
        {
            DbCommand<Question> selectAllCommand = new SelectAllCommand<Question>();
            List<Question> questions = await ServiceHelper<Question>.ExecuteSelectCommand(selectAllCommand);
            return questions.OrderBy(a => Guid.NewGuid()).Take(num).ToList();
        }

    }
}
