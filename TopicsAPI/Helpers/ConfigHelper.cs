using Microsoft.Extensions.Configuration;
using System.IO;

public class ConfigHelper
{
    /// <summary>
    /// 取得 ConnectionStrings 底下資料
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetConnectionString(string name)
    {
        IConfiguration config = GetConfiguration();
        return config.GetConnectionString(name);
    }

    public static IConfiguration GetConfiguration()
    {
        IConfiguration config = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .Build();

        return config;
    }

}