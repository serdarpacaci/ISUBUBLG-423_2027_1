using MongoDB.Bson.Serialization.Attributes;

namespace IsubuSatis.KatalogApi.Models.Dtos
{
    public class CreateUrunDto
    {
        public string Ad { get; set; }

        public decimal Fiyat { get; set; }

        public string KategoriId { get; set; }
    }
}

