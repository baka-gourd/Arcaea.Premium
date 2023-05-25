using EasyCaching.Core;
using EasyCaching.Disk;
using EasyCaching.Serialization.SystemTextJson.Configurations;

namespace Arcaea.Premium;

public static class Cache
{
    private static IServiceCollection? _services;
    private static IEasyCachingProviderFactory? _factory;
    private static IEasyCachingProvider? _diskCache;

    public static void InitCache()
    {
        _services = new ServiceCollection();
        _services.AddEasyCaching(option =>
        {
            option.UseDisk(cfg =>
            {
                cfg.DBConfig = new DiskDbOptions { BasePath = $"{FileSystem.CacheDirectory}/cache" };
                cfg.SerializerName = "json";
            }, "disk")
                .WithSystemTextJson();
        });

        var provider = _services.BuildServiceProvider();
        _factory = provider.GetService<IEasyCachingProviderFactory>();
        _diskCache = _factory!.GetCachingProvider("disk");
    }

    public static T? GetOrAdd<T>(string key, T? value = default)
    {
        if (_diskCache is null)
        {
            throw new NullReferenceException("Cache must be initialized.");
        }

        var data = _diskCache.Get<T>(key);

        if (data.HasValue && !data.IsNull)
        {
            return data.Value;
        }

        if (value is not null)
        {
            _diskCache.Set(key, value, TimeSpan.FromDays(64));
            return value;
        }

        return default;
    }
}