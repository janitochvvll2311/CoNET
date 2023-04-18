
using Microsoft.Extensions.Configuration;

namespace CoNet.Services.Server;

public class ServerService
{

    public string ServerRootPath { get; }
    public string ConfigFile { get; }
    public Dictionary<string, (DateTime date, IConfiguration config)> Configurations { get; }

    public IConfiguration? GetConfiguration(string path)
    {
        var configPath = Path.Combine(ServerRootPath, path, ConfigFile);
        if (File.Exists(configPath))
        {
            var date = File.GetLastWriteTime(configPath);
            var entry = Configurations.GetValueOrDefault(path);
            if (entry.config == null || entry.date < date)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(configPath);
                var configuration = builder.Build();
                entry = (date, configuration);
                Configurations[path] = entry;
            }
            return entry.config;
        }
        Configurations.Remove(path);
        return null;
    }

    public ServerService(string serverRootPath, string configFile = "config.json")
    {
        ServerRootPath = serverRootPath;
        ConfigFile = configFile;
        Configurations = new Dictionary<string, (DateTime date, IConfiguration config)>();
    }

}
