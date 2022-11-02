using Newtonsoft.Json;

namespace GameStore.Models.Recaptcha
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorMessages { get; set; }
    }
}
