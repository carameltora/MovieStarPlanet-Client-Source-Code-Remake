using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.MovieStarPlanet.UserServiceWeb
{
    internal class LoginEntity
    {
        public Actor Actor { get; set; }
        public string LoginStatus { get; set; }
        public bool Success { get; set; }
    }

    internal class Actor
    {
        public string Ticket { get; set; }
        public int ActorId { get; set; }
        public int Fame { get; set; }
        public int Level { get; set; }
        public int StarCoins { get; set; }
        public int Diamonds { get; set; }
        public int Fortune { get; set; }
        public string SkinColor { get; set; }
        public int NoseId { get; set; }
        public int MouthId { get; set; }
        public int EyeId { get; set; }
        public int FriendCount { get; set; }
        public int FriendCountVIP { get; set; }
        public string Email { get; set; }
    }
}
