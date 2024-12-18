using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Contracts;

public record RegisterUserRequest(
    [Required] string UserName,
    [Required] string Password,
    [Required] string Email,
    [Required] string role);