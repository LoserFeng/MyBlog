using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders;
using Microsoft.OpenApi.Models;
using MyBlog.WebAPI.Extensions;
using MyBlog.WebAPI.Filters;
using SqlSugar;
using SqlSugar.IOC;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Utility._AutoMapper;

var builder = WebApplication.CreateBuilder(args);


#region 编码
// Add services to the container.
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

/*
 * 配置json中文乱码问题
 */


builder.Services.AddSession();

builder.Services.AddControllersWithViews().AddJsonOptions((options =>
{
    // 配置System.Text.Json 全局Json非英文字符
    //options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);//此配置部分字符无法原封显示
    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
}));


builder.Services.AddRazorPages(options =>
{
    options.Conventions
        .AddPageApplicationModelConvention("/StreamedSingleFileUploadDb",
            model =>
            {
                model.Filters.Add(
                    new GenerateAntiforgeryTokenCookieAttribute());
                model.Filters.Add(
                    new DisableFormValueModelBindingAttribute());
            });
    options.Conventions
        .AddPageApplicationModelConvention("/StreamedSingleFileUploadPhysical",
            model =>
            {
                model.Filters.Add(
                    new GenerateAntiforgeryTokenCookieAttribute());
                model.Filters.Add(
                    new DisableFormValueModelBindingAttribute());
            });
});


#endregion


builder.Services.AddMvc().AddRazorRuntimeCompilation(); 
builder.Services.AddEndpointsApiExplorer();

#region swagger JWT
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });

    #region Swagger使用鉴权组件
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
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
    #endregion
});
*/


#endregion

builder.Services.AddSwagger();




#region SqlSugarIOC
builder.Services.AddSqlSugar(new IocConfig()
{
    //ConnectionString = builder.Configuration.GetConnectionString("SugarConnectString"), //数据库连接串
    ConnectionString = builder.Configuration["ConnectionStrings:SugarConnectString"], 
    DbType = IocDbType.SqlServer,   //数据库类型
    IsAutoCloseConnection = true//自动释放
});


#endregion
//IOC注入sqlSugar



#region IOC依赖注入
builder.Services.AddCustomIOC();

#endregion

#region JWT鉴权注入
/*
builder.Services.AddCustomJWT();
*/
#endregion


#region AutoMapper注入

builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));


#endregion


builder.Services.AddSettings(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);   //配置授权信息的。
//builder.Services.AddHttpContextAccessor();    //Asp.NET core是默认不支持Session的，这个是用于开启Session的


  var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{

    /*    app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "api-docs/swagger";
            options.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin");
            options.SwaggerEndpoint("/swagger/blog/swagger.json", "Blog");
            options.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth");
            options.SwaggerEndpoint("/swagger/common/swagger.json", "Common");
            options.SwaggerEndpoint("/swagger/test/swagger.json", "Test");
        });*/

    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseAuthentication();  //用于鉴权 身份认证

app.UseAuthorization();  //用于授权 使用授权

app.UseSession();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();





app.MapControllers();
app.Run();



