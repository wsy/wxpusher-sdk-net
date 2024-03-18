using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Responses
{
    /// <summary>分页数据</summary>
    /// <typeparam name="T"></typeparam>
    public record PageResponse<T>
    {
        [JsonPropertyName("total")]
        public int Total { get; init; }
        [JsonPropertyName("page")]
        public int Page { get; init; }
        [JsonPropertyName("pageSize")]
        public int PageSize { get; init; }
        [JsonPropertyName("records")]
        public List<T> Data { get; init; }
    }
}
