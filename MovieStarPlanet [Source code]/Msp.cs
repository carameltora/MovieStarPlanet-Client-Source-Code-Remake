using com.moviestarplanet.amf;
using com.movieStarPlanet.security;
using com.MovieStarPlanet;
using com.MovieStarPlanet.UserServiceWeb;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MovieStarPlanet__Source_code_
{
    internal class Msp
    {
        public static AmfCaller amfCaller = new AmfCaller();

        public static void SessionUpdater()
        {
            new Thread(async () =>
            {
                while (true)
                {
                    new HashId().GetSessionId();
                    await Task.Delay(10000);
                }
            }).Start();
        }

        public async static Task<LoginEntity> Login(string Username, string Password, string Server)
        {
            SessionUpdater();
            await Task.Delay(1600);
            AmfCaller.GetEndpoint(Server);
            while(AmfCaller.SessionID == null)
            {}
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login", new object[]
            {
                 Username, Password, null, null, null, "MSP1-Standalone:XXXXXX"
            });
            return await EntityWorker.ExtarctLoginEntity(resp.Content);
        }
    }
}
