

using FundooRepositoryLayer;
using FundooServiceLayer;
using FundooServiceLayer.CoundForImages;
using FundooServiceLayer.EmailService;
using FundooServiceLayer.MSMQService;
using FundooServiceLayer.TokenAuthentification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes
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
            EmailConfiguration emailConfig = Configuration
               .GetSection("EmailConfiguration")
               .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);

            var cloudConfigurations = Configuration.GetSection("Cloudinary").Get<CloudConfiguration>();
            services.AddSingleton(cloudConfigurations);
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<FundooDBContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("UserDbConnection")));
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IRegistrationService,RegistrationService>();
            services.AddScoped<ILableRepository,LableRepository>();
            services.AddScoped<ILableService,LableService>();
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<ICollaboratorService, CollaboratorService>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IMSMQ, MSMQ>();
            services.AddScoped<IMSMQForMail, MSMQForMail>();
            services.AddScoped<ICloudForImages, CouldForImages>();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379";
                option.InstanceName = "FundooNotes";
            });



            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowCredentials().AllowAnyMethod()
                ));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FundooApp API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert JWT token into field"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                });
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }


    }
}
