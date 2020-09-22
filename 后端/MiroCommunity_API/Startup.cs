using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace MiroCommunity_API
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        private static string basePath => AppContext.BaseDirectory;

        /// <summary>
        ///
        /// </summary>
        private readonly IHostEnvironment _env;

        /// <summary>
        ///
        /// </summary>
        public readonly IConfiguration Configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AssemblyName = Configuration["AssemblyName"].ToString();
        }

        /// <summary>
        /// 配置
        /// </summary>

        /// <summary>
        /// 当前程序集名称
        /// </summary>
        public readonly string AssemblyName;

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                o.Filters.Add<GlobalExceptionFilter>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiroCommunity_API", Version = "v1" });
                var files = Directory.GetFiles(basePath, "*.xml");
                foreach (var item in files)
                {
                    if (item.Contains(AssemblyName))
                        c.IncludeXmlComments(Path.Combine(basePath, item));
                }
            });

            #region JWT

            services.Configure<JwtConfig>(Configuration.GetSection("jwtconfig"));
            var jwtConfig = Configuration.GetSection("jwtconfig").Get<JwtConfig>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            var payload = JsonConvert.SerializeObject(new { code = "401", message = "身份验证失败" });

                            //自定义返回的数据类型
                            context.Response.ContentType = "application/json";
                            //自定义返回状态码，默认为401 我这里改成 200
                            context.Response.StatusCode = StatusCodes.Status200OK;
                            //输出Json数据结果
                            context.Response.WriteAsync(payload);
                            return Task.FromResult(0);
                        }
                    };
                });

            #endregion JWT
        }

        /// <summary>
        /// 管道配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }
    }
}