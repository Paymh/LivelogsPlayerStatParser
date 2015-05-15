using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivelogsPlayerStatParser
{
    public static class LogHandler
    {
        public static List<Log> logsTracked = new List<Log>();

        public static bool ParseLog(String URL)
        {
            if(logsTracked.FindIndex(o => o.URL == URL) <0)
            {
                Log log = new Log(URL);
                log.ParseLog();
                logsTracked.Add(log);
                return true;
            }
            return false;
        }

        public static List<Player> GetPlayerStats(long steamID)
        {
            List<Player> PlayerStats = new List<Player>();

            foreach(Log log in logsTracked)
            {
                foreach(Player player in log.BlueTeam.TeamPlayers)
                {
                    if (player.steamID == steamID)
                        PlayerStats.Add(player);
                }
                foreach (Player player in log.RedTeam.TeamPlayers)
                {
                    if (player.steamID == steamID)
                        PlayerStats.Add(player);
                }
            }
            return PlayerStats;
        }

        public static List<long> GetPlayerIDs()
        {
            List<long> PlayerIDs = new List<long>();
            foreach(Log log in logsTracked)
            {
                foreach(Player player in log.BlueTeam.TeamPlayers)
                {
                    if (PlayerIDs.IndexOf(player.steamID) == -1)
                        PlayerIDs.Add(player.steamID);
                }
                foreach(Player player in log.RedTeam.TeamPlayers)
                {
                    if (PlayerIDs.IndexOf(player.steamID) == -1)
                        PlayerIDs.Add(player.steamID);
                }
            }
            return PlayerIDs;
        }
    }
}
