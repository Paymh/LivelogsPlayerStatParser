using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace LivelogsPlayerStatParser
{
    public static class ExcelHandler
    {
        static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        static Workbook wb;
        static Worksheet ws;

        public static void CreateExcel()
        {
            try
            {
                wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = (Worksheet)wb.Worksheets[1];
                ws.Name = "Livelogs Output";
            }
            catch (System.Runtime.InteropServices.COMException e)
            {

            }
        }
        public static void ShowExcel()
        {
            xlApp.Visible = true;

        }

        public static void HideExcel()
        {
            xlApp.Visible = false;
        }

        public static void ChangeCell(int Column, int Row, String Value, CellType type)
        {
            try
            {
                ws.Cells[Row, Column].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                if (type == CellType.Header)
                {
                    ws.Cells[Row, Column].Font.Bold = true;
                    ws.Cells[Row, Column].Font.Size = 12;
                }
                else if (type == CellType.Stat)
                {
                    ws.Cells[Row, Column].Font.Bold = false;
                    ws.Cells[Row, Column].Font.Size = 8;
                }
                else if (type == CellType.Average)
                {
                    ws.Cells[Row, Column].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    ws.Cells[Row, Column].Font.Size = 8;
                }
                else if (type == CellType.Name)
                {
                    ws.Cells[Row, Column].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    ws.Cells[Row, Column].Font.Size = 8;
                }
                ws.Cells[Row, Column] = Value;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {

            }
        }

        public static void OutputExcel(bool full)
        {
            int currentRow = 1, currentColumn = 1;
            for (int i = 0; i < LogHandler.logsTracked[0].BlueTeam.TeamPlayers[0].Stats.Count; i++) //Loop each stat
            {
                ChangeCell(currentColumn, currentRow, LogHandler.logsTracked[0].BlueTeam.TeamPlayers[0].Stats[i].FriendlyName, CellType.Header); //Output headers
                currentColumn++;
            }
            for (int i = 0; i < LogHandler.logsTracked[0].BlueTeam.TeamPlayers[0].CustomStats.Count; i++) //Loop each stat
            {
                ChangeCell(currentColumn, currentRow, LogHandler.logsTracked[0].BlueTeam.TeamPlayers[0].CustomStats[i].FriendlyName, CellType.Header); //Output headers
                currentColumn++;
            }
            currentRow++;
            currentColumn = 1;
            foreach (Player player in LogHandler.GetPlayers())
            {
                double logWeighting = 0;
                List<Player> playerStats = LogHandler.GetPlayerStats(player.steamID, ref logWeighting);
                Player averages = new Player();

                for (int i = 0; i < playerStats.Count; i++) //Loop each log player played
                {
                    if (i == 0)
                    {
                        ChangeCell(currentColumn, currentRow, playerStats[i].Stats[0].Value.ToString(), CellType.Name);
                        if (full)
                            currentRow++;
                    }
                    String classesPlayed = "";
                    foreach (Class classes in playerStats[i].ClassesPlayed)
                    {
                        classesPlayed += classes.classType.ToString() + " | ";
                    }
                    if (full)
                        ChangeCell(currentColumn, currentRow, classesPlayed, CellType.Stat);
                    for (int j = 1; j < playerStats[i].Stats.Count; j++) //Output stats and add to average
                    {
                        currentColumn++;
                        if (full)
                            ChangeCell(currentColumn, currentRow, Convert.ToString(playerStats[i].Stats[j].Value), CellType.Stat);
                        averages.Stats[j].ChangeValue(playerStats[i].Stats[j].Value, true);
                    }
                    currentColumn++;
                    for (int k = 0; k < playerStats[i].CustomStats.Count; k++) //Output custom stats and add to average
                    {
                        if (full)
                            ChangeCell(currentColumn, currentRow, Convert.ToString(playerStats[i].CustomStats[k].Value), CellType.Stat);
                        currentColumn++;
                        averages.CustomStats[k].ChangeValue(playerStats[i].CustomStats[k].Value, true);
                    }
                    if (full)
                        currentRow++;
                    currentColumn = 1;
                }
                currentColumn = 2;
                //Output averages
                if (playerStats.Count == 1)
                    logWeighting = 1;
                for (int j = 1; j < averages.Stats.Count; j++) //Output averages
                {
                    if (averages.Stats[j].IdName == "dpm" || averages.Stats[j].IdName == "dpd" || averages.Stats[j].IdName == "dpr" || averages.Stats[j].IdName == "kpd")
                        ChangeCell(currentColumn, currentRow, Math.Round((averages.Stats[j].Value / playerStats.Count), 2).ToString(), CellType.Average);
                    else
                        ChangeCell(currentColumn, currentRow, Math.Round((averages.Stats[j].Value / logWeighting), 2).ToString(), CellType.Average);
                    currentColumn++;
                }
                for (int k = 0; k < averages.CustomStats.Count; k++) //Output custom averages
                {
                    if (averages.CustomStats[k].IdName == "hrr" || averages.CustomStats[k].IdName == "drg" || averages.CustomStats[k].IdName == "drt")
                        ChangeCell(currentColumn, currentRow, Math.Round((averages.CustomStats[k].Value / playerStats.Count), 2).ToString(), CellType.Average);
                    else if (averages.CustomStats[k].IdName == "ulp")
                    {
                        if (averages.CustomStats[3].Value > 0 && averages.CustomStats[3].Value > 0)
                            ChangeCell(currentColumn, currentRow, Math.Round((((double)averages.CustomStats[3].Value / (double)averages.CustomStats[2].Value)) * 100, 2).ToString(), CellType.Average);
                        else
                            ChangeCell(currentColumn, currentRow, "0", CellType.Average);
                    }

                    else
                        ChangeCell(currentColumn, currentRow, Math.Round((averages.CustomStats[k].Value / logWeighting), 2).ToString(), CellType.Average);
                    currentColumn++;
                }
                if (full)
                    currentRow++;
                else
                    currentRow += 2;
                currentColumn = 1;
            }
            xlApp.Columns.AutoFit();
        }

        public enum CellType
        {
            Header,
            Stat,
            Average,
            Name
        }
    }
}
