using scripts.com.moviestarplanet.enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scripts.com.moviestarplanet.utils
{
    internal class MspHelper
    {
        public static string ParseServer(Server Server)
        {
            if(Server == Server.France)
            {
                return "FR";
            }
            if(Server == Server.UnitedStates)
            {
                return "US";
            }
            return "GB";
        }
    }
}
