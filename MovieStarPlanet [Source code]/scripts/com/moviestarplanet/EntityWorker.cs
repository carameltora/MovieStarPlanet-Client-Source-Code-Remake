using com.moviestarplanet.AMFActorServiceForWeb;
using com.MovieStarPlanet.UserServiceWeb;
using MovieStarPlanet__Source_code_;
using scripts.com.moviestarplanet.AMFMovieStarService;

using System;
using System.Threading.Tasks;


namespace com.MovieStarPlanet
{
    internal class EntityWorker
    {
        public async static Task<ActorService> ExtarctActorServiceSearch(dynamic Content)
        {
            return new ActorService()
            {
                ActorId = (int)Content[0]["ActorId"],
                ProfileId = Content[0]["ProfileId"],
                IsVip = (bool)Content[0]["IsVIP"],
                Level = (int)Content[0]["Level"],
                Name = Content[0]["Name"],
                Status = (int)Content[0]["Status"],
            };
        }
        public async static Task<MovieStarListRevisedEntity> ExtarctMovieStarListRevised(dynamic Content)
        {
            scripts.com.moviestarplanet.AMFMovieStarService.Actor Actor = new scripts.com.moviestarplanet.AMFMovieStarService.Actor();
            try
            {
                Actor.ActorId = (int)Content[0]["ActorId"];
                Actor.MouthId = (int)Content[0]["MouthId"];
                Actor.EyeId = (int)Content[0]["EyeId"];
                Actor.Diamonds = (int)Content[0]["Diamonds"];
                Actor.StarCoins = (int)Content[0]["Money"];
                Actor.Fame = (int)Content[0]["Fame"];
                Actor.Level = (int)Content[0]["Level"];
                Actor.FriendCount = (int)Content[0]["FriendCount"];
                Actor.FriendCountVIP = (int)Content[0]["FriendCountVIP"];
                Actor.NoseId = (int)Content[0]["NoseId"];
                Actor.Fortune = (int)Content[0]["Fortune"];
                Actor.NebulaProfileId = (string)Content[0]["NebulaProfileId"];
                Actor.EyeColors = (string)Content[0]["EyeColors"];
                return new MovieStarListRevisedEntity()
                {
                    Actor = Actor
                };
            }
            catch
            {
                return null;
            }
        }
        public async static Task<LoginEntity> ExtarctLoginEntity(dynamic Content)
        {
            if (Content["loginStatus"]["status"] == "Success" || Content["loginStatus"]["status"] == "ThirdPartyCreated")
            {
                UserServiceWeb.Actor Actor = new UserServiceWeb.Actor();
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
                MspApi.Ticket = Actor.Ticket;
                MspApi.ActorId = Actor.ActorId;
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
                    UserServiceWeb.Actor Actor = new UserServiceWeb.Actor();
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
