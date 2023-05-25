using Microsoft.EntityFrameworkCore;

namespace Arcaea.Premium;

public class Ptt
{
    public static double CalcBest30()
    {
        var app = DataBase.AppDataBase;
        var scores = app.Scores.ToList();
        var ptts = new List<double>();
        double total = 0;
        foreach (var score in scores)
        {
            var id = score.SongId;
            var s = score.Score1;
            var diff = score.SongDifficulty;
            var filter = app.SongList
                .Include(x => x.ConstantValueTuples)
                .Where(x => x.Id == id).ToList();
            if (filter.Count > 0)
            {
                ptts.Add(CalcSongPtt(Convert.ToInt32(s), filter[0].ConstantValueTuples[Convert.ToInt32(diff)].Constant));
            }
        }

        total = ptts.OrderByDescending(x => x).Take(30).Concat(ptts.OrderByDescending(x => x).Take(10)).Sum();
        
        return total / 40;
    }

    public static double CalcSongPtt(long score, double constant)
    {
        double targetPtt;
        if (score >= 10000000)
        {
            targetPtt = constant + 2.0f;
        }
        else if (score >= 9800000)
        {
            targetPtt = constant + 1.0f + (double)(score - 9800000) / 200000;
        }
        else if (score > 0)
        {
            targetPtt = constant + (double)(score - 9500000) / 300000;
        }
        else
        {
            targetPtt = 0.0f;
        }
        return targetPtt > 0.0f ? targetPtt : 0.0f;
    }
}