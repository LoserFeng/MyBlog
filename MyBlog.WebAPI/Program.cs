using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SqlSugar;
using SqlSugar.IOC;
using Utility._AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

builder.Services.AddCustomJWT();
#endregion


#region AutoMapperע��

builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));


#endregion






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();  //���ڼ�Ȩ �����֤

app.UseAuthorization();  //������Ȩ ʹ����Ȩ



app.MapControllers();

app.Run();



