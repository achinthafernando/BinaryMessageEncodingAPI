using BinaryMessageEncodingAPI.Models;

namespace BinaryMessageEncodingAPI.Services
{
    public class MessageValidator: IMessageValidator
    {
        private readonly IConfiguration _configuration;
        public MessageValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Validate the message.
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidateMessage(Message message)
        {
            var maxHeaderCount = _configuration.GetValue<int>("MaxHeaderCount");
            var maxHeaderSize = _configuration.GetValue<int>("MaxHeaderSize");
            var maxPayloadSize = _configuration.GetValue<int>("MaxPayloadSize");

            if (message.Headers == null || message.Headers.Count == 0)
                throw new ArgumentException("Message must contain headers.");

            if (message.Headers.Count > maxHeaderCount)
                throw new ArgumentException($"Number of headers exceeds maximum limit ({maxHeaderCount}).");

            foreach (var header in message.Headers)
            {
                if (header.Key.Length > maxHeaderSize || header.Value.Length > maxHeaderSize)
                    throw new ArgumentException($"Header name or value exceeds maximum size ({maxHeaderSize}).");
            }

            if (message.Payload == null || message.Payload.Length == 0)
                throw new ArgumentException("Message must contain a payload.");

            if (message.Payload.Length > maxPayloadSize)
                throw new ArgumentException($"Payload size exceeds maximum limit ({maxPayloadSize}).");
        }
    }
}
