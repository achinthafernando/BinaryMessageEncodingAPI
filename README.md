# Binary Message Encoding API

This is a .NET Core Web API project implementing a simple binary message encoding scheme for a signaling protocol. The project allows peers in a real-time communication application to exchange messages with binary payloads.

## Assumptions

1. **Message Structure**: I assumed each message consists of a variable number of headers and a binary payload, as specified in the task description.
  *example payload*
  {
  "headers": {
     "Content-Type": "application/json",
    "Content-Length": "1024"
  },
  "payload": "U2luY2ggQ29kZSBUZXN0" //Base64 Encoded -> Sinch Code Test
}
   
2. **Payload Format**: Although the task does not specify the payload format, I assumed it could represent any binary data, such as files, serialized objects, or raw binary data. (In the example payload I used Base64 Encoded string "Sinch Code Test")

3. **Maximum Limits**: Maximum limits for header names, values, and the payload size are checked as specified in the task description. I believe these limits are essential for ensuring the efficiency and safety of the encoding scheme.

4. **Error Handling**: I assumed that proper error handling is required to handle invalid messages, exceeding maximum limits, or unexpected data format during the encoding and decoding process.

5. **Clean Code**: Following the best practices of clean code, the implementation must be well-structured, readable, and maintainable. Even though this project doesn't have many functionalities I tried to separate the the projects and the folder in the best possible way

6. **Unit Testing**: I assumed that thorough unit tests should be written to validate the functionality of the encoding and decoding logic. I wrote a test case called *Encode_Decode_Success* in the *BinaryMessageEncodingAPI.nUnitTest => MessageCodecTests* class to validate the encode and decode functionality

