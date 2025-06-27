using System;
using System.Collections.Generic;

namespace FractionsApp.Shared.Models;

public record ProblemSetModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Description { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Difficulty { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Category { get; set; } // Added 'required' modifier to ensure non-null value
    public List<FractionProblemModel> Problems { get; set; } = new List<FractionProblemModel>();
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

public record FractionProblemModel
{
    public Guid Id { get; set; }
    public Guid ProblemSetId { get; set; }
    public required string Question { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Answer { get; set; } // Added 'required' modifier to ensure non-null value
    public required string[] Options { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Explanation { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Hint { get; set; } // Added 'required' modifier to ensure non-null value
    public int Difficulty { get; set; }
    public required FractionModel Operand1 { get; set; } // Added 'required' modifier to ensure non-null value
    public required FractionModel Operand2 { get; set; } // Added 'required' modifier to ensure non-null value
    public required string Operation { get; set; } // Added 'required' modifier to ensure non-null value
}