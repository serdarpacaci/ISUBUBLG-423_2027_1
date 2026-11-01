using IsubuSatis.KatalogApi.Models.Dtos;
using IsubuSatis.KatalogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsubuSatis.KatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriService _kategoriService;
        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        [HttpGet]
        public async Task<List<KategoriDto>> GetKategoriler()
        {
            return await _kategoriService.GetKategoriler();
        }

        [HttpPost]
        public async Task CreateOrUpdate(CreateOrUpdateKategoriDto input)
        {
            await _kategoriService.CreateOrUpdateKategori(input);
        }

        [HttpDelete]
        public async Task Delete(string id)
        {
            await _kategoriService.DeleteKategori(id);
        }
    }
}
