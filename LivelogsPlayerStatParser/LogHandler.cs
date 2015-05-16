using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LivelogsPlayerStatParser
{
    public static class LogHandler
    {
        public static List<Log> logsTracked = new List<Log>();
        public static bool ParseLog(String URL, double weighting)
        {
            if(logsTracked.FindIndex(o => o.URL == URL) <0)
            {
                Log log = new Log(URL,weighting);
                log.ParseLog();
                logsTracked.Add(log);
                logsTracked = logsTracked.OrderBy(o => o.dateTime).ToList();
                return true;
            }
            return false;
        }

        public static List<Player> GetPlayerStats(long steamID, ref double logWeighting)
        {
            List<Player> PlayerStats = new List<Player>();
            double newLogWeighting = 0;
            foreach(Log log in logsTracked)
            {
                
                foreach(Player player in log.BlueTeam.TeamPlayers)
                {
                    if (player.steamID == steamID)
                    {
                        newLogWeighting += log.Weighting;
                        PlayerStats.Add(player);
                    }
                        
                }
                foreach (Player player in log.RedTeam.TeamPlayers)
                {
                    if (player.steamID == steamID)
                    {
                        newLogWeighting += log.Weighting;
                        PlayerStats.Add(player);
                    }   
                }
            }
            logWeighting = newLogWeighting;
            return PlayerStats;
        }

        public static List<Player> GetPlayers()
        {
            List<Player> Players = new List<Player>();
            foreach(Log log in logsTracked)
            {
                foreach(Player player in log.BlueTeam.TeamPlayers)
                {
                    if (Players.FindIndex(o => o.steamID == player.steamID) < 0)
                        Players.Add(player);
                }
                foreach(Player player in log.RedTeam.TeamPlayers)
                {
                    if (Players.FindIndex(o => o.steamID == player.steamID) < 0)
                        Players.Add(player);
                }
            }
            Players = Players.OrderBy(o => o.Stats[0].Value).ToList() ;
            return Players;
        }
    }
}
