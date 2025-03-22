using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyWebApplication.Domain;
using MyWebApplication.Domain.Entities;
using MyWebApplication.Domain.Repositories.Abstract;
using MyWebApplication.Domain.Repositories.EntityFramework;
using MyWebApplication.Infrastructure;

namespace MyWebApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            //Подключаем в конфигурацию файл appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //Оборачиваем секцию Project в объектную форму для удобства
            IConfiguration configuration = configBuild.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;

            //Подключаем контекст БД
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(config.Database.ConnectionString)
                //ИСПРАВЛЯЕМ БАГ
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            builder.Services.AddTransient<ICasesRepository, EFCasesRepository>();
            builder.Services.AddTransient<DataManager>();
            
            //Настраиваем Identity систему
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            //Настраиваем Auth Cookie!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyWebApplicationAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied"; 
                options.SlidingExpiration = true;
            });

            //Подключаем функционал контроллеров
            builder.Services.AddControllersWithViews();

            //Собираем конфигурацию
            WebApplication app = builder.Build();

            //! Порядок следования middleware очень важен, они будут выполняться согласно нему

            //Подключаем использование статичных файлов(js,css,любых)
            app.UseStaticFiles();

            //Подключаем систему маршрутизации
            app.UseRouting();

            //Подключаем аутентификацию и авторизацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            
            //Регистрируем нужные нам маршруты
            app.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");

            await app.RunAsync();
        }
    }
}
