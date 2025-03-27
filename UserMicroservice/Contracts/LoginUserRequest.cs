using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Contracts;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);