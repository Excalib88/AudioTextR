using Newtonsoft.Json;

namespace AudioTextR.Core.Abstractions.Models
{
    public class RecognizeResponse: IRecognizeResponse
    {
        [JsonProperty("_text")]
        public string Text { get; set; }
    }
}
