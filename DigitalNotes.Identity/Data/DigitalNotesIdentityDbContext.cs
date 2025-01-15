using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotes.Identity.Data;

public class DigitalNotesIdentityDbContext : IdentityDbContext<DigitalNotesUser>
{
    public DigitalNotesIdentityDbContext(DbContextOptions<DigitalNotesIdentityDbContext> options)
        : base(options)
    {
    }
}