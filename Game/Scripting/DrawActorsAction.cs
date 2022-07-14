using System;
using System.Collections.Generic;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;


namespace CSE210_Final.Game.Scripting
{
    /// <summary>
    /// Handles drawing *most* of the actors.
    /// </summary>
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
                Camera camera = scene.GetFirstActor<Camera>("camera");

                // Background background = scene.GetFirstActor<Background>("background");
        // Actor world = camera.GetWorld();
                List<SolidWall> walls = scene.GetAllActors<SolidWall>("wall");
                List<Label> labels = scene.GetAllActors<Label>("label");

                // List<Image> background = scene.GetAllActors<Image>("background");

                _videoService.ClearBuffer();
                

                // _videoService.Draw(background, camera);


                Skeleton boss = scene.GetFirstActor<Skeleton>("boss");
                List<Skeleton> skeletons = scene.GetAllActors<Skeleton>("skeleton");

                
                
                // Draw Walls
                foreach (SolidWall wall in walls) 
                { _videoService.Draw(wall, camera); }
                
                // Draw Labels
                foreach (Label label in labels)
                {_videoService.Draw(label);}
                
                //Draw Skeletons
                // foreach (Skeleton skeleton in skeletons)
                // {_videoService.Draw(skeleton);}
                // if(boss != null)
                //     _videoService.Draw(boss);
                
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