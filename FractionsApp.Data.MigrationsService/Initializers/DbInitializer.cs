using FractionsApp.Shared.Data.Context;
using FractionsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FractionsApp.Data.Migrations.Initializers;

public static class DbInitializer
{
    public static async Task SeedDatabaseAsync(FractionsDbContext context)
    {
        // Ensure database is created and migrations are applied
        await context.Database.MigrateAsync();
            
        // Check if we already have problem sets
        if (await context.ProblemSets.AnyAsync())
        {
            return; // Database has been seeded already
        }
            
        // Create sample problem sets
        var problemSets = new List<ProblemSetModel>
        {
            new ProblemSetModel
            {
                Name = "Basic Fraction Addition",
                Description = "Simple problems for learning to add fractions with the same denominator",
                Difficulty = "Easy",
                Category = "Addition",
                CreatedDate = DateTime.Now,
                Problems = new List<FractionProblemModel>
                {
                    new FractionProblemModel
                    {
                        Question = "What is 1/4 + 2/4?",
                        Answer = "3/4",
                        Options = new[] { "1/2", "3/4", "3/8", "1/8" },
                        Explanation = "When adding fractions with the same denominator, add the numerators and keep the same denominator.",
                        Hint = "Add the top numbers (numerators) and keep the bottom number (denominator) the same.",
                        Difficulty = 1,
                        Operand1 = new FractionModel { Numerator = 1, Denominator = 4 },
                        Operand2 = new FractionModel { Numerator = 2, Denominator = 4 },
                        Operation = "+"
                    },
                    new FractionProblemModel
                    {
                        Question = "What is 3/5 + 1/5?",
                        Answer = "4/5",
                        Options = new[] { "4/5", "4/10", "3/10", "2/5" },
                        Explanation = "When adding fractions with the same denominator, add the numerators and keep the same denominator.",
                        Hint = "Add the top numbers (numerators) and keep the bottom number (denominator) the same.",
                        Difficulty = 1,
                        Operand1 = new FractionModel { Numerator = 3, Denominator = 5 },
                        Operand2 = new FractionModel { Numerator = 1, Denominator = 5 },
                        Operation = "+"
                    }
                }
            },
            new ProblemSetModel
            {
                Name = "Basic Fraction Subtraction",
                Description = "Simple problems for learning to subtract fractions with the same denominator",
                Difficulty = "Easy",
                Category = "Subtraction",
                CreatedDate = DateTime.Now,
                Problems = new List<FractionProblemModel>
                {
                    new FractionProblemModel
                    {
                        Question = "What is 3/4 - 1/4?",
                        Answer = "2/4",
                        Options = new[] { "2/4", "1/2", "1/4", "3/8" },
                        Explanation = "When subtracting fractions with the same denominator, subtract the numerators and keep the same denominator. The answer 2/4 can be simplified to 1/2.",
                        Hint = "Subtract the top numbers (numerators) and keep the bottom number (denominator) the same.",
                        Difficulty = 1,
                        Operand1 = new FractionModel { Numerator = 3, Denominator = 4 },
                        Operand2 = new FractionModel { Numerator = 1, Denominator = 4 },
                        Operation = "-"
                    },
                    new FractionProblemModel
                    {
                        Question = "What is 5/6 - 2/6?",
                        Answer = "3/6",
                        Options = new[] { "3/6", "1/2", "3/12", "1/3" },
                        Explanation = "When subtracting fractions with the same denominator, subtract the numerators and keep the same denominator. The answer 3/6 can be simplified to 1/2.",
                        Hint = "Subtract the top numbers (numerators) and keep the bottom number (denominator) the same.",
                        Difficulty = 1,
                        Operand1 = new FractionModel { Numerator = 5, Denominator = 6 },
                        Operand2 = new FractionModel { Numerator = 2, Denominator = 6 },
                        Operation = "-"
                    }
                }
            },
            new ProblemSetModel
            {
                Name = "Mixed Denominator Addition",
                Description = "Problems for learning to add fractions with different denominators",
                Difficulty = "Medium",
                Category = "Addition",
                CreatedDate = DateTime.Now,
                Problems = new List<FractionProblemModel>
                {
                    new FractionProblemModel
                    {
                        Question = "What is 1/2 + 1/3?",
                        Answer = "5/6",
                        Options = new[] { "2/5", "1/6", "5/6", "2/6" },
                        Explanation = "To add fractions with different denominators, find a common denominator first. For 1/2 and 1/3, the least common denominator is 6. Convert 1/2 to 3/6 and 1/3 to 2/6. Then add: 3/6 + 2/6 = 5/6.",
                        Hint = "Find a common denominator by multiplying the denominators.",
                        Difficulty = 3,
                        Operand1 = new FractionModel { Numerator = 1, Denominator = 2 },
                        Operand2 = new FractionModel { Numerator = 1, Denominator = 3 },
                        Operation = "+"
                    },
                    new FractionProblemModel
                    {
                        Question = "What is 2/3 + 3/4?",
                        Answer = "17/12",
                        Options = new[] { "5/7", "17/12", "5/12", "1/12" },
                        Explanation = "To add fractions with different denominators, find a common denominator first. For 2/3 and 3/4, the least common denominator is 12. Convert 2/3 to 8/12 and 3/4 to 9/12. Then add: 8/12 + 9/12 = 17/12.",
                        Hint = "Find a common denominator by multiplying the denominators.",
                        Difficulty = 3,
                        Operand1 = new FractionModel { Numerator = 2, Denominator = 3 },
                        Operand2 = new FractionModel { Numerator = 3, Denominator = 4 },
                        Operation = "+"
                    }
                }
            }
        };
            
        // Add problem sets to database
        await context.ProblemSets.AddRangeAsync(problemSets);
        await context.SaveChangesAsync();
    }
}