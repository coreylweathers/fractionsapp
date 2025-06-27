using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionsApp.Shared.Models;

public record UserProgressModel
{
    public Guid Id { get; set; }
    public string ActivityType { get; set; }
    public string UserId { get; set; }
    public DateTime CompletedDate { get; set; } // Changed accessor to public  
    public bool IsSynced { get; set; }
}