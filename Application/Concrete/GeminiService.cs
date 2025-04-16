using Application.Abstract;
using Application.GeminiSettings;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly Gemini _settings;

        public GeminiService(HttpClient httpClient, IOptions<Gemini> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        public async Task<string> SuggestContinuationAsync(string input, string currentContent = "")
        {
            try
            {
                string promptText = string.IsNullOrEmpty(currentContent)
                    ? input
                    : $"Mevcut blog içeriği:\n\n{currentContent}\n\nKullanıcı isteği: {input}";

                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            role = "user",
                            parts = new[]
                            {
                                new { text = promptText }
                            }
                        }
                    },
                    generationConfig = new
                    {
                        temperature = 0.7,
                        maxOutputTokens = 800
                    }
                };

                var jsonContent = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.ModelName}:generateContent?key={_settings.ApiKey}")
                {
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"API hatası: {response.StatusCode} - {responseContent}";
                }

                using var doc = JsonDocument.Parse(responseContent);
                var candidatesElement = doc.RootElement.GetProperty("candidates");

                if (candidatesElement.GetArrayLength() == 0)
                    return "API yanıt verdi ancak içerik üretemedi.";

                var contentElement = candidatesElement[0].GetProperty("content");
                var partsElement = contentElement.GetProperty("parts");

                if (partsElement.GetArrayLength() == 0)
                    return "API yanıt verdi ancak boş içerik döndü.";

                return partsElement[0].GetProperty("text").GetString();
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }

        public void ClearConversationHistory()
        {
            // Şu an için gereksiz
        }
    }
}
