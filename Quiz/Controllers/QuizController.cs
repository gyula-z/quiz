using Microsoft.AspNetCore.Mvc;
using Quiz.Models;
using Quiz.Services;

namespace Quiz.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public QuizController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("game")]
        public IActionResult GetRandomQuestion()
        {
            var question = _databaseService.GetRandomQuestion();
            if (question == null)
            {
                return NotFound("No questions found");
            }

            var answers = _databaseService.GetAnswersForQuestion(question.Id);
            question.Answers = answers;
            return Ok(question);
        }
        
        
        [HttpGet("questions")]
        public IActionResult GetAllQuestions()
        {
            var questions = _databaseService.GetAllQuestions();
            return Ok(questions);
        }
        
        [HttpPost("questions")]
        public IActionResult AddQuestion([FromBody] NewQuestion newQuestion)
        {
            if (newQuestion.Answers.Count < 2 || newQuestion.Answers.Count > 4)
            {
                return BadRequest("A question must have at least 2 and at most 4 answers.");
            }

            if (newQuestion.Answers.Any(a => string.IsNullOrEmpty(a.Answer)))
            {
                return BadRequest("None of the provided answers can be empty.");
            }

            // Log the state of each NewAnswer instance
            foreach (var answer in newQuestion.Answers)
            {
                Console.WriteLine(answer.ToString());
            }

            int correctAnswersCount = newQuestion.Answers.Count(a => a.IsCorrect == true);
            if (correctAnswersCount != 1)
            {
                return BadRequest($"Exactly 1 answer must be marked as the correct one, but found {correctAnswersCount}.");
            }

            var questionId = _databaseService.AddQuestion(newQuestion);

            return Ok(questionId);
        }



        
        [HttpDelete("questions/{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            _databaseService.DeleteQuestion(id);
            return Ok();
        }


    }  