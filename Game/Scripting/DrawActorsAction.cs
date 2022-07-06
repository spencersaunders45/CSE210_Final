using System;
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
                List<Skeleton> allSkeletons = scene.GetAllActors<Skeleton>("skeleton");
                Skeleton boss = scene.GetFirstActor<Skeleton>("boss");
                _videoService.ClearBuffer();
                allSkeletons.ForEach(DrawSkeletons);
                _videoService.Draw(boss);
                _videoService.Draw(player);
                _videoService.FlushBuffer();
            }
            catch (Exception exception)
            {
                callback.OnError("Couldn't draw actors.", exception);
            }
        }

        private void DrawSkeletons(Skeleton skeleton)
        {
            _videoService.Draw(skeleton);
        }
    }
}