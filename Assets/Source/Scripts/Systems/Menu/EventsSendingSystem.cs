using Kuhpik;
using System.Collections.Generic;

public class EventsSendingSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        var levelName = game.environment.name.Replace("(Clone)", "").Trim();

        var @params = new Dictionary<string, object>()
        {
            { "level_number", player.level + 1 },
            { "level_name", levelName },
            { "level_count", player.levelsPlayed + 1 },
            { "level_diff", game.characters.Length - 1 },
            { "level_loop", game.levelLoop },
            { "level_random", 0},
            { "level_type", game.levelType }
        };

        AppMetrica.Instance.ReportEvent("level_start", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }
}
