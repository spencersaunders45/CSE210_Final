using System;
using System.Collections.Generic;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

public class UpdateActorsAction : CSE210_Final.Game.Scripting.Action
{
    private IServiceFactory _serviceFactory;
    
    public UpdateActorsAction(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
    {
        try
        {
            PlayerController player = scene.GetFirstActor<PlayerController>("player");
            player.Update(_serviceFactory);
        }
        catch (Exception exception)
        {
            callback.OnError("Couldn't draw actors.", exception);
        }
    }
}