using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

/// <summary>
/// Handles loading the title scene.
/// </summary>
public class TitleSceneLoader : SceneLoader
{
    public TitleSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { }

    public override void Load(Scene scene)
    {
        GetServiceFactory().GetVideoService().SetBackground(Color.Black());
        
        // Label title = new Label();
        // title.Display("Save Your Dog!");
        // title.MoveTo(320, 200);
        // title.Align(Label.Center);
        
        // Label instructions = new Label();
        // instructions.Display("press 's' to start game");
        // instructions.MoveTo(320, 240);
        // instructions.Align(Label.Center);

        Image background = new Image();
        background.SizeTo(640, 480);
        background.MoveTo(0, 0);
        background.Display("Assets/Images/background/TitleScreen.png");
        
        IServiceFactory serviceFactory = GetServiceFactory();
        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);

        GameSceneLoader gameSceneLoader = new GameSceneLoader(serviceFactory);
        
        // SceneTransitionAction handles grabbing user input to transition between different scenes.
        SceneTransitionAction sceneTransitionAction =
            new SceneTransitionAction(serviceFactory, gameSceneLoader, KeyboardKey.S);
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);
        
        scene.Clear();
        scene.AddActor("background", background);
        DrawImageAction drawImageAction = new DrawImageAction(serviceFactory);
        // scene.AddActor("label", title);
        // scene.AddActor("label", instructions);

        scene.AddAction(Phase.Output, drawImageAction);
        // scene.AddAction(Phase.Output, drawActorsAction);
        scene.AddAction(Phase.Input, updateActorsAction);
        scene.AddAction(Phase.Input, sceneTransitionAction);
    }
}