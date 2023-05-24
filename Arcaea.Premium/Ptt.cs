using Microsoft.EntityFrameworkCore;

namespace Arcaea.Premium;

public class Ptt
{
    public static double CalcBest30()
    {
        var app = DataBase.AppDataBase;
        var exp = from score in app.Scores
                  orderby score.Score1 descending
                  select score;
        var scores = exp.ToList();
        scores = scores.Count > 30 ? scores.Take(30).ToList() : scores;
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
                total += CalcSongPtt(Convert.ToInt32(s), filter[0].ConstantValueTuples[Convert.ToInt32(diff)].Constant);
            }
        }

        foreach (var score in scores.Take(10))
        {
            var id = score.SongId;
            var s = score.Score1!.Value;
            var diff = score.SongDifficulty;
            var filter = app.SongList
                .Include(x => x.ConstantValueTuples)
                .Where(x => x.Id == id).ToList();
            if (filter.Count > 0)
            {
                total += CalcSongPtt(s, filter[0].ConstantValueTuples[Convert.ToInt32(diff)].Constant);
            }
        }

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
            targetPtt = constant + 1.0f + (score - 9800000.0f) / 200000.0f;
        }
        else if (score > 0)
        {
            targetPtt = constant + (score - 9500000.0f) / 300000.0f;
        }
        else
        {
            targetPtt = 0.0f;
        }
        return targetPtt > 0.0f ? targetPtt : 0.0f;
    }
}