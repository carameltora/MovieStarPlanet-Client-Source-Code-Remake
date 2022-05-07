using com.MovieStarPlanet.UserServiceWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStarPlanet__Source_code_
{
    internal class Program
    {
        Msp Msp = new Msp();
        static async Task Main(string[] args)
        {
            LoginEntity login = await Msp.Login("apeXxienss", "beXyza60", "FR");
            Console.WriteLine(login.Actor.ActorId);

            await Task.Delay(-1);
        }
    }
}
