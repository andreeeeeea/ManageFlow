using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManageFlow.Components;
using ManageFlow.Components.Account;
using ManageFlow.Data;
using ManageFlow.Services;
using Supabase;

namespace ManageFlow;

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

        builder.Services.AddScoped<ITaskManagerService, TaskManagerService>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        // Register Identity with Entity Framework and use Supabase as the database
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration["Supabase:ConnectionString"]));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // If you are not using an email sender in production, you can configure it here as below:
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}
