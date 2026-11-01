using MongoDB.Bson.Serialization.Attributes;

namespace IsubuSatis.KatalogApi.Models
{
    public class Urun
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Ad { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
        public decimal Fiyat { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime EklemeTarihi { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string KategoriId { get; set; }

        public Kategori Kategori { get; set; }
    }

}
