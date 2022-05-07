using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.moviestarplanet.amf.valueobjects
{
    internal class TicketHeader
    {
        public string Ticket { get; set; }

        public string anyAttribute { get; set; } = null;
    }
}
