using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptUncryptService
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
            services.AddControllersWithViews();
            services.AddSingleton<ICheckToken, CheckToken>();

            //    services.AddAuthentication(authOptions => //Bearer yöntemi ile
            //    {
            //        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //      .AddJwtBearer(jwtOptions =>
            //      {
            //          var key = Configuration.GetValue<string>("JwtConfig:Key");
            //          var keyBytes = Encoding.ASCII.GetBytes(key);

            //          jwtOptions.SaveToken = true;
            //          jwtOptions.TokenValidationParameters = new TokenValidationParameters
            //          {
            //              IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            //              ValidateLifetime = true,
            //              ValidateAudience = false, //token isteði yapabilecek servisler
            //              ValidateIssuer = false  //token'ý saðlayan servis 
            //          };
            //      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
