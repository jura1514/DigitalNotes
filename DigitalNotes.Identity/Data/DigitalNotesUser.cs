using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DigitalNotes.Identity.Data;

// Add profile data for application users by adding properties to the DigitalNotesUser class
public class DigitalNotesUser : IdentityUser
{
    [Required] [MaxLength(100)] public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(100)] public string LastName { get; set; } = string.Empty;
}