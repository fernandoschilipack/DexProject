using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DexMAUI.Services
{
    public class DexUploadService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:5001/vdi-dex"; // add here the localhost

        public DexUploadService()
        {
            _httpClient = new HttpClient();

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("vendsys:NFsZGmHAGWJSZ#RuvdiV"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }

        /// <summary>
        /// Service to Send the Dex file to the server.
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="dexText"></param>
        /// <returns></returns>
        public async Task<string> SendDexAsync(string machine, string dexText)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(machine), "machine" },
                { new StringContent(dexText), "file", "report.dex" }
            };

            try
            {
                var response = await _httpClient.PostAsync(ApiUrl, content);
                var body = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? $"Success: {body}" : $"Error: {body}";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }

}
