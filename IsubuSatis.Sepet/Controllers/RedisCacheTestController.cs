using IsubuSatis.Sepet.Models;
using IsubuSatis.Sepet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IsubuSatis.Sepet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheTestController : ControllerBase
    {
        private readonly RedisService _redisService;

        public RedisCacheTestController(RedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestData()
        {
            var database = _redisService.GetDatabase();
            var dersler = await database.StringGetAsync("dersler");
            return Ok(JsonSerializer.Deserialize<List<Ders>>(dersler)); 
        }

        [HttpPost]
        public async Task<IActionResult> SetTestData(List<Ders> dersler)
        {
            var database = _redisService.GetDatabase();
            var dersJson = JsonSerializer.Serialize(dersler);
            await database.StringSetAsync("dersler", dersJson);

            return Ok();
        }


        private async Task<List<Ders>> GetDersler()
        {
            await Task.Delay(5000);

            var ders = new List<Ders>()
            {
                new Ders (){ Id = 1, Ad = "Sql"},
                new Ders (){ Id = 2, Ad = ".NET"},
                new Ders (){ Id = 3, Ad = "Network"},
            };

            return ders;
        }
    }
}
