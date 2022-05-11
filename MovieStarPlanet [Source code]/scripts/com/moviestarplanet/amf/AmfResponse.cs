using System.Net;

namespace com.moviestarplanet.amf
{
    internal class AmfResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; }
    }
}
