using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Surf_Boards.Areas.Identity.Data;

namespace Surf_Boards.Data;

public class Surf_BoardsContext1 : IdentityDbContext<Surf_BoardsUser>
{
    public Surf_BoardsContext1(DbContextOptions<Surf_BoardsContext1> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new Surf_BoardUserEntityConfiguration());

    }

    public class Surf_BoardUserEntityConfiguration : IEntityTypeConfiguration<Surf_BoardsUser>
    {
        public void Configure(EntityTypeBuilder<Surf_BoardsUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
        }
    }
}
