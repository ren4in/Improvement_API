namespace Improvement_API.db;
using Microsoft.EntityFrameworkCore;


public partial class DepartmentDbContext : DbContext
{
    public DepartmentDbContext()
    {
    }
    public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
      : base(options)
    {

    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Report> Report { get; set; }

    public virtual DbSet<Role> Role { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.id_Report);

            entity.Property(e => e.id_Report).HasColumnName("id_Report");
            entity.Property(e => e.Manager_Comment).HasColumnType("text");

            entity.HasOne(d => d.id_UserNavigation)
                  .WithMany(p => p.Reports)
                  .HasForeignKey(d => d.id_User)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Reports_Users");
        });

      /* modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.id_Role);

            entity.Property(e => e.id_Role).HasColumnName("id_Role");

            entity.HasMany(u => u.Users)
                 .WithOne(r => r.id_RoleNavigation)
                 .HasForeignKey(r => r.id_Role)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_Roles_Users");
        }); */



        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.id_User);

            entity.Property(e => e.id_User).HasColumnName("id_User");
            entity.Property(e => e.FirstName).HasColumnName("FirstName");
            entity.Property(e => e.MiddleName).HasColumnName("MiddleName");
            entity.Property(e => e.LastName).HasColumnName("LastName");
            entity.Property(e => e.Login).HasColumnName("Login");
            entity.Property(e => e.Password).HasColumnName("Password");

            entity.HasMany(u => u.Reports)
                  .WithOne(r => r.id_UserNavigation)
                  .HasForeignKey(r => r.id_User)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Reports_Users");
            entity.HasOne(d => d.id_RoleNavigation)
                 .WithMany(p => p.Users)
                 .HasForeignKey(d => d.id_Role)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_Roles_Users");
        });
    }

}
