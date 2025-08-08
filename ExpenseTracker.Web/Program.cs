using ExpenseTracker.Application.Interface;
using ExpenseTracker.Application.Service;
using ExpenseTracker.Domain.Repository;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages().AddSessionStateTempDataProvider();
            builder.Services.AddSession();
            builder.Configuration.AddUserSecrets("secrets.json");
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ExpenseDbContext>(options =>
            options.UseSqlServer(connectionString,
            sqlOptions => sqlOptions.MigrationsAssembly("ExpenseTracker.Infrastructure")));

            builder.Services.AddScoped<IExpenseRepositoty, ExpenseRepository>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(1); 
            });
            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error");
            app.UseSession();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }

    }
}
