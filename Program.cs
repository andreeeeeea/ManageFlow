using EmployeeManager.Components;
using EmployeeManager.Services;
using Supabase;

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
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}