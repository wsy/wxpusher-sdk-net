using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public record Message
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("appToken")]
        public required string AppToken { get; init; }

        /// <summary>发送的目标</summary>
        [JsonPropertyName("uids")]
        public HashSet<string> UserIds { get; set; } = new();

        [JsonPropertyName("topicIds")]
        public HashSet<long> TopicIds { get; set; } = new();

        [JsonPropertyName("contentType")]
        public MessageContentTypes ContentType { get; set; } = MessageContentTypes.Text;

        /// <summary>消息内容，只需要 body 标签内部的内容</summary>
        [JsonPropertyName("content")]
        public required string Content { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        /// <summary>URL，仅针对text消息类型有效</summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }


    }

    /// <summary>
    /// 消息格式
    /// </summary>
    public enum MessageContentTypes : int
    {
        /// <summary>Text，可以直接显示在卡片里面</summary>
        Text = 1,
        /// <summary>Html，点击以后查看，支持html</summary>
        Html = 2,
        /// <summary>Markdown格式，和html类似</summary>
        Markdown = 3,
    }
}
