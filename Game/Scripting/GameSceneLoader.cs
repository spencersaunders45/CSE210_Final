using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;
/// <summary>
/// Handles loading the main game scene
/// </summary>
public class GameSceneLoader : SceneLoader
{
    public GameSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { }

    public override void Load(Scene scene)
    {
        IServiceFactory serviceFactory = GetServiceFactory();
        serviceFactory.GetVideoService().SetBackground(Color.Black());

        // Define Actors
        PlayerController playerController = new PlayerController(Vector2.One * 100, new Vector2(16, 32), Color.Green(), scene);
        TitleSceneLoader titleSceneLoader = new TitleSceneLoader(serviceFactory);
        Image playerImage = new Image();
        
        // Define Actions
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);
        SceneTransitionAction sceneTransitionAction =
            new SceneTransitionAction(serviceFactory, titleSceneLoader, KeyboardKey.Enter);
        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        AnimatePlayerAction animatePlayerAction = new AnimatePlayerAction(serviceFactory, playerController, playerImage);
        Label back = new Label();
        back.Display("press 'enter' to go back to main screen");
        back.MoveTo(0, 0);
        back.Align(Label.Left);
        
        scene.Clear();
        
        // Add Actors
        scene.AddActor("player-image", playerImage);
        scene.AddActor("player" , playerController);
        scene.AddActor("label", back);

        for (int i = 0; i < 8; i++)
        {
            scene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 100, 200), Vector2.One * 32, Color.Purple(), scene));
        }
        for (int i = 0; i < 8; i++)
        {
            scene.AddActor("wall", new SolidWall(new Vector2(164, (i * 32) + 150), Vector2.One * 32, Color.Purple(), scene));
        }
        
        // Add Actions
        scene.AddAction(Phase.Input, updateActorsAction);
        scene.AddAction(Phase.Input, sceneTransitionAction);
        scene.AddAction(Phase.Output, animatePlayerAction);
        scene.AddAction(Phase.Output, drawActorsAction);
    }
}