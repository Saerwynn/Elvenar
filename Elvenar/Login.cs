using Elvenar.Actions;
using Elvenar.Misc;
using Elvenar.WebRequests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Elvenar
{
    class Login
    { 
        public UserTokens Start(UserTokens x)
        {
            HttpRequest newLogin = new HttpRequest();

            UserTokens newTokens = new UserTokens
            {
                username = x.username,
                password = x.password,
                language = x.language,
                world = x.world,
                requestId = 0
            };

            string[] initialTokens = newLogin.getXsrfPhp(newTokens);

            newTokens.xsrf = initialTokens[0];
            newTokens.phpsessid = initialTokens[1];

            newTokens.tid = Tid.CreateTid().ToString();

            newTokens.glps = newLogin.getGlps(newTokens);
            newTokens.redirect = newLogin.getRedirect(newTokens);
            newTokens.mid = newLogin.getMid(newTokens);
            newTokens.gateway = newLogin.getGateway(newTokens);
            newTokens.sid = newLogin.getSid(newTokens);
            newTokens.game_gateway = newLogin.getGameGateway(newTokens);

            newTokens.gateway_id = newTokens.game_gateway.Substring(28);

            return newTokens;
        }
    }

    class UserTokens
    {
        public string username { get; set; }
        public string password { get; set; }
        public string language { get; set; }
        public string world { get; set; }
        public string xsrf { get; set; }
        public string phpsessid { get; set; }
        public string glps { get; set; }
        public string tid { get; set; }
        public string redirect { get; set; }
        public string mid { get; set; }
        public string gateway { get; set; }
        public string sid { get; set; }
        public string game_gateway { get; set; }
        public string gateway_id { get; set; }
        public int requestId { get; set; }
        public List<int> player_ids { get; set; }

        public class GatewayJson
        {
            public string redirect { get; set; }
            public string url { get; set; }
        }
    }

    class User
    {
        public void loginUser(UserTokens user)
        {
            Login newLogin = new Login();
            UserTokens newTokens = newLogin.Start(user);

            var csv = new StringBuilder();

            csv.AppendLine("Name,WonderBaseName,Invested KP,Required KP,Δ,1,2,3,4,5,6,7,8"); 

            var playerRankingListData = Request.getPlayerRankingList(newTokens);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 61;
            Console.Write("]"); //end

            float oneChunk = 60.0f / playerRankingListData.length;

            foreach (var p in playerRankingListData.rankings)
            {
                int position = (int)Math.Floor(oneChunk * p.rank);
                if (position > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.CursorLeft = position;
                    Console.Write("■");
                }

                if (p.points > 2500)
                {
                    var ancientWonderResearchPhasesData = Request.getAncientWonderResearchPhases(newTokens, p.player.player_id);

                    if (ancientWonderResearchPhasesData != null)
                    {
                        foreach (var aw in ancientWonderResearchPhasesData)
                        {
                            if (aw.investedKnowledgePoints > 0)
                            {
                                var remain = aw.requiredKnowledgePoints - aw.investedKnowledgePoints;
                                if (remain > 0 && remain < 100)
                                {
                                    var newLine = string.Format("{0},{1},{2},{3},{4}",
                                        p.player.name,
                                        aw.entityBaseName,
                                        aw.investedKnowledgePoints,
                                        aw.requiredKnowledgePoints,
                                        remain);

                                    foreach (var c in aw.contributions)
                                    {
                                        if (c.rank != -1) {
                                            newLine += string.Format(",{0}", c.knowledgePoints);
                                            if (user.player_ids.Contains(c.player.player_id)) newLine += "*";
                                        }
                                    }

                                    csv.AppendLine(newLine);
                                }
                            }
                        }
                    }
                }

                //draw totals
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorLeft = 65;
                string formatString = "{0," + playerRankingListData.length.ToString().Length + "} / {1}";
                Console.Write(formatString, p.rank, playerRankingListData.length);
            }

            File.WriteAllText(@"AncientWonders_" + user.world.ToUpper() + ".csv", csv.ToString());
        }
    }
}


