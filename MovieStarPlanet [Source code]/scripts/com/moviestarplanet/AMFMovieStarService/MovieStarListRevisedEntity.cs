using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scripts.com.moviestarplanet.AMFMovieStarService
{
    internal class MovieStarListRevisedEntity
    {
        public Actor Actor { get; set; }
    }

    public class Actor
    {
        public string NebulaProfileId { get; set; }
        public int ActorId { get; set; }
        public int Fame { get; set; }
        public int Level { get; set; }
        public int StarCoins { get; set; }
        public int Diamonds { get; set; }
        public int Fortune { get; set; }
        public int NoseId { get; set; }
        public int MouthId { get; set; }
        public int EyeId { get; set; }
        public int FriendCount { get; set; }
        public int FriendCountVIP { get; set; }
        public string EyeColors { get; set; }
    }
}
