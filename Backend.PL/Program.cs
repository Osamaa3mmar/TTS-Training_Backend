
using Backend.BLL.Services.Classes;
using Backend.BLL.Services.Interfaces;
using Backend.DAL.Data;
using Backend.DAL.Repository.Classes;
using Backend.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Backend.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();




            //Dep injection
            builder.Services.AddDbContext<ApplicationDbContext>(op=>op.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));


            //repositories
            builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
            //services
            builder.Services.AddScoped<ICategoryService,CategoryService>(); 


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
