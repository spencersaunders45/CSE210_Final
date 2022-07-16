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
                // Camera
                Camera camera = scene.GetFirstActor<Camera>("camera");
                // Walls
                List<SolidWall> walls = scene.GetAllActors<SolidWall>("wall");
                List<Label> labels = scene.GetAllActors<Label>("label");

                _videoService.ClearBuffer();

                //Entities
                Actor player = scene.GetFirstActor<Actor>("player");
                Skeleton boss = scene.GetFirstActor<Skeleton>("boss");
                List<Skeleton> skeletons = scene.GetAllActors<Skeleton>("skeleton");
                //Game over label
                _endSceneLoader.EndScreen(scene, boss, player);
                //Player health label
                PlayerHealthLabel(scene);
                DrawHealthLabel(scene);
                // Draw Labels
                DrawLabels(labels);
                
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

        private void PlayerHealthLabel(Scene scene)
        {  
            int playerHealth = scene.GetFirstActor<PlayerController>("player").GetHealth();
            string newHealth = playerHealth.ToString();
            Label health = new Label();
            health.Display("Health: " + newHealth);
            health.MoveTo(0, 0);
            health.Align(Label.Left);
            scene.AddActor("health", health);
        }

        private void DrawHealthLabel(Scene scene)
        {
            List<Label> healthList = scene.GetAllActors<Label>("health");
            int length = healthList.Count;
            Label health = healthList[length-1];
            _videoService.Draw(health);
        }

        private void DrawLabels(List<Label> labels)
        {
            foreach (Label label in labels)
            {_videoService.Draw(label);}
        }
    }
}