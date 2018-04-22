using System;

namespace Rocket.Parser
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Use commands:");
            Console.WriteLine("/start -- start parsing");
            Console.WriteLine("/abort -- abort parsing");
            Console.WriteLine("/quit -- close the app");

            var quitNow = false;
            while (!quitNow)
            {
                var parser = new Middleware();

                var command = Console.ReadLine();
                switch (command)
                {
                    case "/start":
                        Console.Write("Write start page:");
                        var startPage = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Write end page:");
                        var endPage = Convert.ToInt32(Console.ReadLine());

                        parser.Start(startPage, endPage);

                        break;

                    case "/abort":
                        parser.Abort();
                        Console.WriteLine("App was abort");
                        break;

                    case "/quit":
                        parser.Abort();
                        quitNow = true;
                        break;

                    default:
                        Console.WriteLine("Unknown Command " + command);
                        break;
                }
            }
        }


    }
}
