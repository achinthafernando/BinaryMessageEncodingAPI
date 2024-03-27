using BinaryMessageEncodingAPI.Models;
using BinaryMessageEncodingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BinaryMessageEncodingAPI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageCodec _messageCodec;
        public MessageController(IMessageCodec messageCodec)
        {
            _messageCodec = messageCodec;
        }

        // POST api/message/encode
        [HttpPost("encode")]
        public ActionResult<byte[]> EncodeMessage([FromBody] Message message)
        {
            try
            {
                byte[] encodedData = _messageCodec.Encode(message);
                return Ok(encodedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/message/decode
        [HttpPost("decode")]
        public ActionResult<Message> DecodeMessage([FromBody] byte[] data)
        {
            try
            {
                Message decodedMessage = _messageCodec.Decode(data);
                return Ok(decodedMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
