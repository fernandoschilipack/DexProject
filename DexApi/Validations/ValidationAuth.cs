namespace DexApi.Validations
{

    public static class ValidationAuth
    {
        public static bool IsAuthorized(HttpRequest request, IConfiguration config)
        {
            if (!request.Headers.TryGetValue("Authorization", out var header))
                return false;

            var expected = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{config["Auth:Username"]}:{config["Auth:Password"]}")
            );

            return header.ToString() == $"Basic {expected}";
        }
    }
}
