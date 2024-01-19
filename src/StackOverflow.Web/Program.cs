using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using StackOverflow.Application;
using StackOverflow.Application.External;
using StackOverflow.Infrastructure;
using StackOverflow.Infrastructure.Extensions;

namespace StackOverflow.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //Log4net 
            builder.Logging.ClearProviders();
            builder.Logging.AddLog4Net();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(x =>
            {
                x.RegisterModule(new WebModule());
                x.RegisterModule(new ApplicationModule());
                x.RegisterModule(new InfrastructureModule());
            });

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            builder.Services.AddIdentity();

            builder.Services.AddNHibernate(connectionString);

            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var log = LogManager.GetLogger(typeof(Program));
            log.Info("Hello");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            await app.Services.GetService<ISeedService>()!.DataSeed();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}