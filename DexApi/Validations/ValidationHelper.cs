using DexApi.Models.Request;

namespace DexApi.Validations
{
    /// <summary>
    /// Simply validation helper for DEX file uploads.
    /// </summary>
    public class ValidationHelper
    {
        public static object? ValidateUpload(DexUploadRequest request)
        {
            if (request.File is null || request.File.Length == 0)
            {
                return new
                {
                    status = "ERROR",
                    body = new { message = "file parameter is required" }
                };
            }

            if (request.Machine is null)
            {
                return new
                {
                    status = "ERROR",
                    body = new { message = "the field 'machine' is required" }
                };
            }

            if (request.Machine is not ("A" or "B"))
            {
                return new
                {
                    status = "ERROR",
                    body = new { message = "the field 'machine' only accepts A or B" }
                };
            }

            return null;
        }
    }
}
