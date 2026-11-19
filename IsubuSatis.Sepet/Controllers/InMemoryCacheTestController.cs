using IsubuSatis.Sepet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace IsubuSatis.Sepet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InMemoryCacheTestController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheTestController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetCacheTest()
        {
            var dersListesi =  await _memoryCache.GetOrCreateAsync("dersler", async x =>
            {
                //x.AbsoluteExpiration = DateTime.Now.AddSeconds(15);
                x.SlidingExpiration = TimeSpan.FromSeconds(10);
                var dersler = await GetDersler();
                return dersler;
            });

            return Ok(dersListesi);
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
