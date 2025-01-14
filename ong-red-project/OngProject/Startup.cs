using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OngProject.Core.Entities;
using OngProject.Core.Helper;
using OngProject.Core.Helper.Pagination;
using OngProject.Core.Interfaces.IServices;
using OngProject.Core.Interfaces.IServices.AWS;
using OngProject.Core.Services;
using OngProject.Core.Services.AWS;
using OngProject.Infrastructure.Data;
using OngProject.Infrastructure.Repositories;
using OngProject.Infrastructure.Repositories.IRepository;
using OngProject.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OngProject
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

            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<ApplicationDbContext>((services, options) =>
            {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnectionString"));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
               
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // JWT Token Generator
           

            //AWS S3 Configuration
            services.AddAWSService<IAmazonS3>();

            // Add Services
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IContactsServices, ContactsServices>();
            services.AddTransient<IMemberServices, MemberServices>();
            services.AddTransient<ISlidesServices, SlidesServices>();
            services.AddTransient<ICategoriesServices, CategoriesServices>();
            services.AddTransient<IOrganizationsServices, OrganizationsServices>();
            services.AddTransient<INewsServices, NewsServices>();
            services.AddTransient<IActivitiesServices, ActivitiesServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ICommentsServices, CommentsServices>();   
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ITestimonialsServices, TestimonialsServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSingleton<IUriService>(x =>
            {
                var accessor = x.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"));
            }
            //app.UseMiddleware<OwnerShipMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseMiddleware<AdminRoutesMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
