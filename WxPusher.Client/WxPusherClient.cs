using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using WxPusher.Client.Data;
using WxPusher.Client.Requests;
using WxPusher.Client.Responses;
namespace WxPusher.Client
{
    /// <summary>
    /// WxPusher的客户端
    /// </summary>
    public class WxPusherClient
    {
        private HttpClient httpClient = new() { BaseAddress = new Uri("https://wxpusher.zjiecode.com") };

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Result<List<MessageResponse>>> Send(Message message)
        {
            if (message == null)
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "消息不能为空");
            }
            if (string.IsNullOrWhiteSpace(message.AppToken))
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "appToken不能为空");
            }
            if (string.IsNullOrWhiteSpace(message.Content))
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "content内容不能为空");
            }
            var response = await httpClient.PostAsJsonAsync("/api/send/message", message);
            var sendResult = await HandleResponse<List<MessageResponse>>(response);
            return sendResult;
        }

        /// <summary>
        /// 创建带参数的app临时二维码
        /// </summary>
        /// <param name="createQrcodeReq"></param>
        /// <returns></returns>
        public async Task<Result<CreateQrcodeResponse>> CreateAppTempQrcode(CreateQrcodeRequest createQrcodeReq)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/fun/create/qrcode", createQrcodeReq);
            Result<CreateQrcodeResponse> result = await HandleResponse<CreateQrcodeResponse>(response);
            return result;
        }

        /// <summary>
        /// 查询消息发送状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public async Task<Result<int>> QueryMessageStatus(long messageId)
        {
            if (messageId <= 0)
            {
                return Result.FromData<int>(default, (int)ResultCodes.BIZ_FAIL, "messageId为空");
            }
            var response = await httpClient.GetAsync(string.Format("/api/send/query/{0}", messageId));
            return await HandleResponse<int>(response);
        }

        /// <summary>
        /// 查询关注你App的微信用户
        /// </summary>
        /// <param name="appToken">应用token</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="uid">根据UID过滤用户</param>
        /// <returns>查询的结果</returns>
        public async Task<Result<PageResponse<WxUser>>> QueryWxUser(string appToken, int page = 1, int pageSize = 1, string? uid = null)
        {
            const string url = "/api/fun/wxuser";
            if (string.IsNullOrWhiteSpace(appToken))
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "appToken不能为空");
            }
            if (page <= 0)
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "page不合法");
            }
            if (pageSize <= 0)
            {
                return new(null, (int)ResultCodes.BIZ_FAIL, "pageSize不合法");
            }
            StringBuilder request = new(url);
            request.Append("?appToken=").Append(appToken);
            request.Append("&page=").Append(page);
            request.Append("&pageSize=").Append(pageSize);
            if (string.IsNullOrWhiteSpace(uid))
            {
                request.Append("uid").Append(uid);
            }
            var response = await httpClient.GetAsync(request.ToString());
            Result<PageResponse<WxUser>> result = await HandleResponse<PageResponse<WxUser>>(response);
            return result;
        }

        private static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return Result.FromData<T>(default, (int)response.StatusCode, "http请求错误");
            }
            Stream resultStream = await response.Content.ReadAsStreamAsync();
            if (DebugFlag.Debug)
            {
                MemoryStream memoryStream = new();
                resultStream.CopyTo(memoryStream);
                Console.WriteLine("ResultStream.Position: " + resultStream.Position);
                byte[] resultBytes = memoryStream.ToArray();
                Console.WriteLine("ResultBytes.Length: " + resultBytes.Length);
                string resultString = Encoding.UTF8.GetString(resultBytes);
                Console.WriteLine("Result String: " + resultString);
                resultStream.Position = 0;
                Console.WriteLine("ResultStream.Position: " + resultStream.Position);
                Thread.Sleep(1000);
            }

            Result<T>? result = await JsonSerializer.DeserializeAsync<Result<T>>(resultStream);
            if (result == null)
            {
                return Result.FromData<T>(default, (int)ResultCodes.INTERNAL_SERVER_ERROR, "服务器响应为空");
            }
            return result;
        }
    }
}
