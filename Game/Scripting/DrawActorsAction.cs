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

                // Background background = scene.GetFirstActor<Background>("background");
                
                List<SolidWall> walls = scene.GetAllActors<SolidWall>("wall");
                List<Label> labels = scene.GetAllActors<Label>("label");

                // Label status = scene.GetFirstActor<Label>("status");

                // List<Image> background = scene.GetAllActors<Image>("background");

                _videoService.ClearBuffer();
                // _videoService.Draw(status);
                

                // _videoService.Draw(background, camera);


                Actor player = scene.GetFirstActor<Actor>("player");
                Skeleton boss = scene.GetFirstActor<Skeleton>("boss");
                List<Skeleton> skeletons = scene.GetAllActors<Skeleton>("skeleton");

                _endSceneLoader.EndScreen(scene, boss, player);

                
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