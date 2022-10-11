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