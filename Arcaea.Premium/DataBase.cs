using Arcaea.Premium.Models.DataModels;
using Arcaea.Premium.Models.StModels;

namespace Arcaea.Premium;

public class DataBase
{
    private static AppDataContext? _appData;

    public static AppDataContext AppDataBase
    {
        get
        {
            if (_appData is null)
            {
                _appData = new AppDataContext();
            }

            _appData.Database.EnsureCreated();
            return _appData;
        }
    }

    private static St3Context? _st3Data;

    public static St3Context St3DataBase
    {
        get { return _st3Data ??= new St3Context(); }
    }
}