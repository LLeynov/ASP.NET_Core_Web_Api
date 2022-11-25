using System.Net;
using System.Text;
using ASP.NET_Core_Web_Api.Models;
using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Models.Validators;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using ASP.NET_Core_Web_Api.Services.Impl.Grpc_Services;
using EmployeeService.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;


namespace ASP.NET_Core_Web_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Any, 5001, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                    listenOptions.UseHttps(@"C:\Sertificate\testcerf.pfx", "12345");
                });
            });

            #region Configure Services

            builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();

            #endregion

            #region Configure Logging

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });

            #endregion

            #region Configure gRPC

            builder.Services.AddGrpc();

            #endregion

            #region Configure Authenticate

            builder.Services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            #endregion

            #region Configure EF DBContext Service
            builder.Services.AddDbContext<EmployeeServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });


            #endregion


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My test Service", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme(Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseWhen(
                ctx => ctx.Request.ContentType != "application/grpc",
                builder =>
            {
                builder.UseHttpLogging();
            });

            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DictionariesService>();
            });

            app.Run();

        }
    }
}