using System.Numerics;
using ASP_Project.DB;
using ASP_Project.Repository;
using ASP_Project.Service;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASP_Project;
public class Program{
    static void Main(string[] args){
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PokemonTrainerDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionStr")));
        builder.Services.AddHttpClient<IRESTPKMService, RESTPKMService>();
        builder.Services.AddScoped<IPKMTeamRepo, PokemonTeamRepository>();
        builder.Services.AddScoped<ITrainerRepo, TrainerRepository>();
        builder.Services.AddScoped<ITrainerService, TrainerService>(); 
        builder.Services.AddScoped<IPKMTeamService, PokemonTeamServices>(); 
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        WebApplication app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}