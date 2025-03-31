using EmployeeManager.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Supabase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        var bytes = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:JwtSecret"]);

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication().AddJwtBearer(options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(bytes),
                ValidAudience = builder.Configuration["Authentication:ValidAudience"],
                ValidIssuer = builder.Configuration["Authentication:ValidIssuer"]
            };

        });

        
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
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .RequireAuthorization();
        
        app.Run();
    }
}