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
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {

            //因为是Writer的用户登录功能的JWT，所以只需要注入WriterInfo即可
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();

            return services;
        }

    }
}
