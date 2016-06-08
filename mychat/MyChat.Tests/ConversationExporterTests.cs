using System.IO;
using System.Linq;
using MyChat;
using Newtonsoft.Json;

namespace MindLink.Recruitment.MyChat.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using Rhino.Mocks;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Tests for the <see cref="ConversationExporter"/>.
    /// </summary>
    [TestClass]
    public class ConversationExporterTests
    {

        public string GetFileHash(string filename)
        {
            var hash = new SHA1Managed();
            var clearBytes = File.ReadAllBytes(filename);
            var hashedBytes = hash.ComputeHash(clearBytes);
            return ConvertBytesToHex(hashedBytes);
        }

        public string ConvertBytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x"));
            }
            return sb.ToString();
        }



        [TestMethod]
        public void TestWriteConversation()
        {
            // Assemble portion contains the declarations and required initialization.
            string conversationName = "My Conversation";
            string mockInputFilePath = @"D:\EducationAndWork\mychat\MyChat\chatTest.txt";
            string mockOutputFilePath = @"D:\EducationAndWork\mychat\MyChat\mockOutputChatTest.json";
            string[] mockArgs = new string[] { mockInputFilePath, mockOutputFilePath };

            var _mockMessage1 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470901), "bob", "Hi. I am fucking crazy for houmous.");
            var _mockMessage2 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470905), "mike", "how are you?");
            var _mockMessage3 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470906), "bob", "I'm good thanks, do you like houmous?");
            var _mockMessage4 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470910), "mike", "no, let me ask Angus...");
            var _mockMessage5 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470912), "angus", "Hell yes! Are we buying some houmous?");
            var _mockMessage6 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470914), "bob", "No, just want to know if there's anybody else in the houmous society...");
            var _mockMessage7 = MockRepository.GenerateStub<Message>(DateTimeOffset.FromUnixTimeSeconds(1448470915), "angus", "YES! I'm the head houmous eater there...");

            List<Message> messages = new List<Message>() { _mockMessage1, _mockMessage2, _mockMessage3, _mockMessage4, _mockMessage5, _mockMessage6, _mockMessage7 };
            var _mockConversation = MockRepository.GenerateStub<Conversation>(conversationName, messages);

            var _ConversationExporter = new ConversationExporter();
            //IConversationExporter tes = new ConversationExporter(mockArgs);

            //tes.WriteConversation(_mockConversation, mockOutputFilePath);

            // Act portion contains the code which is used to get the actual result.
            _ConversationExporter.WriteConversation(_mockConversation, mockOutputFilePath);

            var mockJsonFileHashed = GetFileHash(mockOutputFilePath);
            var actualJsonFileHased = GetFileHash(@"D:\EducationAndWork\mychat\MyChat\outputChatTest.json");



            //Expect.Call(_mockConversation.Name).Return("Roy");

            // Assert portion is used for testing the expected and actual result.

            //_mockConversationExporter.AssertWasCalled(x => x.WriteConversation(_mockConversation, mockOutputFilePath));
            Assert.AreEqual(mockJsonFileHashed, actualJsonFileHased);

        }





        ///// <summary>
        ///// Tests that exporting the conversation exports conversation.
        ///// </summary>
        //[TestMethod]
        //public void ExportingConversationExportsConversation()
        //{
        //    string[] args = new string[] { @"D:\EducationAndWork\mychat\MyChat\chat.txt", @"D:\EducationAndWork\mychat\MyChat\outputchat.json" };
        //    ConversationExporter exporter = new ConversationExporter(args);

        //    exporter.ExportConversation("chat.txt", "chat.json");

        //    var serializedConversation = new StreamReader(new FileStream("chat.json", FileMode.Open)).ReadToEnd();

        //    Conversation savedConversation = JsonConvert.DeserializeObject<Conversation>(serializedConversation);

        //    Assert.AreEqual("My Conversation", savedConversation.Name);

        //    var messages = savedConversation.Messages.ToList();

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470901), messages[0].timestamp);
        //    Assert.AreEqual("bob", messages[0].senderId);
        //    Assert.AreEqual("Hello there!", messages[0].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470905), messages[1].timestamp);
        //    Assert.AreEqual("mike", messages[1].senderId);
        //    Assert.AreEqual("how are you?", messages[1].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470906), messages[2].timestamp);
        //    Assert.AreEqual("bob", messages[2].senderId);
        //    Assert.AreEqual("I'm good thanks, do you like pie?", messages[2].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470910), messages[3].timestamp);
        //    Assert.AreEqual("mike", messages[3].senderId);
        //    Assert.AreEqual("no, let me ask Angus...", messages[3].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470912), messages[4].timestamp);
        //    Assert.AreEqual("angus", messages[4].senderId);
        //    Assert.AreEqual("Hell yes! Are we buying some pie?", messages[4].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470914), messages[5].timestamp);
        //    Assert.AreEqual("bob", messages[5].senderId);
        //    Assert.AreEqual("No, just want to know if there's anybody else in the pie society...", messages[5].content);

        //    Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(1448470915), messages[6].timestamp);
        //    Assert.AreEqual("angus", messages[6].senderId);
        //    Assert.AreEqual("YES! I'm the head pie eater there...", messages[6].content);
        //}
    }
}
