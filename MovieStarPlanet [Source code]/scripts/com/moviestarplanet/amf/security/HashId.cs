using com.moviestarplanet.amf;
using com.moviestarplanet.amf.checksum;

using FluorineFx.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace com.movieStarPlanet.security
{
    internal class HashId
    {
        public static Dictionary<string, string> refIds = new Dictionary<string, string>();

        public static string Endpoint = "";

        public static string SessionID1 = "NGEzNzBhYzcyZWIwNWUyODZmMGYyNGNhMzVlY2ViYTQ0NjM3NDdmMzUxMzgzMw==";

        public static AmfCaller amfCaller = new AmfCaller();
        public HashId()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dynamic val = JsonConvert.DeserializeObject(Encryption.DecryptSessionIdFile(File.ReadAllText(folderPath + "\\9oeiture")));
            refIds.Clear();
            foreach (dynamic item in val)
            {
                if (!refIds.ContainsKey((string)item.TjData))
                {
                    refIds.Add((string)item.TjData, (string)item.Histogram);
                }
            }
            GetEndpointForServer("gb");
        }

        public string GetSessionId()
        {
            dynamic val = SendAMF("MovieStarPlanet.WebService.Os.AMFOs.CreateOsRef", new object[0]);
            if (val.ToString().Contains("ERROR"))
            {
                return GetSessionId();
            }
            if ((!refIds.ContainsKey(val["TjData"])))
            {
                return GetSessionId();
            }
            dynamic val2 = SendAMF("MovieStarPlanet.WebService.Os.AMFOs.RunOsCheck", new object[2]
            {
                val["RefId"],
                refIds[val["TjData"]]
            });
            AmfCaller.SessionID = val2;
            return val2;
        }

        public void GetEndpointForServer(string server)
        {
            WebClient webClient = new WebClient();
            JObject jObject = JObject.Parse(webClient.DownloadString("https://disco.mspapis.com/disco/v1/services/msp/" + server + "?services=mspwebservice"));
            Endpoint = (string)jObject["Services"][0]["Endpoint"];
        }

        public object SendAMF(string method, object[] arguments)
        {
            AMFMessage aMFMessage = new AMFMessage(3);
            aMFMessage.AddHeader(new AMFHeader("sessionID", mustUnderstand: false, HashId.SessionID1));
            aMFMessage.AddHeader(new AMFHeader("id", mustUnderstand: false, ChecksumCalculator.createChecksum(arguments)));
            aMFMessage.AddHeader(new AMFHeader("needClassName", mustUnderstand: false, false));
            aMFMessage.AddBody(new AMFBody(method, "/1", arguments));
            MemoryStream memoryStream = new MemoryStream();
            AMFSerializer aMFSerializer = new AMFSerializer(memoryStream);
            aMFSerializer.WriteMessage(aMFMessage);
            aMFSerializer.Flush();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Endpoint + "/Gateway.aspx?method=" + method);
            httpWebRequest.Referer = "app:/cache/t1.bin/[[DYNAMIC]]/2";
            httpWebRequest.Accept = "text/xml, application/xml, application/xhtml+xml, text/html;q=0.9, text/plain;q=0.8, text/css, image/png, image/jpeg, image/gif;q=0.8, application/x-shockwave-flash, video/mp4;q=0.9, flv-application/octet-stream;q=0.8, video/x-flv;q=0.7, audio/mp4, application/futuresplash, */*;q=0.5, application/x-mpegURL";
            httpWebRequest.ContentType = "application/x-amf";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows; U; en) AppleWebKit/533.19.4 (KHTML, like Gecko) AdobeAIR/32.0";
            httpWebRequest.Headers["X-Flash-Version"] = "32,0,0,100";
            httpWebRequest.Method = "POST";
            byte[] bytes = Encoding.Default.GetBytes(Encoding.Default.GetString(memoryStream.ToArray()));
            httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                memoryStream = new MemoryStream();
                httpWebResponse.GetResponseStream().CopyTo(memoryStream);
                return DecodeAMF(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                return "ERROR! " + ex.ToString();
            }
        }

        public dynamic DecodeAMF(byte[] body)
        {
            MemoryStream stream = new MemoryStream(body);
            AMFDeserializer aMFDeserializer = new AMFDeserializer(stream);
            AMFMessage aMFMessage = aMFDeserializer.ReadAMFMessage();
            return aMFMessage.Bodies[0].Content;
        }
    }
}
