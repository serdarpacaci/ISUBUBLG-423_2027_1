using MongoDB.Bson.Serialization.Attributes;

namespace IsubuSatis.KatalogApi.Models.Dtos
{
    public class UrunDto
    {
        public string? Id { get; set; }

        public string Ad { get; set; }

        public decimal Fiyat { get; set; }

        public string KategoriId { get; set; }
        public string KategoriAd { get; set; }
    }
}
