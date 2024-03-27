using BinaryMessageEncodingAPI.Models;

namespace BinaryMessageEncodingAPI.Services
{
    /// <summary>
    /// Interface for message encoding and decoding.
    /// </summary>
    public interface IMessageCodec
    {
        byte[] Encode(Message message);
        Message Decode(byte[] data);
    }
}
