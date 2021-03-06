﻿using Core.Common;
using Core.Services.Interfaces;
using Database.Entities;
using Eve.AutoMapper;
using Eve.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eve.Quiz
{
    public class Quiz
    {
        private static readonly IQuizService quizService = ServicesFactory.GetInstance().CreateIQuizService();

        public List<QuestionViewModel> Questions { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        public QuestionViewModel CurrentQuestion { get; set; }
        public int CorrectAnswersNum { get; set; }
        public int WrongAnswersNum { get; set; }

        public static int TotalNumberOfQuestions { get; private set; } = Shared.Config.Properties.Default.TotalNumberOfQuestions;

        public Quiz() { }

        public Quiz(List<QuestionViewModel> questions, List<AnswerViewModel> answers, int correctAnswers, int wrongAnswers)
        {
            Questions = questions;
            Answers = answers;
            CorrectAnswersNum = correctAnswers;
            WrongAnswersNum = wrongAnswers;
            CurrentQuestion = questions.First();
        }

        public static async Task<Quiz> GetFirst()
        {
            List<QuestionViewModel> nextQuestions = Mapping.Mapper.Map<List<Question>, List<QuestionViewModel>>
                  (await quizService.GetRandomQuestions(TotalNumberOfQuestions));
            return await SetQuiz(nextQuestions);
        }

        public async Task<Quiz> GetNext()
        {
            return await SetQuiz(Questions.Skip(1).ToList(), CorrectAnswersNum, WrongAnswersNum);
        }

        public void ChooseAnswer(int indexOfAnswer)
        {
            if (Answers[indexOfAnswer].Correct == 1)
                CorrectAnswersNum++;
            else WrongAnswersNum++;
        }

        private static async Task<Quiz> SetQuiz(List<QuestionViewModel> nextQuestions, int correctNum = 0, int wrongNum = 0)
        {
            if (nextQuestions.Count == 0)
                return null;

            QuestionViewModel nextQuestion = nextQuestions.First();
            List<AnswerViewModel> newAnswers = Mapping.Mapper.Map<List<Answer>, List<AnswerViewModel>>(await quizService.GetAnswers(nextQuestion.IdQuestion));
            return new Quiz()
            {
                Questions = nextQuestions,
                Answers = newAnswers,
                CurrentQuestion = nextQuestion,
                CorrectAnswersNum = correctNum,
                WrongAnswersNum = wrongNum
            };
        }
    }
}

