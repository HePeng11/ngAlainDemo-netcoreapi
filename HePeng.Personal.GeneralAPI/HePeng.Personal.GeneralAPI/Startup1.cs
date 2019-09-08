using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Common.Jwt;
using HePeng.Personal.Common.ApiExts;
using HePeng.Personal.Common.APIExts;
using HePeng.Personal.Common.Consts;
using HePeng.Personal.Common.CoreHelpers;
using HePeng.Personal.Common.Dtos;
using HePeng.Personal.GeneralAPI.Other;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace HePeng.Personal.GeneralAPI
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup1
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup1(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;// Program 中CreateDefaultBuilder=》ConfigureAppConfiguration已经创建

            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(env.ContentRootPath)
            //   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //this.Configuration = builder.Build();

            APIAppsetting = Configuration.Get<APIAppsetting>();
            //Console.WriteLine(Configuration["Logging:LogLevel:Default"]);
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configuration 转换得到的配置对象 （注意运行时不会随着Configuration更改）
        /// </summary>
        public APIAppsetting APIAppsetting { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// 第一次请求时配置各个实例对象（bean）
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                //config.Filters.Add<SysExceptionFilter>(); //暂不使用此系统异常过滤器 已使用了异常中间件
            }).AddJsonOptions(o =>
            {
                o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //o.SerializerSettings.ContractResolver //可设置返回json的大小写 默认小写
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MvcOptions>(options =>
            {
                //给全局路由添加统一前缀
                options.Conventions.Insert(0, new RouteConvention1("services/v1/"));
            }).AddRouting(o =>
            {
                o.LowercaseUrls = true;
            });


            #region swagger
            services.AddSwaggerGen(c =>
            {
                //文档左上角的描述
                var swaggerInfo = new Info
                {
                    Version = "v1.0.0",
                    Title = "hepeng's dotnetcore test",
                    Description = "路漫漫其修远兮 吾将上下而求索<br />愿你出走半生 归来仍是少年",
                    TermsOfService = "http://www.baidu.com",
                    License = new License() { Name = "license", Url = "http://www.baidu.com" },
                    Contact = new Contact() { Name = "hepeng", Email = "914535402@qq.com", Url = "https://www.cnblogs.com/hepeng/" }
                };
                c.SwaggerDoc("v1", swaggerInfo);
                //读取注释用于显示
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + "HePeng.Personal.GeneralAPI.xml", true);

                //在swagger中显示JWT信息
                var security = new Dictionary<string, IEnumerable<string>> { { JwtBearerDefaults.AuthenticationScheme, new string[] { "test1", "test2" } } };
                c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });

            });
            #endregion

            #region 认证 （与授权配合使用）
            services.AddAuthentication(x =>
            {
                //bearer “持票人”约定俗成
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                //JwtAuthConfigModel jwtConfig = new JwtAuthConfigModel();
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "HP.GeneralAPI",//发行人 需要和token中保持一致
                    ValidAudience = "wr",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(APIAppsetting.JWTSecretKey)),

                    /***********************************TokenValidationParameters的参数默认值***********************************/
                    RequireSignedTokens = true,
                    // SaveSigninToken = false,
                    // ValidateActor = false,
                    // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    // 是否要求Token的Claims中必须包含 Expires
                    RequireExpirationTime = true,
                    // 允许的服务器时间偏移量
                    // ClockSkew = TimeSpan.FromSeconds(300),
                    // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                    ValidateLifetime = true
                };
            });
            #endregion

            #region 授权
            services.AddAuthorization(options =>
            {
                //此处与控制器中的[Authorize(Roles = "Admin,hepeng")]对应
                //options.AddPolicy("RequireClient", policy => policy.RequireRole("Client").Build());
                //options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin").Build());
                //options.AddPolicy("RequireAdminOrClient", policy => policy.RequireRole("Admin,Client").Build());
            });
            #endregion

            #region CORS 启用跨域请求
            //同源三要素: 协议 域名 端口  不同的资源的这三个要素同时相同才叫同源
            //https://i.cnblogs.com/EditLinks.aspx?catid=1357952
            services.AddCors(c =>
            {
                //添加策略
                //此处与控制器中的[EnableCors(CommonConst.CORSPolicyName)]对应
                c.AddPolicy(CommonConst.CORSPolicyName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();//.AllowCredentials();
                });

                //c.AddPolicy("Limit", policy =>
                //{
                //    policy
                //    .WithOrigins("localhost:8083")
                //    .WithMethods("get", "post", "put", "delete")
                //    //.WithHeaders("Authorization");
                //    .AllowAnyHeader();
                //});
            });
            #endregion

            services.AddMemoryCache();
            services.AddSingleton(APIAppsetting);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AclAutherizationFilter.AclAutherationImpl>();
            services.AddScoped<SysExceptionFilter.SysExceptionFilterImpl>();
            InjectServices(services);


        }

        /// <summary>
        /// This method configure gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage(); //实际上添加了一个返回错误页面的中间件 巨坑 导致了自定义的异常中间件无法执行
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            //ILogger<Startup1> logger = loggerFactory.CreateLogger<Startup1>();
            //添加中间件的顺序就是客户端发起http请求时所经过的顺序
            //认证
            app.UseAuthentication();


            //授权
            app.UseMiddleware<JwtAuthorizationMiddleware>();
            //app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles(); // For the wwwroot folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.ContentRootPath + "/assets"),
                OnPrepareResponse = (c) =>
                {
                    c.Context.Response.AllowCors();
                },
                RequestPath = "/assets"
            });

            //app.UseHsts();
            #region swagger
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "api文档 v1");
            });
            #endregion
            ServiceLocator.Instance = app.ApplicationServices;

            //JWT规则 由三部分组成：Header（头部）、Payload（数据）、Signature（签名），将这三部分由‘.’连接而组成字符串然后加密就成为JWT字符串
            //Header:由且只由两个数据组成，一个是“alg”（加密规范）指定了该JWT字符串的加密规则，另一个是“typ”(JWT字符串类型) 列如：{"alg": "HS256","typ": "JWT"}
            //Payload（数据）:由一组数据组成，它负责传递数据，一般是发起请求的用户的信息
            //Signature（签名）：由4个因素所同时决定：编码后的header字符串，编码后的payload字符串，之前在头部声明的加密算法，我们自定义的一个秘钥字符串（secret）
            //列如：HMACSHA256( base64UrlEncode(header) + "." + base64UrlEncode(payload), secret)

            //“令牌”指的是用于http传输headers中用于验证授权的JSON数据，它是key和value两部分组成
            //key为“Authorization”，value为“Bearer {JWT字符串}”，其中value除了JWT字符串外，还在前面添加了“Bearer ”字符串，这里可以把它理解为大家约定俗成的规定即可，没有实际的作用
        }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        private void InjectServices(IServiceCollection services)
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var assemblyNames = files.Select(f =>
            {
                return Assembly.LoadFile(f);
            }).ToList();
            List<Type> interfaceDefinitions = new List<Type>();
            List<Type> interfaceImplements = new List<Type>();
            for (int i = 0; i < assemblyNames.Count; i++)
            {
                var assembly = Assembly.Load(assemblyNames[i].FullName);
                var iServices = assembly.GetTypes().Where(t => t.IsInterface && typeof(IInjectable).IsAssignableFrom(t) && t != typeof(IInjectable) && t != typeof(ISingletonInject) && t != typeof(IScopeInject) && t != typeof(ITransentInject)).ToList();
                var serviceImps = assembly.GetTypes().Where(t => t.IsClass && t.GetInterfaces().ToList().Exists(d => d == typeof(IInjectable))).ToList();
                interfaceDefinitions.AddRange(iServices);
                interfaceImplements.AddRange(serviceImps);
            }
            interfaceDefinitions.ForEach(iServiceType =>
            {
                var implements = interfaceImplements.Where(f => f.GetInterfaces().Contains(iServiceType)).ToList();
                if (implements.Count != 1)
                {
                    throw new NotImplementedException($"not implement interface {iServiceType.FullName} or more than one implement");
                }
                var implement = implements.First();
                if (typeof(ISingletonInject).IsAssignableFrom(iServiceType))
                {
                    services.AddSingleton(iServiceType, implement);
                }
                if (typeof(IScopeInject).IsAssignableFrom(iServiceType))
                {
                    services.AddScoped(iServiceType, implement);
                }
                if (typeof(ITransentInject).IsAssignableFrom(iServiceType))
                {
                    services.AddTransient(iServiceType, implement);
                }

            });
        }
    }
}