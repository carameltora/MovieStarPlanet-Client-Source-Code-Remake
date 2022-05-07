using com.moviestarplanet.amf.checksum;

using FluorineFx.IO;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.moviestarplanet.amf
{
    internal class AmfCaller
    {

        public static string Endpoint;
        public static MemoryStream actualStream;
        public static string SessionID1 = "NGEzNzBhYzcyZWIwNWUyODZmMGYyNGNhMzVlY2ViYTQ0NjM3NDdmMzUxMzgzMw==";
        public static string SessionID;
        public static object[] actualResult;
        public static void GetEndpoint(string Server)
        {
            WebClient webClient = new WebClient();
            JObject jObject = JObject.Parse(webClient.DownloadString("https://disco.mspapis.com/disco/v1/services/msp/" + Server + "?services=mspwebservice"));
            AmfCaller.Endpoint = (string)jObject["Services"][0]["Endpoint"];
            webClient.Dispose();
        }
        public static dynamic DecodeAMF(byte[] body)
        {
            MemoryStream memoryStream = new MemoryStream(body);
            AMFDeserializer aMFDeserializer = new AMFDeserializer(memoryStream);
            AMFMessage aMFMessage = aMFDeserializer.ReadAMFMessage();
            object content = aMFMessage.Bodies[0].Content;
            memoryStream.Dispose();
            aMFDeserializer.Dispose();
            return content;
        }

        public async void AddHeaders(AMFMessage AMFMessageHeader, object[] data)
        {
            AMFMessageHeader.AddHeader(new AMFHeader("sessionID", mustUnderstand: false, AmfCaller.SessionID));
            AMFMessageHeader.AddHeader(new AMFHeader("id", mustUnderstand: false, ChecksumCalculator.createChecksum(data)));
            AMFMessageHeader.AddHeader(new AMFHeader("needClassName", mustUnderstand: false, false));
        }
        public async void AddBody(string Method, AMFMessage AMFMessage, object[] data)
        {
            AMFMessage.AddBody(new AMFBody(Method, "/1", data));
        }

        public async Task<MemoryStream> WriteAmf(AMFMessage AMFMessage)
        {
            MemoryStream memoryStream = new MemoryStream();
            AMFSerializer aMFSerializer = new AMFSerializer(memoryStream);
            aMFSerializer.WriteMessage(AMFMessage);
            aMFSerializer.Flush();
            aMFSerializer.Dispose();
            return memoryStream;
        }

        public async Task <object> CreateRequest(MemoryStream stream, string Method)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(AmfCaller.Endpoint + "/Gateway.aspx?method=" + Method);
                httpWebRequest.Referer = "app:/cache/t1.bin/[[DYNAMIC]]/2";
                httpWebRequest.Accept = "text/xml, application/xml, application/xhtml+xml, text/html;q=0.9, text/plain;q=0.8, text/css, image/png, image/jpeg, image/gif;q=0.8, application/x-shockwave-flash, video/mp4;q=0.9, flv-application/octet-stream;q=0.8, video/x-flv;q=0.7, audio/mp4, application/futuresplash, */*;q=0.5, application/x-mpegURL";
                httpWebRequest.ContentType = "application/x-amf";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; en) AppleWebKit/533.19.4 (KHTML, like Gecko) AdobeAIR/32.0";
                httpWebRequest.Headers["X-Flash-Version"] = "32,0,0,100";
                httpWebRequest.Method = "POST";
                byte[] bytes = Encoding.Default.GetBytes(Encoding.Default.GetString(stream.ToArray()));
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
                try
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    actualStream = new MemoryStream();
                    httpWebResponse.GetResponseStream().CopyTo(actualStream);
                    return DecodeAMF(actualStream.ToArray());
                }
                catch (Exception ex)
                {
                   stream.Dispose();
                   return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<AmfResponse> CallFunction(string Method, object[] data)
        {
            try
            {
                AMFMessage aMFMessage = new AMFMessage(3);
                AddHeaders(aMFMessage, data);
                AddBody(Method, aMFMessage, data);
                dynamic response = await CreateRequest(await WriteAmf(aMFMessage), Method);
                return new AmfResponse() { Content = response };
            }
            catch
            {
                return null;
            }
        }
    }
}
