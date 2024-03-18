using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Responses
{
    public record MessageResponse
    {
        [JsonPropertyName("uid")]
        public string? UserId { get; init; }
        [JsonPropertyName("topicId")]
        public long? TopicId { get; init; }
        [JsonPropertyName("status")]
        public string Status { get; init; }
        [JsonPropertyName("code")]
        public int Code { get; init; }
        [JsonPropertyName("messageId")]
        public long MessageId { get; init; }
        [JsonPropertyName("sendRecordId")]
        public long SendRecordId { get; init; }
        [JsonPropertyName("messageContentId")]
        public long MessageContentId { get; init; }

        /// <summary>消息是否发送成功</summary>
        public bool Success => Code == (int)ResultCodes.SUCCESS;
    }
}
