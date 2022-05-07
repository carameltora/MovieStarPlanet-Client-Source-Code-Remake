using com.MovieStarPlanet.UserServiceWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.MovieStarPlanet
{
    internal class EntityWorker
    {
       
        public async static Task<LoginEntity> ExtarctLoginEntity(dynamic Content)
        {
            if (Content["loginStatus"]["status"] == "Success" || Content["loginStatus"]["status"] == "ThirdPartyCreated")
            {
                Actor Actor = new Actor();
                Actor.Ticket = Content["loginStatus"]["ticket"];
                Actor.ActorId = Content["loginStatus"]["actor"]["ActorId"];
                Actor.Level = (int)Content["loginStatus"]["actor"]["Level"];
                Actor.Fame = (int)Content["loginStatus"]["actor"]["Fame"];
                Actor.Diamonds = (int)Content["loginStatus"]["actor"]["Diamonds"];
                Actor.StarCoins = (int)Content["loginStatus"]["actor"]["Money"];
                Actor.Fortune = (int)Content["loginStatus"]["actor"]["Fortune"];
                Actor.SkinColor = (string)Content["loginStatus"]["actor"]["SkinColor"];
                Actor.NoseId = (int)Content["loginStatus"]["actor"]["NoseId"];
                Actor.EyeId = (int)Content["loginStatus"]["actor"]["EyeId"];
                Actor.MouthId = (int)Content["loginStatus"]["actor"]["MouthId"];
                Actor.FriendCount = (int)Content["loginStatus"]["actor"]["FriendCount"];
                Actor.FriendCountVIP = (int)Content["loginStatus"]["actor"]["FriendCountVIP"];
                Actor.Email = (string)Content["loginStatus"]["actor"]["Email"];
                return new LoginEntity()
                {
                    Actor = Actor,
                    Success = true,
                    LoginStatus = LoginStatus.Success
                };
            }
            else
            {
                if (Content["loginStatus"]["status"] == "InvalidCredentials")
                {
                    Actor Actor = new Actor();
                    Actor.ActorId = 0;
                    Actor.Email = "null";
                    Actor.FriendCount = 0;
                    Actor.FriendCountVIP = 0;
                    Actor.Ticket = "null";
                    Actor.Fortune = 0;
                    Actor.Fame = 0;
                    Actor.Level = 0;
                    Actor.StarCoins = 0;
                    Actor.SkinColor = "null";
                    Actor.NoseId = 0;
                    Actor.MouthId = 0;
                    Actor.EyeId = 0;
                    return new LoginEntity()
                    {  
                        Actor = Actor,
                        Success = false,
                        LoginStatus = LoginStatus.InvalidCredentials
                    };
                }
            }
            return new LoginEntity()
            {
                Actor = null,
                Success = false,
                LoginStatus = LoginStatus.Error
            };
        }
    }
}
