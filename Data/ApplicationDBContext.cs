using Ecommerce.Extensions;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<EmailVerification> emailVerifications { get; set; }
        public DbSet<Profiles> profiles { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<MediaModel> media { get; set; }
        public DbSet<Messages> messages { get; set; }
        public DbSet<Payments> payments { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<CommentLikes> commentLikes { get; set; }
        public DbSet<Follower> followers { get; set; }
        public DbSet<Following> followings { get; set; }
        public DbSet<BlockedUsers> blockedUsers { get; set; }
        public DbSet<Order_Product> order_Products { get; set; }
        public DbSet<ProductsVariants> Variants { get; set; }
        public DbSet<Reviews> reviews { get; set; }
        public DbSet<Wishlists> wishlists { get; set; }
        public DbSet<ProductFiles> productFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER"
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<Payments>()
                .Property(p => p.Status)
                .HasConversion<string>();

            builder.Entity<Profiles>()
            .Property(p => p.Gender)
            .HasConversion<string>();

            builder.Entity<User>()
            .Property(u => u.role)
            .HasConversion<string>();

            builder.Entity<Orders>()
            .Property(p => p.Order_Status).HasConversion<string>();
            builder.Entity<Payments>()
            .Property(p => p.Status).HasConversion<string>();


            builder.ConfigRelations();




        }




    }
}
