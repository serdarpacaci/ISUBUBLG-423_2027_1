using IsubuSatis.KatalogApi.Mappers;
using IsubuSatis.KatalogApi.Models;
using IsubuSatis.KatalogApi.Models.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IsubuSatis.KatalogApi.Services
{
    public class KategoriService : IKategoriService
    {
        private readonly IMongoCollection<Kategori> _kategoriCollection;
        private readonly KategoriMapper _kategoriMapper;
        public KategoriService(IOptions<MongoDbSettings> databaseSetting)
        {
            var settings = databaseSetting.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);
            _kategoriCollection = database.GetCollection<Kategori>(MongoDbCollections.KategoriCollection);

            _kategoriMapper = new KategoriMapper();
        }

        public async Task CreateOrUpdateKategori(CreateOrUpdateKategoriDto input)
        {
            if (string.IsNullOrEmpty(input.Id))
            {
                await CreateKategori(input);
            }
            else
            {
                await UpdateKategori(input);
            }
        }

        private async Task UpdateKategori(CreateOrUpdateKategoriDto input)
        {
            var kategori = await _kategoriCollection.AsQueryable()
                .Where(x => x.Id == input.Id)
                .FirstOrDefaultAsync();

            if (kategori is null)
                throw new Exception("Kategori bulunamadı");

            //kategori.Ad = input.Ad;
            _kategoriMapper.UpdateKategori(input, kategori);
            await _kategoriCollection
                .ReplaceOneAsync(Builders<Kategori>.Filter.Eq(x => x.Id, input.Id), kategori);
            
        }

        private async Task CreateKategori(CreateOrUpdateKategoriDto input)
        {
            var kategori = _kategoriMapper.CreateOrUpdateKategoriToKategori(input);

            await _kategoriCollection.InsertOneAsync(kategori);
        }

        public async Task DeleteKategori(string id)
        {
            await _kategoriCollection
                .DeleteOneAsync(Builders<Kategori>.Filter.Eq(x => x.Id, id));
        }

        public async Task<List<KategoriDto>> GetKategoriler()
        {
            var kategoriler = await _kategoriCollection.AsQueryable()
                .OrderBy(x=> x.Ad)
                .ToListAsync();

            return kategoriler
                .Select(x => _kategoriMapper.KategoriToKategoriDto(x))
                .ToList();
        }
    }
}
