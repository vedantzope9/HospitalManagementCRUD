using Client_HospitalManagmentCRUD.ServiceCalls;
using Client_HospitalManagmentCRUD.Services;

namespace Client_HospitalManagmentCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);          

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<HomeApiClient>(
                client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
                    client.Timeout = TimeSpan.FromSeconds(30);
                }
            );
            builder.Services.AddScoped<HomeService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
