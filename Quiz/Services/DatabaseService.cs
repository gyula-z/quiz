using System.Data;
using Dapper;
using Quiz.Models;
using System.Linq;

namespace Quiz.Services;

    public class DatabaseService
    {
        private IDbConnection _connection;

        public DatabaseService(IDbConnection connection)
        {
            _connection = connection;
        }

        public Question GetRandomQuestion()
        {
            var questionRow = _connection.QueryFirstOrDefault("SELECT TOP 1 * FROM questions ORDER BY NEWID();");
            var question = new Question
            {
                Id = questionRow.id,
                Text = questionRow.question
            };
            return question;
        }

        public List<Answer> GetAnswersForQuestion(int questionId)
        {
            var answers = _connection.Query("SELECT * FROM answers WHERE question_id = @QuestionId", new { QuestionId = questionId })
                                     .Select(row => new Answer
                                     {
                                         Id = row.id,
                                         Text = row.answer,
                                         IsCorrect = row.is_correct,
                                         QuestionId = row.question_id
                                     }).ToList();
            return answers;
        }

        public List<Question> GetAllQuestions()
        {
            var questions = _connection.Query("SELECT * FROM questions")
                                       .Select(row => new Question
                                       {
                                           Id = row.id,
                                           Text = row.question
                                       }).ToList();

            foreach (var question in questions)
            {
                question.Answers = GetAnswersForQuestion(question.Id);
            }

            return questions;
        }

        public int AddQuestion(NewQuestion newQuestion)
        {
            var sql = "INSERT INTO questions (question) OUTPUT INSERTED.id VALUES (@Question)";
            var questionId = _connection.Query<int>(sql, new { Question = newQuestion.Question }).Single();

            foreach (var answer in newQuestion.Answers)
            {
                AddAnswer(questionId, answer);
            }

            return questionId;
        }

        private void AddAnswer(int questionId, NewAnswer newAnswer)
        {
            var sql = "INSERT INTO answers (question_id, answer, is_correct) VALUES (@QuestionId, @Answer, @IsCorrect)";
            _connection.Execute(sql, new { QuestionId = questionId, Answer = newAnswer.Answer, IsCorrect = newAnswer.IsCorrect });
        }

        public void DeleteQuestion(int id)
        {
            _connection.Execute("DELETE FROM answers WHERE question_id = @Id", new { Id = id });
            _connection.Execute("DELETE FROM questions WHERE id = @Id", new { Id = id });
        }
    }
