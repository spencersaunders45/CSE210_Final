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

        private EndSceneLoader _endSceneLoader = new EndSceneLoader();
        
        public DrawActorsAction(IServiceFactory serviceFactory)
        {
            _videoService = serviceFactory.GetVideoService();

        }

        public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
        {
            try
            {
                Camera camera = scene.GetFirstActor<Camera>("camera");
                List<SolidWall> walls = scene.GetAllActors<SolidWall>("wall");
                List<Label> labels = scene.GetAllActors<Label>("label");
                _videoService.ClearBuffer();

                Skeleton boss = scene.GetFirstActor<Skeleton>("boss");
                List<Skeleton> skeletons = scene.GetAllActors<Skeleton>("skeleton");

                _endSceneLoader.EndScreen(scene, boss);
                
                // Draw Walls
                foreach (SolidWall wall in walls) 
                { _videoService.Draw(wall, camera); }
                
                // Draw Labels
                foreach (Label label in labels)
                {_videoService.Draw(label);}
                
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

        private void CheckForGameOver()
        {

        }
    }
}