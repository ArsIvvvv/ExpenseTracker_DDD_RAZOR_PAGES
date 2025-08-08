using ExpenseTracker.Domain.AvailableEntities;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Data
{
    public class ExpenseDbContext: DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AvailableCategory> AvailableCategories { get; set; }

        public ExpenseDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Конфигурация User
            modelBuilder.Entity<User>(e =>
            {

                e.OwnsOne(x => x.Email, a =>
                {
                    a.Property(x => x.Value)
                    .IsRequired();
                });

                e.OwnsOne(x => x.Password, a =>
                {
                    a.Property(x => x.HashValue)
                    .IsRequired();
                });

                e.HasMany(u => u.Expenses)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            //Конфигурация Expense
            modelBuilder.Entity<Expense>(e =>
            {

                e.OwnsOne(x => x.Money, a =>
                {
                    a.Property(x => x.Amount)
                    .IsRequired();

                    a.Property(x => x.Currency)
                    .IsRequired();
                });

                e.OwnsOne(x => x.Category, a =>
                {
                    a.Property(x => x.Name)
                    .IsRequired();
                });

            });

            var category = new[]
            {
                 new  { Id = 1, Name = "Электроника" },
                new  { Id = 2, Name = "Одежда" },
                new  { Id = 3, Name = "Еда" },
                new { Id = 4, Name = "Развлечения" },
                new  { Id = 5, Name = "Медицина" },
                new { Id = 6, Name = "Путешествия" },
                new  { Id = 7, Name = "Транспорт" }
            };

            modelBuilder.Entity<AvailableCategory>(builder =>
            {
                builder.HasData(category.Select(p => new
                {
                    p.Id,
                    p.Name,

                }));
            });
                

                
        }
    }
}
