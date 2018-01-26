using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GameManagerMock
{
    public static List<SaveGame> GetSavegames()
    {
        var saveGamelist = new List<SaveGame>();
        for (int i = 0; i < 5; i++)
        {
            saveGamelist.Add(new SaveGame());
        }

        return saveGamelist;
    }
}
