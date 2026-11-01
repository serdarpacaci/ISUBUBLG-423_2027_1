using IsubuSatis.KatalogApi.Models;
using IsubuSatis.KatalogApi.Models.Dtos;
using Riok.Mapperly.Abstractions;

namespace IsubuSatis.KatalogApi.Mappers
{
    [Mapper]
    public partial class KategoriMapper
    {
        public partial KategoriDto KategoriToKategoriDto(Kategori kategori);

        public partial Kategori CreateOrUpdateKategoriToKategori(CreateOrUpdateKategoriDto input);
        public partial void UpdateKategori(CreateOrUpdateKategoriDto kategorito, Kategori kategori);
    }


    [Mapper]
    public partial class UrunMapper
    {
        public partial UrunDto UrunToUrunDto(Urun urun);

        public partial Urun CreateUrunDtoToUrun(CreateUrunDto input);
        public partial void UpdateUrun(UpdateUrunDto urunDto, Urun urun);
    }
}
