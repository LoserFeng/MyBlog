# 前言

这个项目时源自于[个人博客项目介绍_哔哩哔哩_bilibili](https://www.bilibili.com/video/BV1iV411i7oH?p=1&vd_source=39de5ac609bd713fda33f767041c4d10)  的一个项目，github源地址为 https://github.com/1397771033/MyBlog，主要是想要通过ASP.NET Core来学习一下网页制作的框架以及一些编程的知识，提升自身对新事务的认知和对以往数据库等知识理解的加深。





# 项目架构

最开始的时候生成的是一个ASP.NET Core 6的项目，注意不是ASP.NET Framework



## 配置项目

由于用的是.NET6 所以只有一个Program.cs，说实话这个东西真的有点空。。。这也是程序的入口。



* 注入sqlSugar

~~~C#
#region SqlSugarIOC
    
builder.Services.AddSqlSugar(new IocConfig()
{
    //ConnectionString = builder.Configuration.GetConnectionString("SugarConnectString"), //数据库连接串
    ConnectionString = builder.Configuration["ConnectionStrings:SugarConnectString"],  //这个是引入appsettings.json里面的配置
    DbType = IocDbType.SqlServer,   //数据库类型
    IsAutoCloseConnection = true//自动释放
});


#endregion
//IOC注入sqlSugar

~~~



* 注入仓储层以及服务层（另外建一个类IOCExtend.cs)

~~~C#
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using System.Runtime.CompilerServices;  



//把仓储层和服务层的方法注入进来
//这个命名空间是从builder.Services.AddSwaggerGen();这里抄的。。。。
namespace Microsoft.Extensions.DependencyInjection
{
    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)   //扩展方法
        {

            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();

            return services;
        }

    }
}

~~~





Program.cs

~~~C#
builder.Services.AddCustomIOC();

~~~













# 数据库

本项目的数据库使用的是SqlSugar，使用以及配置过程可以见源码，要是未来有空的话可以整理一下，SqlSugar是 类似EF的存在  ，就是一个ORM。

## 数据库设计

# 2.数据库设计

文章表

```sql
ID
文章标题
文章内容
创建时间
文章类型ID
浏览量
点赞量
作者ID
```

文章类型表

```sql
ID
类型名
```

作者表

```sql
ID
姓名
账号
密码 MD5
```









# 控制器

控制器不写逻辑，可以理解成View 和 Service的接口，有承上启下的作用_(:з」∠)_。





# 服务层

由于服务层基本和仓储层没啥差别，就是在仓储层外嵌套了一层的感觉。 被控制器调用的时候是通过IOC注入的方式，好像_ (:з」∠) _   。





# Model层

数据库模型，是信息系统的最核心的部分。

# 仓储层

用于对接数据库的东西，创建的时候会对数据库进行创建，所以创建完之后，就可以把创建数据库的那段代码给注释掉了。

















# 工具

## MD5

用于加密密码，之后再次需要密码的时候，只需要再次比对加密后的密码和数据库存放的加密密码是否相同即可。

~~~C#
using System.Security.Cryptography;
using System.Text;

namespace MyBlog.WebAPI.Utility._MD5
{
    public class MD5Helper
    {
        public static string MD5Encrypt32(string password)
        {
            string pwd = " ";
            MD5 md5 = MD5.Create();
            byte[]s =md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for(int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}

~~~









## JWT

### JWT授权

1.添加一个webapi项目

2.安装Nuget程序包 System.IdentityModel.Tokens.Jwt 

```C#
var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "张三")
            };
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));
//issuer代表颁发Token的Web应用程序，audience是Token的受理者
var token = new JwtSecurityToken(
    issuer: "http://localhost:6060",
    audience: "http://localhost:5000",
    claims: claims,
    notBefore: DateTime.Now,
    expires: DateTime.Now.AddHours(1),
    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
);
var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
return jwtToken;
```

### JWT鉴权

安装Microsoft.AspNetCore.Authentication.JwtBearer

```C#
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:6060",
                ValidateAudience = true,
                ValidAudience = "http://localhost:5000",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(60)
              };
            });
```

### JWT授权鉴权使用

Swagger想要使用鉴权需要注册服务的时候添加以下代码

```C#
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In=ParameterLocation.Header,
          Type=SecuritySchemeType.ApiKey,
          Description= "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
          Name="Authorization",
          BearerFormat="JWT",
          Scheme="Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
```



遇到的问题：

[(2条消息) .Net Core 6 web api jwt 一直 401 Postman返回WWW-Authenticate Bearer没有其他提示的解决方法_シ゛甜虾的博客-CSDN博客](https://blog.csdn.net/g313105910/article/details/120883318)



切记这个顺序

![image-20220911204819789](E:\develop\VS_Workspace\CSharp\MyBlog\图片\image-20220911204819789.png)











## AutoMapper









