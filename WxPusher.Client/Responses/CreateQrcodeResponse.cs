using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Responses
{
    /// <summary>创建带参数的app临时二维码的响应</summary>
    public record CreateQrcodeResponse
    {
        [JsonPropertyName("expires")]
        public long Expires { get; init; }

        [JsonPropertyName("code")]
        public string Code { get; init; }

        [JsonPropertyName("shortUrl")]
        public string ShortUrl{get;init; }

        [JsonPropertyName("url")]
        public string Url { get; init; }

        [JsonPropertyName("extra")]
        public string Extra { get; init; }
    }
}
