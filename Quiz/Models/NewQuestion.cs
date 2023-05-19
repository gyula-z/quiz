namespace Quiz.Models;

public class NewQuestion
{
    public string Question { get; set; }
    public List<NewAnswer> Answers { get; set; }
}
public class NewAnswer
{
    public string Answer { get; set; }

    private bool _isCorrect;
    public bool IsCorrect 
    {
        get => _isCorrect;
        set
        {
            _isCorrect = value;
        }
    }

    public int Correct
    {
        set
        {
            _isCorrect = value == 1;
        }
    }
    
    // New property for matching with "is_correct" in JSON
    public int Is_Correct
    {
        set
        {
            _isCorrect = value == 1;
        }
    }
    
    public override string ToString()
    {
        return $"Answer: {Answer}, IsCorrect: {_isCorrect}";
    }
}
