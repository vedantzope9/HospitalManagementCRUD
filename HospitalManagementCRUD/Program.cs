using System.Text;
using HospitalManagementCRUD.CommonFunctions;
using HospitalManagementCRUD.Models;
using HospitalManagementCRUD.RepositoryLayer.Implementations;
using HospitalManagementCRUD.RepositoryLayer.Interfaces;
using HospitalManagementCRUD.ServiceLayer.Implementations;
using HospitalManagementCRUD.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace HospitalManagementCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ISecLoginUserRepo, SecLoginUserRepo>();
            builder.Services.AddScoped<ISecLoginUserService, SecLoginUserService>();
            builder.Services.AddSingleton<MyMapper, MyMapper>();
            builder.Services.AddScoped<IDoctorRepo, DoctorRepo>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHospitalRepo, HospitalRepo>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<HospitalDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = builder.Configuration["AppSettings:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                            ValidateIssuerSigningKey = true
                        };
                    });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
