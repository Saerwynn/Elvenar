using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Elvenar
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("Elvenar Ancient Wonders Scraper\n");

            var config = new Dictionary<string, string>();
            var configFile = File.ReadAllLines("config.txt");
            foreach (string line in configFile)
            {
                string s = line.TrimStart();
                int index = s.IndexOf("=");
                config[s.Substring(0, index).Trim()] = s.Substring(index + 1).Trim();
            }

            User login = new User();
            UserTokens newUser = new UserTokens
            {
                username = config["username"],
                password = config["password"],
                language = config["language"],
                world = config["world"],
                player_ids = config["player_ids"].Split(',').Select(int.Parse).ToList()
            };

            Console.WriteLine("World: {0}\n", config["world"]);

            login.loginUser(newUser);

            stopWatch.Stop();

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("\n\nRunTime: " + elapsedTime);

            GC.Collect();
        }
    }
}


