using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.moviestarplanet.amf
{
    internal class AmfResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; }
    }
}
