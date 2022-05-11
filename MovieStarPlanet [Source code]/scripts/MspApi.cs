using ccom.moviestarplanet.amf;
using com.moviestarplanet.amf;
using com.moviestarplanet.amf.valueobjects;
using com.moviestarplanet.AMFActorServiceForWeb;
using com.moviestarplanet.AMFLoggingService;
using com.movieStarPlanet.security;
using com.MovieStarPlanet;
using com.MovieStarPlanet.UserServiceWeb;
using scripts.com.moviestarplanet.AMFMovieStarService;
using scripts.com.moviestarplanet.enums;
using scripts.com.moviestarplanet.utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieStarPlanet__Source_code_
{
    internal class MspApi
    {
        public static AmfCaller amfCaller = new AmfCaller();
        public static string Ticket;
        public static int ActorId;
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
        public async static Task LogClientAsync(string IP, string ActorId, string Username, string Status)
        {
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.Logging.AMFLoggingService.LogClient", new object[]
            {
                 Status, "{\"session\":{\"domain\":\"moviestarplanet.fr\",\"user\":{\"externalIP\":\"" + IP + "\",\"id\":" + ActorId + ",\"name\":\"" + Username + "\"}},\"logVersion\":\"1.0.1\",\"application\":{\"runtimeVersion\":\"WIN 32,0,0,100\",\"title\":\"MovieStarPlanet\",\"mode\":\"normal\",\"version\":\"96.3.1\",\"uptime\":48,\"platform\":\"game shell\",\"buildId\":\"17634\",\"browser\":\"N\\/A (Standalone)\"},\"event\":{\"message\":\"CrashLoggerWeb.as\"},\"hardware\":{\"manufacturer\":\"Adobe Windows\",\"memory\":{\"allocated\":102006784,\"available\":28569600,\"total\":130576384},\"screen\":{\"aspectRatio\":\"16:9\",\"height\":900,\"dpi\":96,\"width\":1600},\"os\":{\"name\":\"Windows 10\"},\"fingerPrint\":\"MSP1-Standalone:XXXXXX\"},\"crashLog\":{\"lastCrashLine\":\"CloseContentCommandfriends\",\"features\":[{\"context\":[],\"featureName\":\"com.moviestarplanet.Forms::FriendActivityListComponent\"},{\"context\":[],\"featureName\":\"friends\"},{\"context\":[],\"featureName\":\"com.moviestarplanet.messaging.module.moduleparts.messagingwindow.view::MessagingView\"},{\"context\":[],\"featureName\":\"com.moviestarplanet.Components.Character::CharacterContainer\"},{\"context\":[],\"featureName\":\"com.moviestarplanet.flash.icons::ActivitiesIcons_ActivitiesBarBackground\"},{\"context\":[],\"featureName\":\"fame_magazine\"},{\"context\":[],\"featureName\":\"com.moviestarplanet.messaging.module.moduleparts.messagingwindow.view::MessagingView\"},{\"context\":[],\"featureName\":\"com.moviestarplanet.messaging.module.moduleparts.messagingwindow.view::MessagingView\"},{\"context\":[],\"featureName\":\"friends\"}],\"steps\":9,\"lastFeaturesBeforeCrash\":\"friends, com.moviestarplanet.messaging.module.moduleparts.messagingwindow.view::MessagingView\",\"behaviourHash\":\"a066e6add106acafd7adf145b3511647\"}}"
            });
        }

        public async static Task<ActorService> SearchUserAsync(string Username)
        {
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.ActorService.AMFActorServiceForWeb.SearchActorByNameNeb", new object[]
            {
                 new TicketHeader { Ticket = TicketGenerator.headerTicket(MspApi.Ticket)}, Convert.ToInt64(MspApi.ActorId), Username
            });
            return await EntityWorker.ExtarctActorServiceSearch(resp.Content);
        }
        
        public async static Task<MovieStarListRevisedEntity> LoadMovieStarListRevisedAsync(int ActorId)
        {
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.MovieStar.AMFMovieStarService.LoadMovieStarListRevised", new object[]
            {
                 new TicketHeader { Ticket = TicketGenerator.headerTicket(MspApi.Ticket)}, new object[] { ActorId }
            });
            return await EntityWorker.ExtarctMovieStarListRevised(resp.Content);
        }

        public async static Task<LoginEntity> LoginProxyAsync(string Username, string Password, string Server, string Proxy)
        {
            SessionUpdater();
            await Task.Delay(1600);
            AmfCaller.GetEndpoint(Server);
            while (AmfCaller.SessionID == null)
            { }
            AmfResponse resp = await amfCaller.CallAmf(AmfCaller.Endpoint, "MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login", new object[]
            {
                 Username, Password, null, null, null, "MSP1-Standalone:XXXXXX"
            }, Proxy);
            return await EntityWorker.ExtarctLoginEntity(resp.Content);
        }

        public async static Task<LoginEntity> LoginAsync(string Username, string Password, string Server)
        {
            SessionUpdater();
            await Task.Delay(1600);
            AmfCaller.GetEndpoint(Server);
            while (AmfCaller.SessionID == null)
            { }
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login", new object[]
            {
                 Username, Password, null, null, null, "MSP1-Standalone:XXXXXX"
            });
            return await EntityWorker.ExtarctLoginEntity(resp.Content);
        }

        public async static Task<LoginEntity> LoginAsync(string Username, string Password, Server Server)
        {
            SessionUpdater();
            await Task.Delay(1600);
            AmfCaller.GetEndpoint(MspHelper.ParseServer(Server));
            while (AmfCaller.SessionID == null)
            { }
            AmfResponse resp = await amfCaller.CallFunction("MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login", new object[]
            {
                 Username, Password, null, null, null, "MSP1-Standalone:XXXXXX"
            });
            return await EntityWorker.ExtarctLoginEntity(resp.Content);
        }
    }
}
