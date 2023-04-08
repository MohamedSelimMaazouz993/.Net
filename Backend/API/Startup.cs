using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Service;
//using Microsoft.IdentityModel.Tokens;
using System;
//using Opw.HttpExceptions.AspNetCore.Mappers;

using Microsoft.AspNetCore.HttpOverrides;
using DAL.Config;
//using Opw.HttpExceptions.AspNetCore.Sample.CustomErrors;

namespace API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            // configuration du appsetting
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true,reloadOnChange: true);

            Environment = environment;

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
            // Serial logger
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region  DI
            services.Configure<DbContextSettings>(Configuration);
            services.AddSingleton(Log.Logger);
            services.AddService(Configuration);
            services.AddHttpClient();

            #endregion

            #region Google Authentification

            //services.AddAuthentication()
            //        .AddGoogle(options =>
            //        {
            //            IConfigurationSection googleAuthNSection =
            //                Configuration.GetSection("Authentication:Google");

            //            options.ClientId = googleAuthNSection["ClientId"];
            //            options.ClientSecret = googleAuthNSection["ClientSecret"];
            //        });

            //    services
            //.AddAuthentication(o =>
            //        {
            //            // This forces challenge results to be handled by Google OpenID Handler, so there's no
            //            // need to add an AccountController that emits challenges for Login.
            //            o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //            // This forces forbid results to be handled by Google OpenID Handler, which checks if
            //            // extra scopes are required and does automatic incremental auth.
            //            o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            //            // Default scheme that will handle everything else.
            //            // Once a user is authenticated, the OAuth2 token info is stored in cookies.
            //            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        })
            //.AddCookie()
            //.AddGoogleOpenIdConnect(options =>
            //        {
            //            IConfigurationSection googleAuthNSection =
            //                            Configuration.GetSection("Authentication:Google");
            //            options.ClientId = googleAuthNSection["ClientId"];
            //            options.ClientSecret = googleAuthNSection["ClientSecret"];
            //        });


            #endregion
            #region Validation du JWT
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //        .AddJwtBearer("Bearer", options =>
            //        {
            //            options.Authority = Configuration.GetValue<string>("Authserver:Authority");
            //            options.RequireHttpsMetadata = false;
            //            options.Audience = Configuration.GetValue<string>("Authserver:Audience");
            //            options.TokenValidationParameters = new
            //                TokenValidationParameters()
            //                {
            //                    ValidateAudience = false,
            //                    ValidateLifetime = true,
            //                    LifetimeValidator = (notBefore, expires, securityToken, validationParameter) =>
            //                            expires >= DateTime.UtcNow
            //                };

            //        });

            #endregion



            #region Cors Settings
            //services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
            //    builder
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    .WithOrigins("http://localhost:4200");
            //}));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    b => b.WithOrigins("*")
                        .WithHeaders("*")
                        .WithMethods("*")
                        .WithExposedHeaders("*"));
            });
            #endregion
            services.AddControllers().AddNewtonsoftJson(options =>
                          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseHttpExceptions();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
