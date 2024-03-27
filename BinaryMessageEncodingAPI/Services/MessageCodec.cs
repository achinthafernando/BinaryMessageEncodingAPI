using BinaryMessageEncodingAPI.Models;

namespace BinaryMessageEncodingAPI.Services
{
    public class MessageCodec: IMessageCodec
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageValidator _messageValidator;

        public MessageCodec(IConfiguration configuration, IMessageValidator messageValidator)
        {
            _configuration = configuration;
            _messageValidator = messageValidator;
        }

        /// <summary>
        /// Encodes a message into a byte array.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public byte[] Encode(Message message)
        {
            try
            {
                //Validate message
                _messageValidator.ValidateMessage(message);

                using (MemoryStream stream = new MemoryStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // Write header count
                    writer.Write((byte)message.Headers.Count);

                    // Write headers
                    foreach (var header in message.Headers)
                    {
                        WriteString(writer, header.Key);
                        WriteString(writer, header.Value);
                    }

                    // Write payload size
                    writer.Write(message.Payload.Length);

                    // Write payload
                    writer.Write(message.Payload);

                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// Decodes a byte array into a message.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="Exception"></exception>
        public Message Decode(byte[] data)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(data))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    var maxHeaderCount = _configuration.GetValue<int>("MaxHeaderCount");
                    var maxPayloadSize = _configuration.GetValue<int>("MaxPayloadSize");
                    // Read header count
                    byte headerCount = reader.ReadByte();
                    if (headerCount > maxHeaderCount)
                        throw new InvalidDataException("Too many headers.");

                    // Read headers
                    var headers = new Dictionary<string, string>();
                    for (int i = 0; i < headerCount; i++)
                    {
                        string name = ReadString(reader);
                        string value = ReadString(reader);
                        headers[name] = value;
                    }

                    // Read payload size
                    int payloadSize = reader.ReadInt32();
                    if (payloadSize > maxPayloadSize)
                        throw new InvalidDataException("Payload size exceeds maximum limit.");

                    // Read payload
                    byte[] payload = reader.ReadBytes(payloadSize);

                    return new Message { Headers = headers, Payload = payload };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// Writes a string to a binary writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private void WriteString(BinaryWriter writer, string value)
        {
            var maxHeaderSize = _configuration.GetValue<int>("MaxHeaderSize");
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(value);
            if (bytes.Length > maxHeaderSize)
                throw new ArgumentException($"String exceeds maximum size ({maxHeaderSize}).");
            writer.Write((ushort)bytes.Length);
            writer.Write(bytes);
        }


        /// <summary>
        /// Reads a string from a binary reader.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private string ReadString(BinaryReader reader)
        {
            ushort length = reader.ReadUInt16();
            byte[] bytes = reader.ReadBytes(length);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

    }
}
