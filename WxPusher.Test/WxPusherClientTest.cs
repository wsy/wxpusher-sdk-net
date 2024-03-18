using System.Diagnostics;

using WxPusher.Client;
using WxPusher.Client.Requests;
using WxPusher.Client.Responses;

namespace WxPusher.Test
{
    [TestClass]
    public class WxPusherClientTest
    {
        private readonly WxPusherClient client = new();

        [TestInitialize]
        public void TestInitialize()
        {
            Debug.WriteLine("Test Initializing!");
            DebugFlag.Debug = true;
            Debug.WriteLine("Test Initialized!");
        }

        [TestMethod]
        public async Task SendContentOnlyTest()
        {
            DebugFlag.Debug = true;
            Message testData = new()
            {
                // Put your own token here
                AppToken = "AT_1234567890abcdef1234567890ABCDEF",
                ContentType = MessageContentTypes.Html,
                Content = "<h1>Unit test</h1>",
                // Put your own TopicId here
                TopicIds = new() { 0000 },
                Url="http://www.example.com",
            };
            Result<List<MessageResponse>> testResult = await client.Send(testData);
            Assert.AreEqual((int)ResultCodes.SUCCESS, testResult.Code);
            Assert.AreEqual("处理成功", testResult.Message);
            List<MessageResponse>? data = testResult.Data;
            Assert.IsNotNull(data);
            Assert.AreNotEqual(0, data.Count);
            MessageResponse message = data[0];
            Assert.IsNotNull(message);
            Assert.AreEqual((int)ResultCodes.SUCCESS, message.Code);
        }
    }
}