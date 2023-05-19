using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models;

public class Answer
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    
    [Column("answer")]
    public string? Text { get; set; }
    public bool IsCorrect { get; set; }
}