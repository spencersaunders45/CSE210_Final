using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

public class TitleSceneLoader : SceneLoader
{
    public TitleSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { }

    public override void Load(Scene scene)
    {
        GetServiceFactory().GetVideoService().SetBackground(Color.Black());
        
        Label title = new Label();
        title.Display("Save Your Dog!");
        title.MoveTo(320, 200);
        title.Align(Label.Center);
        
        Label instructions = new Label();
        instructions.Display("press 's' to start game");
        instructions.MoveTo(320, 240);
        instructions.Align(Label.Center);
        
        IServiceFactory serviceFactory = GetServiceFactory();
        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);

        GameSceneLoader gameSceneLoader = new GameSceneLoader(serviceFactory);
        
        // SceneTransitionAction handles grabbing user input to transition between different scenes.
        SceneTransitionAction sceneTransitionAction =
            new SceneTransitionAction(serviceFactory, gameSceneLoader, KeyboardKey.S);
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);
        
        scene.Clear();
        scene.AddActor("label", title);
        scene.AddActor("label", instructions);
        scene.AddAction(Phase.Output, drawActorsAction);
        scene.AddAction(Phase.Input, updateActorsAction);
        scene.AddAction(Phase.Input, sceneTransitionAction);
    }
}