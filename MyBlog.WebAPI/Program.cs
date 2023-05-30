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


#region ����
// Add services to the container.
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

/*
 * ����json������������
 */


builder.Services.AddSession();

builder.Services.AddControllersWithViews().AddJsonOptions((options =>
{
    // ����System.Text.Json ȫ��Json��Ӣ���ַ�
    //options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);//�����ò����ַ��޷�ԭ����ʾ
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

    #region Swaggerʹ�ü�Ȩ���
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
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
    //ConnectionString = builder.Configuration.GetConnectionString("SugarConnectString"), //���ݿ����Ӵ�
    ConnectionString = builder.Configuration["ConnectionStrings:SugarConnectString"], 
    DbType = IocDbType.SqlServer,   //���ݿ�����
    IsAutoCloseConnection = true//�Զ��ͷ�
});


#endregion
//IOCע��sqlSugar



#region IOC����ע��
builder.Services.AddCustomIOC();

#endregion

#region JWT��Ȩע��
/*
builder.Services.AddCustomJWT();
*/
#endregion


#region AutoMapperע��

builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));


#endregion


builder.Services.AddSettings(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);   //������Ȩ��Ϣ�ġ�
//builder.Services.AddHttpContextAccessor();    //Asp.NET core��Ĭ�ϲ�֧��Session�ģ���������ڿ���Session��


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


app.UseAuthentication();  //���ڼ�Ȩ �����֤

app.UseAuthorization();  //������Ȩ ʹ����Ȩ

app.UseSession();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();





app.MapControllers();
app.Run();



