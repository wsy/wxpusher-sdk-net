using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WxPusher.Client
{
    public record Result
    {
        [JsonPropertyName("code")]
        public required int Code { get; init; }
        [JsonPropertyName("msg")]
        public required string Message { get; init; }

        [SetsRequiredMembers]
        public Result(int resultCode, string message)
        {
            this.Code = resultCode;
            this.Message = message;
            IsSuccess = Code == (int)ResultCodes.SUCCESS;
        }

        public bool IsSuccess { get; }

        public static Result<T> FromData<T>(T? data)
        {
            return new Result<T>(data);
        }
        public static Result<T> FromData<T>(T? data, int resultCode, string message)
        {
            return new Result<T>(data, resultCode, message);
        }
    }
    public record Result<T> : Result
    {
        [JsonPropertyName("data")]
        public required T? Data { get; init; }

        [SetsRequiredMembers]
        public Result() : this(default(T)) { }
        [SetsRequiredMembers]
        public Result(T? data) : this(data, (int)ResultCodes.SUCCESS, string.Empty) { }

        [SetsRequiredMembers]
        public Result(T? data, int resultCode, string message) : base(resultCode, message)
        {
            this.Data = data;
        }

        protected override bool PrintMembers(StringBuilder builder)
        {
            base.PrintMembers(builder);
            builder.Append("Data = ");
            if (Data == null)
            {
                builder.Append("null");
            }
            else if (Data is IEnumerable enumerable)
            {
                StringBuilder stringBuilder = new();
                foreach (var item in enumerable)
                {
                    stringBuilder.Append(", ").Append(item.ToString());
                }
                stringBuilder.Append(" ]");
                stringBuilder[0] = '[';
                builder.Append(stringBuilder);
            }
            else
            {
                builder.Append(Data.ToString());
            }
            return true;
        }
    }
}
