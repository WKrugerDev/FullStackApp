using System.Text.Json;
using ClientApp.Models;

namespace ClientApp.Services
{
    // Service responsible for fetching and caching product data from the back-end API
    // Registered as a scoped service in Program.cs via dependency injection
    public class ProductService
    {
        private readonly HttpClient _http;

        // In-memory cache to store products after the first API call
        // Prevents redundant API calls on subsequent component renders
        private List<Product>? _cachedProducts;

        // HttpClient injected via constructor - configured with BaseAddress in Program.cs
        // In production this would use a typed or named client for the specific API
        public ProductService(HttpClient http)
        {
            _http = http;
        }

        // Fetches product list from the API, returning cached data if available
        // In production this would include cache expiry and invalidation strategies
        public async Task<List<Product>?> GetProductsAsync()
        {
            // Return cached data if already fetched, avoiding unnecessary API calls
            if (_cachedProducts != null)
                return _cachedProducts;

            // Configure deserializer to handle camelCase JSON from API matching
            // PascalCase C# model properties
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Fetch raw response from API endpoint
            var response = await _http.GetAsync("http://localhost:5267/api/productlist");

            // Throws HttpRequestException if response status is not successful (non 2xx)
            response.EnsureSuccessStatusCode();

            // Read response body as raw JSON string
            var json = await response.Content.ReadAsStringAsync();

            // Deserialize JSON into strongly typed list and cache for subsequent calls
            _cachedProducts = JsonSerializer.Deserialize<List<Product>>(json, options);
            return _cachedProducts;
        }
    }
}