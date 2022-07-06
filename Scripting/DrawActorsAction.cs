﻿using System;
using System.Collections.Generic;
using CSE210_Final.Casting;
using CSE210_Final.Scripting;
using CSE210_Final.Services;


namespace CSE210_Final.Scripting
{
    public class DrawActorsAction : CSE210_Final.Scripting.Action
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
                _videoService.ClearBuffer();
                _videoService.Draw(player);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }
    }
}