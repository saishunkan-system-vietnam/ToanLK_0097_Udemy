using BaseProject.Shared.Entity;
using BaseProject.Shared.Modal.Entity;
using BaseProject.Shared.Modal.LinkTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Configuration;

namespace BaseProject.Infrastructure
{
    public class DBMasterContext : DbContext
    {
        public DBMasterContext(DbContextOptions<DBMasterContext> dbContextOption) : base(dbContextOption)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        public DbSet<Product>? Product { get; set; }
        public DbSet<Account>? Account { get; set; }
        public DbSet<Technology> Technology { get; set; }
        public DbSet<Shared.Modal.Entity.Type>? Type { get; set; }
        public DbSet<Setting>? Setting { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<News> News { get; set; }
    }

    public class DBSlaveContext : DbContext
    {
        public DBSlaveContext(DbContextOptions<DBSlaveContext> dbContextOption) : base(dbContextOption)
        {
        }
        public DbSet<Account>? Account { get; set; }
        public DbSet<Product>? Product { get; set; }
        public DbSet<Technology>? Technology { get; set; }
        public DbSet<ProductTechnology>? ProductTechnology { get; set; }
        public DbSet<Shared.Modal.Entity.Type>? Type { get; set; }
        public DbSet<Setting>? Setting { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<News> News { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // product m - m technology
            modelBuilder.Entity<ProductTechnology>()
                        .HasKey(bc => new { bc.ProductId, bc.TechnologyId });

            modelBuilder.Entity<ProductTechnology>()
                        .HasOne(bc => bc.Product)
                        .WithMany(bc => bc.ProductTechnologies)
                        .HasForeignKey(bc => bc.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductTechnology>()
                       .HasOne(bc => bc.Technology)
                       .WithMany(bc => bc.ProductTechnologies)
                       .HasForeignKey(bc => bc.TechnologyId);


            // product m - m account (cart)
            modelBuilder.Entity<Cart>()
                        .HasKey(bc => new { bc.ProductId, bc.AccountId});

            modelBuilder.Entity<Cart>()
                       .HasOne(bc => bc.Product);

            modelBuilder.Entity<Cart>()
                       .HasOne(bc => bc.Account);

            // product 1 - m type
            modelBuilder.Entity<Shared.Modal.Entity.Type>()
                        .HasMany(p => p.Products)
                        .WithOne(t => t.Type)
                        .HasForeignKey(t => t.TypeId);

        }
    }
}
