using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Requests
{
    /// <summary>创建带参数的app临时二维码请求</summary>
    public record CreateQrcodeRequest
    {
        /// <summary>应用的apptoken</summary>
        [JsonPropertyName("appToken")]
        public string AppToken { get; set; }

        /// <summary>附带的数据</summary>
        [JsonPropertyName("extra")]
        public string ExtraData { get; set; }

        /// <summary>二维码有效时间，秒为单位，最大30天。</summary>
        [JsonPropertyName("validTime")]
        public int ValidTime { get; set; }
    }
}
