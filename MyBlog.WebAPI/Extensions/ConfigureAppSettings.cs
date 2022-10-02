using MyBlog.Model.Config;

namespace MyBlog.WebAPI.Extensions; 

public static class ConfigureAppSettings {
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration) {
        // 安全配置,把appsettings中的安全配置信息提取出来
        services.Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)));
    }
}