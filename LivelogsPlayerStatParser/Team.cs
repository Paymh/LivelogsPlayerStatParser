using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LivelogsPlayerStatParser
{
    public class Team
    {
        public List<Player> TeamPlayers = new List<Player>();
        public int Score;

        public int GetTeamHealing()
        {
            int healing = 0;
            foreach (Player player in TeamPlayers)
            {
                if (player.ClassesPlayed.FindIndex(o => o.classType == Class.ClassTypes.Medic) >= 0)
                    healing += Convert.ToInt32(player.CustomStats[player.FindStatIndex(player.CustomStats, "healing_done")].Value) + Convert.ToInt32(player.CustomStats[player.FindStatIndex(player.CustomStats, "overhealing_done")].Value);
            }
            return healing;
        }

        public int GetTeamDamage()
        {
            int damage = 0;
            foreach (Player player in TeamPlayers)
            {
                damage += Convert.ToInt32(player.Stats[player.FindStatIndex(player.Stats, "damage_dealt")].Value);
            }
            return damage;
        }

        public Player GetPlayer(long steamID)
        {
            if (TeamPlayers.FindIndex(o => o.steamID == steamID) > 0)
                return TeamPlayers.Find(o => o.steamID == steamID);

            return null;
        }

    }
}
