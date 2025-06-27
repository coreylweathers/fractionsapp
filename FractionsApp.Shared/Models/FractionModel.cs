using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionsApp.Shared.Models;

public record FractionModel
{
    public Guid Id { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get; set; }
}