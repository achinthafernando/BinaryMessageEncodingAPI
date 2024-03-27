# Binary Message Encoding API

This is a .NET Core Web API project implementing a simple binary message encoding scheme for a signaling protocol. The project allows peers in a real-time communication application to exchange messages with binary payloads.

## Assumptions

1. **Message Structure**: I assumed each message consists of a variable number of headers and a binary payload, as specified in the task description. *example payload* <br/>
{
  "headers": {
    "Content-Type": "application/json",
    "Content-Length": "1024"
  },
  "payload": "U2luY2ggQ29kZSBUZXN0"
}
   
2. **Payload Format**: Although the task does not specify the payload format, I assumed it could represent any binary data, such as files, serialized objects, or raw binary data. (In the example payload I used Base64 Encoded string "Sinch Code Test")

3. **Maximum Limits**: Maximum limits for header names, values, and the payload size are checked as specified in the task description. I believe these limits are essential for ensuring the efficiency and safety of the encoding scheme.

4. **Error Handling**: I assumed that proper error handling is required to handle invalid messages, exceeding maximum limits, or unexpected data format during the encoding and decoding process.

5. **Clean Code**: Following the best practices of clean code, the implementation must be well-structured, readable, and maintainable. Even though this project doesn't have many functionalities I tried to separate the the projects and the folder in the best possible way

6. **Unit Testing**: I assumed that thorough unit tests should be written to validate the functionality of the encoding and decoding logic. I wrote a test case called *Encode_Decode_Success* in the *BinaryMessageEncodingAPI.nUnitTest => MessageCodecTests* class to validate the encode and decode functionality

## Program Execution
1. Clone the *BreadcrumbsBinaryMessageEncodingAPI* project and run the project
2. It will open the Swagger home page (http://localhost:5161/swagger/index.html) and there are two POST APIs available <br/>
*encode* <br/>
POST: http://localhost:5161/encode <br/>
Payload <br/>
{
  "headers": {
    "Content-Type": "application/json",
    "Content-Length": "1024"
  },
  "payload": "U2luY2ggQ29kZSBUZXN0"
}
<br/>

![image](https://github.com/achinthafernando/BinaryMessageEncodingAPI/assets/22466166/50b9c93e-13d7-4784-b43d-87057b7a7ee5)



*decode*<br/>
http://localhost:5161/decode  <br/>
Payload <br/>
"AgwAQ29udGVudC1UeXBlEABhcHBsaWNhdGlvbi9qc29uDgBDb250ZW50LUxlbmd0aAQAMTAyNA8AAABTaW5jaCBDb2RlIFRlc3Q="  <br/>
Payload is the encoded binary message <br/>

![image](https://github.com/achinthafernando/BinaryMessageEncodingAPI/assets/22466166/9684a71a-d7ce-4815-a88b-5a1b636fc633)


*Please note that I haven't implemented any authentication for the API as it's not requested in the test.* <br/>
Contact me if you need any clarification about the project achintha.fdo@gmail.com


