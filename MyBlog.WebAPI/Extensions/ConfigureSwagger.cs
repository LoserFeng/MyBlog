using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MyBlog.WebAPI.Extensions;

public static class ConfigureSwagger {
    public static void AddSwagger(this IServiceCollection services) {
        services.AddSwaggerGen(options => {
/*            options.SwaggerDoc("admin", new OpenApiInfo {
                Version = "v1",
                Title = "Admin APIs",
                Description = "管理员相关接口"
            });*/  //不知道干啥用的，加了就会报错

            // 开启小绿锁
            var security = new OpenApiSecurityScheme {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            };
            options.AddSecurityDefinition("Bearer", security);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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







            /* options.AddSecurityRequirement(new OpenApiSecurityRequirement { { security, new List<string>() } });
             options.OperationFilter<AddResponseHeadersFilter>();
             options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
             options.OperationFilter<SecurityRequirementsOperationFilter>();*/

            /*            // XML注释 
                        var filePath = Path.Combine(System.AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
                        options.IncludeXmlComments(filePath, true);*/
        });
    }
}