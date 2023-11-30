using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;

namespace PlayersLog;

public class PlayersLog : BasePlugin
{
    public override string ModuleAuthor => "ScarWxlf";
    public override string ModuleName => "PlayersLog";
    public override string ModuleVersion => "1.0.1";
     
    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerConnect>(OnPlayerConnect);
        RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
    }

    [GameEventHandler]
    public HookResult OnPlayerConnect(EventPlayerConnect @event, GameEventInfo info)
    {
        var playerName = @event.Name;
        var isAdmin = AdminManager.PlayerHasPermissions(@event.Userid, "@css/generic");
        var data = DateTime.Now.ToString("M:MM:yyyy");
        var time = DateTime.Now.ToString("HH:mm:ss");
        var path = "PlayersLog.txt";
        var text = isAdmin ? $"Admin {playerName} connected, data:[{data}], time:[{time}]" : $"{playerName} connected, data:[{data}], time:[{time}]";
        if (!File.Exists(path))
            File.Create(path).Close();
        File.AppendAllText(path, text + Environment.NewLine);
        return HookResult.Continue;
    }

    [GameEventHandler]
    public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        var playerName = @event.Name;
        var isAdmin = AdminManager.PlayerHasPermissions(@event.Userid, "@css/generic");
        var data = DateTime.Now.ToString("M:MM:yyyy");
        var time = DateTime.Now.ToString("HH:mm:ss");
        var path = "PlayersLog.txt";
        var text = isAdmin ? $"Admin {playerName} disconnected, data:[{data}], time:[{time}]" : $"{playerName} disconnected, data:[{data}], time:[{time}]";
        if (!File.Exists(path))
            File.Create(path).Close();
        File.AppendAllText(path, text + Environment.NewLine);
        return HookResult.Continue;
    }
}