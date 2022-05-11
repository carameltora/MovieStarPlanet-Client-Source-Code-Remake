using System;
using System.Security.Cryptography;
using System.Text;

namespace ccom.moviestarplanet.amf
{
    internal class TicketGenerator
    {
        public static int markingID = new Random().Next(0, 1000);

        public static string headerTicket(string ticket)
        {
            return ticket + getMarkingId();
        }

        public static string getMarkingId()
        {
            markingID++;
            byte[] bytes = Encoding.UTF8.GetBytes(markingID.ToString());
            MD5 mD = MD5.Create();
            return BitConverter.ToString(mD.ComputeHash(bytes)).Replace("-", "").ToLower() + BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
