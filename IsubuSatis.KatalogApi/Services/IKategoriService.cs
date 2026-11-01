using IsubuSatis.KatalogApi.Models.Dtos;

namespace IsubuSatis.KatalogApi.Services
{
    public interface IKategoriService
    {
        Task<List<KategoriDto>> GetKategoriler();

        Task CreateOrUpdateKategori(CreateOrUpdateKategoriDto input);

        Task DeleteKategori(string id);
    }
}
