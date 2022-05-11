using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.moviestarplanet.AMFActorServiceForWeb
{
    internal class ActorService
    {
        public int ActorId { get; set; }
        public string ProfileId { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsVip { get; set; }
    }
}
