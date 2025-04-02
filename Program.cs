using EmployeeManager.Components;
using EmployeeManager.Services;
using EmployeeManager.Data.Models;
using EmployeeManager.Data.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supabase.Gotrue;
using Supabase;
using EmployeeManager.Data;

namespace EmployeeManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var url = builder.Configuration["Supabase:Url"];
        var key = builder.Configuration["Supabase:Key"];

        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
        {
            throw new Exception("Supabase URL or Key not found in configuration");
        }

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.SignIn.RequireConfirmedAccount = false;
        }).AddDefaultTokenProviders();

        builder.Services.AddScoped<ITaskManagerService, TaskManagerService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserStore<ApplicationUser>, SupabaseUserStore>();
        builder.Services.AddScoped<IRoleStore<IdentityRole>, SupabaseRoleStore>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
            options.AccessDeniedPath = "/access-denied";
        });

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        try 
        {
            var supabase = new Supabase.Client(url, key, options);
            
            await supabase.InitializeAsync();
            Console.WriteLine("Supabase client initialized successfully");
            
            builder.Services.AddSingleton(supabase);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Supabase client: {ex.Message}");
            throw;
        }
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}