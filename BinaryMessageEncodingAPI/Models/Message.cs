namespace BinaryMessageEncodingAPI.Models
{
    public class Message
    {
        public required Dictionary<string, string> Headers { get; set; }
        public required byte[] Payload { get; set; }
    }
}
