using IsubuSatis.KatalogApi.Models.Dtos;
using IsubuSatis.KatalogApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IsubuSatis.KatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IKategoriService _kategoriService;
        private readonly IUrunService _urunService;
        public UrunController(IKategoriService kategoriService,
            IUrunService urunService)
        {
            _kategoriService = kategoriService;
            _urunService = urunService;
        }

        [HttpGet]
        public async Task<List<UrunDto>> GetUrunler()
        {
            return await _urunService.GetUrunler();
        }

        [HttpPost]
        public async Task CreateUrun(CreateUrunDto input)
        {
            await _urunService.CreateUrun(input);
        }

        [HttpPut]
        public async Task UpdateUrun(UpdateUrunDto input)
        {
            await _urunService.UpdateUrun(input);
        }

        [HttpDelete]
        public async Task Delete(string id)
        {
            await _urunService.DeleteUrun(id);
        }
    }
}
