using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Models;

public class Question
{
    public int Id { get; set; }
    
    [Column("question")]
    public string? Text { get; set; }
    public List<Answer> Answers { get; set; }
}