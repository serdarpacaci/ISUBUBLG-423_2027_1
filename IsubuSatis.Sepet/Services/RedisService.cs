using IsubuSatis.Sepet.Models;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace IsubuSatis.Sepet.Services
{
    public class RedisService
    {
        private readonly string host;
        private readonly int port;

        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(IOptions<RedisSetting> options)
        {
            var setting = options.Value;
            host = setting.Host;
            port = setting.Port;

            Baglan();
        }

        private void Baglan()
        {
            var configuraton = $"{host}:{port}";
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuraton);
        }

        public IDatabase GetDatabase(int db = 1)
        {
            return _connectionMultiplexer.GetDatabase(db);
        }
    }
}
