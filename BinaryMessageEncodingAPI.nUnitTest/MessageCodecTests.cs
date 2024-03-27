
using BinaryMessageEncodingAPI.Models;
using BinaryMessageEncodingAPI.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Text;


namespace BinaryMessageEncodingAPI.nUnitTest
{
    [TestFixture]
    public class MessageCodecTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IMessageValidator> _mockMessageValidator;

        [SetUp]
        public void Setup()
        {
            _mockMessageValidator = new Mock<IMessageValidator>();
        }

        [Test]
        public void Encode_Decode_Success()
        {
            string messageString = "This is a sample string message.";
            byte[] payloadBytes = Encoding.UTF8.GetBytes(messageString);
            // Arrange
            var originalMessage = new Message
            {
                Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Content-Length", "1024" }
            },
                Payload = payloadBytes
            };


            var mockMessageValidator = new Mock<IMessageValidator>();
            mockMessageValidator.Setup(x => x.ValidateMessage(It.IsAny<Message>()));
            var codec = new MessageCodec(_mockMessageValidator.Object);

            // Act
            byte[] encodedData = codec.Encode(originalMessage);
            Message decodedMessage = codec.Decode(encodedData);

            // Assert
            Assert.That(originalMessage.Headers.Count, Is.EqualTo(decodedMessage.Headers.Count));
            foreach (var header in originalMessage.Headers)
            {
                Assert.That(decodedMessage.Headers.ContainsKey(header.Key));
                Assert.That(header.Value, Is.EqualTo(decodedMessage.Headers[header.Key]));
            }
            Assert.That(originalMessage.Payload.Length, Is.EqualTo(decodedMessage.Payload.Length));
            Assert.That(originalMessage.Payload, Is.EqualTo(decodedMessage.Payload));

        }
    }
}