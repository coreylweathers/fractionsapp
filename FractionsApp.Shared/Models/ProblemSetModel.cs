using System;
using System.Collections.Generic;

namespace FractionsApp.Shared.Models;

public class ProblemSetModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Difficulty { get; set; }  // Easy, Medium, Hard
    public string Category { get; set; }    // Addition, Subtraction, Multiplication, etc.
    public List<FractionProblemModel> Problems { get; set; } = new List<FractionProblemModel>();
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

public class FractionProblemModel
{
    public Guid Id { get; set; }
    public Guid ProblemSetId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string[] Options { get; set; }  // For multiple choice questions
    public string Explanation { get; set; }
    public string Hint { get; set; }
    public int Difficulty { get; set; }    // 1-5 scale
    public FractionModel Operand1 { get; set; }
    public FractionModel Operand2 { get; set; }
    public string Operation { get; set; }  // +, -, *, /
}