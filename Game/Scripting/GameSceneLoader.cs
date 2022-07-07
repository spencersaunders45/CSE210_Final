using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

public class GameSceneLoader : SceneLoader
{
    public GameSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { }

    public override void Load(Scene scene)
    {
        IServiceFactory serviceFactory = GetServiceFactory();
        serviceFactory.GetVideoService().SetBackground(Color.Black());

        PlayerController playerController = new PlayerController(Vector2.One * 100, Vector2.One * 30, Color.Green(), scene);

        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);

        TitleSceneLoader titleSceneLoader = new TitleSceneLoader(serviceFactory);
        SceneTransitionAction sceneTransitionAction =
            new SceneTransitionAction(serviceFactory, titleSceneLoader, KeyboardKey.Enter);
        
        Label back = new Label();
        back.Display("press 'enter' to go back to main screen");
        back.MoveTo(0, 0);
        back.Align(Label.Left);
        
        scene.Clear();
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
        
        scene.AddAction(Phase.Output, drawActorsAction);
        scene.AddAction(Phase.Input, updateActorsAction);
        scene.AddAction(Phase.Input, sceneTransitionAction);
    }
}