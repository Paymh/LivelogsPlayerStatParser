using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LivelogsPlayerStatParser
{
    public class Player
    {
        public long steamID;
        public List<Statistics> Stats = new List<Statistics>();
        public List<Statistics> CustomStats = new List<Statistics>();
        public List<Class> ClassesPlayed = new List<Class>();

        public void SetupStats()
        {
            Stats.Add(new Statistics("Name", "name", Statistics.DataTypes.STRING));
            Stats.Add(new Statistics("Kills", "kills", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Deaths", "deaths", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Assists", "assists", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Points Captured", "captures", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Headshots", "headshots", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Points", "points", Statistics.DataTypes.DOUBLE));
            Stats.Add(new Statistics("Damage Dealt", "damage_dealt", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Damage Taken", "damage_taken", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Healing Received", "healing_received", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Overhealing Received", "overhealing_received", Statistics.DataTypes.INT));
            Stats.Add(new Statistics("Kills Per Death", "kpd", Statistics.DataTypes.DOUBLE));
            Stats.Add(new Statistics("Damage Dealt Per Death", "dpd", Statistics.DataTypes.DOUBLE));
            Stats.Add(new Statistics("Damage Per Round", "dpr", Statistics.DataTypes.DOUBLE));
            Stats.Add(new Statistics("Damage Per Minute", "dpm", Statistics.DataTypes.DOUBLE));

            CustomStats.Add(new Statistics("Heals Given", "healing_done", Statistics.DataTypes.INT));
            CustomStats.Add(new Statistics("Overhealing Given", "overhealing_done", Statistics.DataTypes.INT));
            CustomStats.Add(new Statistics("Ubers Used", "ubers_used", Statistics.DataTypes.INT));
            CustomStats.Add(new Statistics("Ubers Lost", "ubers_lost", Statistics.DataTypes.INT));
            CustomStats.Add(new Statistics("Uber Lost Percentage", "ulp", Statistics.DataTypes.FLOAT));

            CustomStats.Add(new Statistics("Heal Ratio Received", "hrr", Statistics.DataTypes.FLOAT));
            CustomStats.Add(new Statistics("Damage Ratio Given", "drg", Statistics.DataTypes.FLOAT));
            CustomStats.Add(new Statistics("Damage Ratio Taken", "drt", Statistics.DataTypes.FLOAT));
        }

        public int FindStatIndex(List<Statistics> StatList, String IdName)
        {
            return StatList.FindIndex(o => o.IdName == IdName);
        }

        public Statistics GetStat(String IDname)
        {

            if (this.Stats.First(o => o.IdName == IDname) != null)
                return this.Stats.First(o => o.IdName == IDname);
            else if (this.CustomStats.First(o => o.IdName == IDname) != null)
                return this.CustomStats.First(o => o.IdName == IDname);

                return null;
        }
        public Player()
        {
            SetupStats();
        }
    }
}
