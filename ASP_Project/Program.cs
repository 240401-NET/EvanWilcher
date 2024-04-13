namespace ASP_Project;
public class Program{
    static void Main(string[] args){
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        WebApplication app = builder.Build();
        app.MapGet("/", () => "Hello World!");
        app.Run();
    }
}