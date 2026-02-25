using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ITIRR.Web.Models.Auth;

namespace ITIRR.Web.Services
{
    public class ErrorsConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                return new List<string> { reader.GetString() ?? "" };

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = new List<string>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray) break;
                    if (reader.TokenType == JsonTokenType.String)
                        list.Add(reader.GetString() ?? "");
                }
                return list;
            }

            reader.Skip();
            return new List<string>();
        }

        public override void Write(Utf8JsonWriter writer,
            List<string> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
                writer.WriteStringValue(item);
            writer.WriteEndArray();
        }
    }

    public class AuthApiService : IAuthApiService
    {
        private readonly HttpClient _http;

        public AuthApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/v1/auth/login", new
                {
                    emailOrPhone = model.EmailOrPhone,
                    password = model.Password
                });

                var result = await response.Content
                    .ReadFromJsonAsync<ApiResponse<AuthResponseData>>();

                if (result?.Success == true && result.Data != null)
                    return new AuthResult
                    {
                        Success = true,
                        Message = result.Message,
                        AccessToken = result.Data.AccessToken,
                        RefreshToken = result.Data.RefreshToken,
                        User = result.Data.User
                    };

                return new AuthResult
                {
                    Success = false,
                    Message = result?.Message ?? "Login failed",
                    Errors = result?.Errors ?? new List<string>()
                };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/v1/auth/register-partner", new
                {
                    emailOrPhone = model.EmailOrPhone,
                    password = model.Password,
                    confirmPassword = model.ConfirmPassword,
                    agencyName = model.AgencyName,
                    contactType = model.ContactType
                });

                var rawJson = await response.Content.ReadAsStringAsync();

                var result = System.Text.Json.JsonSerializer.Deserialize<ApiResponse<RegisterResponseData>>(
                    rawJson,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return new AuthResult
                {
                    Success = result?.Success ?? false,
                    Message = result?.Message ?? "Registration failed",
                    Errors = result?.Errors ?? new List<string>()
                };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<AuthResult> VerifyOtpAsync(VerifyOtpModel model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/v1/auth/verify-otp", new
                {
                    emailOrPhone = model.EmailOrPhone,
                    otpCode = model.OtpCode
                });

                var rawJson = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"VerifyOTP Response: {rawJson}");

                var result = System.Text.Json.JsonSerializer.Deserialize<ApiResponse<AuthResponseData>>(
                    rawJson,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Success == true && result.Data != null)
                    return new AuthResult
                    {
                        Success = true,
                        Message = result.Message,
                        AccessToken = result.Data.AccessToken,
                        RefreshToken = result.Data.RefreshToken,
                        User = result.Data.User
                    };

                return new AuthResult
                {
                    Success = false,
                    Message = result?.Message ?? "OTP verification failed",
                    Errors = result?.Errors ?? new List<string>()
                };
            }
            catch (Exception ex)
            {
                return new AuthResult { Success = false, Message = ex.Message };
            }
        }
        private class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public T? Data { get; set; }

            [JsonConverter(typeof(ErrorsConverter))]
            public List<string> Errors { get; set; } = new();
        }

        private class AuthResponseData
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public UserInfo? User { get; set; }
        }

        private class RegisterResponseData
        {
            public string UserId { get; set; } = string.Empty;
            public bool OtpSent { get; set; }
        }
    }
}