using Microsoft.AspNetCore.Mvc;

namespace DexApi.Models.Request
{
    public class DexUploadRequest
    {
        [FromForm(Name = "file")]
        public IFormFile? File { get; set; }

        [FromForm(Name = "machine")]
        public string? Machine { get; set; }
    }
}
