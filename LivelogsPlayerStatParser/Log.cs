using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace LivelogsPlayerStatParser
{
    public class Log
    {
        public Team RedTeam = new Team();
        public Team BlueTeam = new Team();
        public String Html, LogID, Name, Server, Map, GameTime, URL;
        public double Weighting;
        public DateTime dateTime;
        public HtmlDocument doc = new HtmlDocument();
        public Log(String URL, double weighting)
        {
            if (!URL.StartsWith("http://"))
                URL = "http://" + URL;
            Html = new WebClient().DownloadString(URL);
            doc.LoadHtml(Html);
            this.URL = URL;
            this.Weighting = weighting;
        }
        public void ParseLog()
        {
            GetLogOverview();
            GetStats();
        }

        private void GetStats()
        {
            var playerTable = doc.GetElementbyId("general_stats").SelectSingleNode(".//tbody");
            var players = playerTable.SelectNodes(".//tr");

            foreach (HtmlAgilityPack.HtmlNode node in players) //Loop each player in the log
            {
                Player player = new Player();
                player.steamID = long.Parse(node.SelectSingleNode(".//a").GetAttributeValue("href", "").Replace(@"/player/", ""));
                for (int i = 0; i < player.Stats.Count; i++) // Fill stats
                {
                    player.Stats[i].ChangeValue((doc.GetElementbyId(player.steamID + "." + player.Stats[i].IdName).InnerText), false);
                    ;
                }
                foreach (HtmlAgilityPack.HtmlNode classes in doc.GetElementbyId(player.steamID + ".class").SelectNodes(".//img")) //Get classes played by player
                {
                    String className = classes.GetAttributeValue("alt", "not_defined");
                    player.ClassesPlayed.Add(new Class((Class.ClassTypes)Enum.Parse(typeof(Class.ClassTypes), className, true)));
                }
                int healing_done_index = player.FindStatIndex(player.CustomStats, "healing_done");
                int overhealing_done_index = player.FindStatIndex(player.CustomStats, "overhealing_done");
                int ubers_used_index = player.FindStatIndex(player.CustomStats, "ubers_used");
                int ubers_lost_index = player.FindStatIndex(player.CustomStats, "ubers_lost");
                int ulp_index = player.FindStatIndex(player.CustomStats, "ulp");
                if (player.ClassesPlayed.FindIndex(o => o.classType == Class.ClassTypes.Medic) >= 0 && (doc.GetElementbyId(player.steamID + "." + player.CustomStats[healing_done_index].IdName) != null))
                {
                    
                    player.CustomStats[healing_done_index].ChangeValue(doc.GetElementbyId(player.steamID + "." + player.CustomStats[healing_done_index].IdName).InnerText, false);               
                    player.CustomStats[overhealing_done_index].ChangeValue(doc.GetElementbyId(player.steamID + "." + player.CustomStats[overhealing_done_index].IdName).InnerText, false);                
                    player.CustomStats[ubers_used_index].ChangeValue(doc.GetElementbyId(player.steamID + "." + player.CustomStats[ubers_used_index].IdName).InnerText, false);                  
                    player.CustomStats[ubers_lost_index].ChangeValue(doc.GetElementbyId(player.steamID + "." + player.CustomStats[ubers_lost_index].IdName).InnerText, false);              
                    player.CustomStats[ulp_index].ChangeValue(GetPercentage(player.CustomStats[ubers_used_index].Value, player.CustomStats[ubers_lost_index].Value), false);
                }
                else
                {
                    player.CustomStats[healing_done_index].ChangeValue(0, false);
                    player.CustomStats[overhealing_done_index].ChangeValue(0, false);
                    player.CustomStats[ubers_used_index].ChangeValue(0, false);
                    player.CustomStats[ubers_lost_index].ChangeValue(0, false);
                    player.CustomStats[ulp_index].ChangeValue(0, false);
                }

                if (node.SelectSingleNode(".//a").GetAttributeValue("class", "not_defined") == "player_community_id_link red_player") //Sort into teams
                    this.RedTeam.TeamPlayers.Add(player);
                else
                    this.BlueTeam.TeamPlayers.Add(player);

            }

            foreach (Player player in BlueTeam.TeamPlayers) //Finish blue stats
            {
                player.CustomStats[player.FindStatIndex(player.CustomStats, "hrr")].ChangeValue(GetPercentage(BlueTeam.GetTeamHealing(), player.Stats[player.FindStatIndex(player.Stats, "healing_received")].Value + player.Stats[player.FindStatIndex(player.Stats, "overhealing_received")].Value), false);
                player.CustomStats[player.FindStatIndex(player.CustomStats, "drg")].ChangeValue(GetPercentage(BlueTeam.GetTeamDamage(), player.Stats[player.FindStatIndex(player.Stats, "damage_dealt")].Value), false);
                player.CustomStats[player.FindStatIndex(player.CustomStats, "drt")].ChangeValue(GetPercentage(RedTeam.GetTeamDamage(), player.Stats[player.FindStatIndex(player.Stats, "damage_taken")].Value), false);
            }
            foreach (Player player in RedTeam.TeamPlayers) //Finish red stats
            {
                player.CustomStats[player.FindStatIndex(player.CustomStats, "hrr")].ChangeValue(GetPercentage(RedTeam.GetTeamHealing(), player.Stats[player.FindStatIndex(player.Stats, "healing_received")].Value+ player.Stats[player.FindStatIndex(player.Stats, "overhealing_received")].Value), false);
                player.CustomStats[player.FindStatIndex(player.CustomStats, "drg")].ChangeValue(GetPercentage(RedTeam.GetTeamDamage(), player.Stats[player.FindStatIndex(player.Stats, "damage_dealt")].Value), false);
                player.CustomStats[player.FindStatIndex(player.CustomStats, "drt")].ChangeValue(GetPercentage(BlueTeam.GetTeamDamage(), player.Stats[player.FindStatIndex(player.Stats, "damage_taken")].Value), false);
            }
        }
        private void GetLogOverview()
        {
            HtmlNode details_container = doc.DocumentNode.SelectSingleNode("//div[@class='details_container']");
            HtmlNode[] details = details_container.SelectNodes(".//span[@class='log_detail']").ToArray();

            this.LogID = details[0].InnerText;
            this.dateTime = DateTime.Parse(details[1].InnerText);
            this.Name = details[2].InnerText;
            this.Server = details[3].InnerText;
            this.Map = details[4].InnerText;
            this.GameTime = details_container.SelectSingleNode(".//span[@id='time_elapsed']").InnerText;
            this.RedTeam.Score = int.Parse(details_container.SelectSingleNode(".//span[@id='red_score_value']").InnerText);
            this.BlueTeam.Score = int.Parse(details_container.SelectSingleNode(".//span[@id='blue_score_value']").InnerText);
        }

        public static float GetPercentage(float original, float comparison)
        {
            float result = 0;
            if(original > 0)
            result = comparison / original * 100;

            return result;
        }
    }
}
