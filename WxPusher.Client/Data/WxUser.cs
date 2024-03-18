using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client.Data
{
    /// <summary>微信用户数据</summary>
    public record WxUser
    {

        /// <summary>UID，用户标志</summary>
        [JsonPropertyName("uid")]
        public string UserId { get; init; }

        /// <summary>用户是否接收消息，也就是是否打开了消息开关</summary>
        [JsonPropertyName("enable")]
        public bool Enabled { get; init; }

        /// <summary>用户关注应用的时间</summary>
        [JsonPropertyName("createTime")]
        public long CreateTime { get; init; }

        /// <summary>昵称</summary>
        [JsonPropertyName("nickName")]
        public string NickName { get; init; }

        /// <summary>头像</summary>
        [JsonPropertyName("headImg")]
        public string HeadImg { get; init; }
    }
}
