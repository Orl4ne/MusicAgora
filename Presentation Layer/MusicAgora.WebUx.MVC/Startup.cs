using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Identity.DAL;
using Microsoft.AspNetCore.Identity.UI.Services;
using Library.DAL;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using Library.DAL.Repositories;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using Library.BLL.UseCases;

namespace MusicAgora.WebUx.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configuring Contexts
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("LibraryConnection")));

            services.AddControllersWithViews();
            services.AddRazorPages();

            //Configuring Identity
            services.AddIdentity<ApplicationUser, AccessRight>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                    .AddRoleManager<RoleManager<AccessRight>>()
                    .AddRoles<AccessRight>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddSignInManager()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            //Put dependency injections here
            services.AddTransient<ILibraryUnitOfWork, LibraryUnitOfWork>();
            services.AddTransient<IChiefUC, ChiefUC>();
            services.AddTransient<ILibrarianUC, LibrarianUC>();
            services.AddTransient<IMusicianUC, MusicianUC>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
