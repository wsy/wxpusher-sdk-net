using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WxPusher.Client
{
    /// <summary>返回编码,参考http语义</summary>
    public enum ResultCodes : int
    {
        ///<summary>成功</summary>
        SUCCESS = 1000,
        ///<summary>业务异常错误</summary>
        BIZ_FAIL = 1001,
        ///<summary>未认证</summary>
        UNAUTHORIZED = 1002,
        ///<summary>签名错误</summary>
        SIGN_FAIL = 1003,
        ///<summary>接口不存在</summary>
        NOT_FOUND = 1004,
        ///<summary>服务器内部错误</summary>
        INTERNAL_SERVER_ERROR = 1005,
        ///<summary>和微信交互的过程中发生异常</summary>
        WEIXIN_ERROR = 1006,
        ///<summary>网络异常</summary>
        NETWORK_ERROR = 1007,
        ///<summary>数据异常</summary>
        DATA_ERROR = 1008,
        ///<summary>未知异常</summary>
        UNKNOWN_ERROR = 1009,
    }
}
