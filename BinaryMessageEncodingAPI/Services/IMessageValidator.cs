using BinaryMessageEncodingAPI.Models;

namespace BinaryMessageEncodingAPI.Services
{
    /// <summary>
    /// Interface for message validation.
    /// </summary>
    public interface IMessageValidator
    {
        void ValidateMessage(Message message);
    }
}