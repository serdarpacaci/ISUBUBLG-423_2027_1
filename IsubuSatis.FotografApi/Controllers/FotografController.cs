using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsubuSatis.FotografApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotografController : ControllerBase
    {
        private readonly string _fotografKlasorPath;
        private static List<string> _izinVerilenFormatlar = new List<string>
        {
            "image/png",
            "image/jpeg",
        };
        public FotografController()
        {
            _fotografKlasorPath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwRoot", "images");

            if (!Directory.Exists(_fotografKlasorPath))
            {
                Directory.CreateDirectory(_fotografKlasorPath);
            }
        }

        [HttpPost]
        public IActionResult Kaydet(IFormFile formFile)
        {
            (bool flowControl, IActionResult value) = CheckData(formFile);

            if (!flowControl)
                return value;

            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileName = Path.GetRandomFileName();
            fileName = fileName.Substring(0, fileName.Length - 4) + fileExtension;

            var fotografPath = Path.Combine(_fotografKlasorPath, fileName);

            using var stream = new FileStream(fotografPath, FileMode.Create);
            formFile.CopyTo(stream);
            stream.Close();

            return Ok(fileName);
        }

        //[HttpPost]
        //public async Task<IActionResult> Kaydet(IFormFile formFile, CancellationToken token)
        //{
        //    (bool flowControl, IActionResult value) = CheckData(formFile);

        //    if (!flowControl)
        //        return value;

        //    var fileExtension = Path.GetExtension(formFile.FileName);
        //    var fileName = Path.GetRandomFileName();
        //    fileName = fileName.Substring(0, fileName.Length - 4) + fileExtension;

        //    var fotografPath = Path.Combine(_fotografKlasorPath, fileName);

        //    using var stream = new FileStream(fotografPath, FileMode.Create);
        //    await formFile.CopyToAsync(stream);
        //    stream.Close();

        //    if (token.IsCancellationRequested && System.IO.File.Exists(fileName))
        //        System.IO.File.Delete(fileName);

        //    return Ok(fileName);
        //}

        [HttpDelete]
        public IActionResult Sil(string fileName)
        {
            var fotografPath = Path.Combine(_fotografKlasorPath, fileName);
            if (System.IO.File.Exists(fotografPath))
                System.IO.File.Delete(fotografPath);

            return Ok();
        }

        private (bool flowControl, IActionResult value) CheckData(IFormFile formFile)
        {
            if (formFile.Length == 0)
                return (flowControl: false, value: BadRequest("Dosya yüklenmemiş"));

            if (!_izinVerilenFormatlar.Any(x=> x == formFile.ContentType))
                return (flowControl: false, value: BadRequest("Dosya formatı uygun değil"));

            return (flowControl: true, value: null);
        }


    }
}
