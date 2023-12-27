using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Postgres
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;

        public DbSet<UserRole> UserRoles { get; set; } = null!;

        public DbSet<Claim> Claims { get; set; } = null!;

        public DbSet<RoleClaim> RoleClaims { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region product modifications

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.Sku)
              .HasColumnType("varchar(50)")
              .IsRequired();

            #endregion product modifications

            #region order modifications

            modelBuilder.Entity<Order>()
                .Property(p => p.UserId)
                .IsRequired();

            #endregion order modifications

            #region order item modifications

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.ProductId)
                .IsRequired();

            #endregion order item modifications

            #region identity user modifications

            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(p => p.Username)
               .HasColumnType("varchar(50)")
               .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .HasColumnType("varchar(8000)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.NameSurname)
                .HasColumnType("varchar(100)");

            #endregion identity user modifications

            #region role modifications

            modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion role modifications

            #region user role modifications

            modelBuilder.Entity<UserRole>()
                .Property(p => p.UserId)
                  .IsRequired();

            modelBuilder.Entity<UserRole>()
               .Property(p => p.RoleId)
                 .IsRequired();

            #endregion user role modifications

            #region claim modifications

            modelBuilder.Entity<Claim>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion claim modifications

            #region role claim modifications

            modelBuilder.Entity<RoleClaim>()
                .Property(p => p.ClaimId)
                  .IsRequired();

            modelBuilder.Entity<RoleClaim>()
               .Property(p => p.RoleId)
                 .IsRequired();

            #endregion role claim modifications

            #region seed

            var userId = Guid.Parse("87622649-96c8-40b5-bcef-8351b0883b49");
            var createdAt = new DateTime(2024, 1, 1);

            modelBuilder.Entity<Role>().HasData(
                CreateRole(1, "freemium"),
                CreateRole(2, "paid"),
                CreateRole(3, "admin")
            );

            modelBuilder.Entity<UserRole>().HasData(
                CreateUserRole(1, 2),
                CreateUserRole(2, 3)
            );

            modelBuilder.Entity<Claim>().HasData(
                CreateClaim(1, "first_claim"),
                CreateClaim(2, "second_claim")
            );

            modelBuilder.Entity<RoleClaim>().HasData(
                CreateRoleClaim(1, 3, 1),
                CreateRoleClaim(2, 3, 2)
            );

            modelBuilder.Entity<User>().HasData(
                CreateUser("Mustafa Korkmaz",
                    "mustafakorkmazdev@gmail.com"));

            Role CreateRole(int id, string name)
            {
                return new Role(id, name, createdAt);
            }

            UserRole CreateUserRole(int userRoleId, int roleId)
            {
                return new UserRole(userRoleId, userId, roleId, createdAt);
            }

            Claim CreateClaim(int id, string name)
            {
                return new Claim(id, name, createdAt);
            }

            RoleClaim CreateRoleClaim(int id, int roleId, int claimId)
            {
                return new RoleClaim(id, roleId, claimId, createdAt);
            }

            User CreateUser(string name, string email)
            {
                return new User(userId, email, email, name, true,
                     "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==", createdAt);
            }

            #endregion seed
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}