namespace ASP_Project;
public class Program{
    static void Main(string[] args){
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        WebApplication app = builder.Build();
        app.UseRouting();
        app.MapGet("/", () => "Hello World!");
        app.MapControllerRoute(name: "defalt", pattern: "{controller=Trainer}/{action=Index}/");
        app.Run();
    }
}