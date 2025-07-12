namespace DexApi.Models.Response
{
    /// <summary>
    /// Used to describe Swagger response format
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// SUCCESS
        /// </summary>
        public string status { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public object body { get; set; } = new();
    }
}
