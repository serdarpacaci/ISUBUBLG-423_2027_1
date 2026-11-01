using IsubuSatis.KatalogApi.Mappers;
using IsubuSatis.KatalogApi.Models;
using IsubuSatis.KatalogApi.Models.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IsubuSatis.KatalogApi.Services
{
    public interface IUrunService
    {
        Task<List<UrunDto>> GetUrunler();

        Task CreateUrun(CreateUrunDto input);
        Task UpdateUrun(UpdateUrunDto input);

        Task DeleteUrun(string id);
    }

    public class UrunService : IUrunService
    {

        private readonly IMongoCollection<Urun> _urunCollection;
        private readonly UrunMapper _urunMapper;
        public UrunService(IOptions<MongoDbSettings> databaseSetting)
        {
            var settings = databaseSetting.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _urunCollection = database.GetCollection<Urun>(MongoDbCollections.UrunCollection);

            _urunMapper = new UrunMapper();
        }
        
        public async Task CreateUrun(CreateUrunDto input)
        {
            var urun = _urunMapper.CreateUrunDtoToUrun(input);
            urun.EklemeTarihi = DateTime.Now;

            await _urunCollection.InsertOneAsync(urun);
        }

        public async Task DeleteUrun(string id)
        {
            await _urunCollection
              .DeleteOneAsync(Builders<Urun>.Filter.Eq(x => x.Id, id));
        }

        public async Task<List<UrunDto>> GetUrunler()
        {
            var urunler = await _urunCollection.AsQueryable()
               .OrderBy(x => x.Ad)
               .ToListAsync();

            return urunler
                .Select(x => _urunMapper.UrunToUrunDto(x))
                .ToList();
        }

        public async Task UpdateUrun(UpdateUrunDto input)
        {
            var urun = await _urunCollection.AsQueryable()
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            if (urun is null)
                throw new Exception("Ürün bulunamadı");

            _urunMapper.UpdateUrun(input, urun);
            await _urunCollection
                .ReplaceOneAsync(Builders<Urun>.Filter.Eq(x => x.Id, input.Id), urun);
        }
    }
}
