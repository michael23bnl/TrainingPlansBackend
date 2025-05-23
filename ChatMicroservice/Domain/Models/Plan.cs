

namespace ChatMicroservice.Models;
public class Plan
{
    public string? Category { get; set; }
    
    public List<Exercise> Exercises {get; set;}
    
}