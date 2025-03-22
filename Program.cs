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
            
            //���������� � ������������ ���� appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //����������� ������ Project � ��������� ����� ��� ��������
            IConfiguration configuration = configBuild.Build();
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;

            //���������� �������� ��
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(config.Database.ConnectionString)
                //���������� ���
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            builder.Services.AddTransient<ICasesRepository, EFCasesRepository>();
            builder.Services.AddTransient<DataManager>();
            
            //����������� Identity �������
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            //����������� Auth Cookie!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyWebApplicationAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied"; 
                options.SlidingExpiration = true;
            });

            //���������� ���������� ������������
            builder.Services.AddControllersWithViews();

            //�������� ������������
            WebApplication app = builder.Build();

            //! ������� ���������� middleware ����� �����, ��� ����� ����������� �������� ����

            //���������� ������������� ��������� ������(js,css,�����)
            app.UseStaticFiles();

            //���������� ������� �������������
            app.UseRouting();

            //���������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            
            //������������ ������ ��� ��������
            app.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");

            await app.RunAsync();
        }
    }
}
