﻿using System;
using System.Collections.Generic;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;


namespace CSE210_Final.Game.Scripting
{
    public class DrawActorsAction : CSE210_Final.Game.Scripting.Action
    {
        private IVideoService _videoService;

        public DrawActorsAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();
        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                PlayerController player = scene.GetFirstActor<PlayerController>("player");
                List<SolidWall> walls = scene.GetAllActors<SolidWall>("wall");
                _videoService.ClearBuffer();
                _videoService.Draw(player); // Draw Player
                foreach (SolidWall wall in walls) // Draw Walls
                {
                    _videoService.Draw(wall);
                }
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}