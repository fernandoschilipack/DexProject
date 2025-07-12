using Azure.Core;
using DexApi.Models.Request;
using DexApi.Models.Response;
using DexApi.Services;
using DexApi.Validations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace DexApi.Controllers;

/// <summary>
/// Minimal API controller for handling DEX file uploads.
/// </summary>
public static class DexController
{
    /// <summary>
    /// Method to handle the upload of a DEX file.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="config"></param>
    /// <param name="file"></param>
    /// <param name="machine"></param>
    /// <param name="dexService"></param>
    /// <returns></returns>
    [SwaggerOperation(
       Summary = "Upload and process a DEX file",
       Description = "Requires basic auth. Accepts a machine (A/B) and a .dex text file. Processes and stores the data.",
       Tags = new[] { "DEX API" }
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "File processed", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed", typeof(ApiResponse))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
    public static async Task<IResult> HandleUpload(
        [FromForm] DexUploadRequest dexuploadrequest,
        HttpRequest request,
        IConfiguration config,
        DexService dexService)
    {
        if (!ValidationAuth.IsAuthorized(request, config))
        {
            return Results.Json(new
            {
                status = "ERROR",
                body = new { message = "Username or password incorrect." }
            }, statusCode: StatusCodes.Status401Unauthorized);
        }
        // Validation for the request parameters    
        var validationResult = ValidationHelper.ValidateUpload(dexuploadrequest);
        if (validationResult is not null)
            return Results.BadRequest(validationResult);

        // Read file content
        using var reader = new StreamReader(dexuploadrequest?.File?.OpenReadStream()!);
        var dexText = await reader.ReadToEndAsync();

        // Process and persist
        await dexService.ProcessDexAsync(dexText, dexuploadrequest?.Machine!);

        // If all is well Return a 200 OK response
        return Results.Json(new
        {
            status = "SUCCESS",
            body = new { message = $"DEX File report {dexuploadrequest?.Machine} has been executed." }
        }, statusCode: StatusCodes.Status200OK);
    }


}
