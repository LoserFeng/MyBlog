using SqlSugar.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
