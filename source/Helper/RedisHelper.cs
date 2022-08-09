using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace SpareManagement.Helper
{
    public class RedisHelper
    {
        private static string _redisConnectionString = string.Empty;
        private static ConnectionMultiplexer _redis;

        public RedisHelper(IConfiguration configuration)
        {
            //_cacheService = new CSRedis.CSRedisClient($"{configuration.GetValue<string>("Redis:ConnectionString")}" +
            //    $"{configuration.GetValue<string>("Redis:OptionalSetting")}");
            //_expireSeconds = configuration.GetValue<int>("Redis:DefaultTimeOut");
            _redisConnectionString = configuration.GetSection("Redis").GetValue<string>("ConnectionString");
            _redis = ConnectionMultiplexer.Connect(_redisConnectionString);
        }
    }
}
