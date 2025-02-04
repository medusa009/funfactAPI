namespace HNG_Stage_One.DTO;

public class NumberClassificationResponse
{
    public object Number { get; set; } // Nullable to handle invalid input
    public bool? IsPrime { get; set; }
    public bool? IsPerfect { get; set; }
    public List<string>? Properties { get; set; }
    public int? DigitSum { get; set; }
    public string? FunFact { get; set; }
    public bool Error { get; set; } // Flag for error handling
}