using Elvenar.Misc;
using Elvenar.WebRequests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;

namespace Elvenar.Actions
{
    class Request
    {
        public static string responseData;

        public static StartupVO getStartupData(UserTokens tokens)
        {
            ServerRequestVO newRequest = new ServerRequestVO
            {
                requestId = ++tokens.requestId,
                requestMethod = "getData",
                requestClass = "StartupService",
                requestData = new List<object>(),
                __clazz__ = "ServerRequestVO"
            };

            List<object> readyData = new List<object>
            {
                newRequest
            };

            string initialData = JsonConvert.SerializeObject(readyData);

            string query = Encryption.Hash(tokens, initialData);
            //Console.WriteLine(" query: {0}\n", query);

            HttpRequest requestData = new HttpRequest();

            string rawData = requestData.Request(tokens, query);
            //File.WriteAllText(@"D:\Temp\getStartupData - rawData.txt", JsonConvert.SerializeObject(rawData));       

            var a = JsonConvert.DeserializeObject<List<ServerResponseVO>>(rawData);
            //File.WriteAllText(@"D:\Temp\getStartupData - a.txt", JsonConvert.SerializeObject(a));

            foreach (var value in a)
            {
                if (value.requestClass == "StartupService")
                {
                    responseData = value.responseData.ToString();
                    break;
                }
            }
            //File.WriteAllText(@"D:\Temp\getStartupData - responseData.txt", responseData);     

            return JsonConvert.DeserializeObject<StartupVO>(responseData);
        }

        public static PlayerRankingListVO getPlayerRankingList(UserTokens tokens)
        {
            var wL = new List<object> { "player", 0, 999999, "", "name" };

            ServerRequestVO newRequest = new ServerRequestVO
            {
                requestId = ++tokens.requestId,
                requestMethod = "getRankingList",
                requestClass = "RankingService",
                requestData = wL,
                __clazz__ = "ServerRequestVO"
            };

            List<object> readyData = new List<object>
            {
                newRequest
            };

            string initialData = JsonConvert.SerializeObject(readyData);

            string query = Encryption.Hash(tokens, initialData);
            //Console.WriteLine(" query: {0}\n", query);

            HttpRequest requestData = new HttpRequest();

            string rawData = requestData.Request(tokens, query);
            //File.WriteAllText(@"D:\Temp\getPlayerRankingList - rawData.txt", JsonConvert.SerializeObject(rawData));            

            var a = JsonConvert.DeserializeObject<List<ServerResponseVO>>(rawData);
            //File.WriteAllText(@"D:\Temp\getPlayerRankingList - a.txt", JsonConvert.SerializeObject(a));            

            foreach (var value in a)
            {
                if (value.requestClass == "RankingService")
                {
                    responseData = value.responseData.ToString();
                    break;
                }
            }
            //File.WriteAllText(@"D:\Temp\getPlayerRankingList - responseData.txt", responseData);         

            return JsonConvert.DeserializeObject<PlayerRankingListVO>(responseData);
        }

        public static GuildRankingVO[] getGuildRankings(UserTokens tokens)
        {
            var wL = new List<object> { "guild", 0, 999999, "", "name" };

            ServerRequestVO newRequest = new ServerRequestVO
            {
                requestId = ++tokens.requestId,
                requestMethod = "getRankingList",
                requestClass = "RankingService",
                requestData = wL,
                __clazz__ = "ServerRequestVO"
            };

            List<object> readyData = new List<object>
            {
                newRequest
            };

            string initialData = JsonConvert.SerializeObject(readyData);

            string query = Encryption.Hash(tokens, initialData);
            //Console.WriteLine(" query: {0}\n", query);

            HttpRequest requestData = new HttpRequest();

            string rawData = requestData.Request(tokens, query);
            //File.WriteAllText(@"D:\Temp\getGuildRankingList - rawData.txt", JsonConvert.SerializeObject(rawData));            

            var a = JsonConvert.DeserializeObject<List<ServerResponseVO>>(rawData);
            //File.WriteAllText(@"D:\Temp\getGuildRankingList - a.txt", JsonConvert.SerializeObject(a));            

            foreach (var value in a)
            {
                if (value.requestClass == "RankingService")
                {
                    responseData = value.responseData.ToString();
                    break;
                }
            }
            //File.WriteAllText(@"D:\Temp\getGuildRankingList - responseData.txt", responseData);

            return (JsonConvert.DeserializeObject<GuildRankingListVO>(responseData)).rankings;
        }

        public static ResearchPhaseVO[] getAncientWonderResearchPhases(UserTokens tokens, int player_id)
        {
            var wL = new List<object> { player_id };

            ServerRequestVO newRequest = new ServerRequestVO
            {
                requestId = ++tokens.requestId,
                requestMethod = "visitPlayer",
                requestClass = "OtherPlayerService",
                requestData = wL,
                __clazz__ = "ServerRequestVO"
            };

            List<object> readyData = new List<object>
            {
                newRequest
            };

            string initialData = JsonConvert.SerializeObject(readyData);

            string query = Encryption.Hash(tokens, initialData);
            //Console.WriteLine(" query: {0}\n", query);

            HttpRequest requestData = new HttpRequest();

            int i = 2;
            string rawData;

            do
            {
                rawData = requestData.Request(tokens, query);
                if (rawData != null) break;

                Thread.Sleep(1000);
            }
            while (i-- > 0);
            //File.WriteAllText(@"D:\Temp\getOtherPlayerCityMap - rawData.txt", JsonConvert.SerializeObject(rawData));

            var a = JsonConvert.DeserializeObject<List<ServerResponseVO>>(rawData);
            //File.WriteAllText(@"D:\Temp\getOtherPlayerCityMap - a.txt", JsonConvert.SerializeObject(a));            

            foreach (var value in a)
            {
                if (value.requestClass == "OtherPlayerService")
                {
                    responseData = value.responseData.ToString();
                    break;
                }
            }
            //File.WriteAllText(@"D:\Temp\getOtherPlayerCityMap - responseData.txt", responseData);

            return (JsonConvert.DeserializeObject<OtherPlayerCityVO>(responseData)).city_map.ancientWonderResearchPhases;
        }
    }
}
