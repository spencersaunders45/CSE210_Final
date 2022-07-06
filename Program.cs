using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Directing;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;

namespace CSE210_Final;

public static class Program
{
    public static void Main(string[] args)
    {
        IServiceFactory serviceFactory = new RaylibServiceFactory();
        Scene testScene = new Scene();

        //-------------------Testing-------------------
        serviceFactory.GetVideoService().SetBackground(Color.Black());
        PlayerController playerController = new PlayerController(Vector2.One * 100, Vector2.One * 30, Color.Green(), testScene);
        testScene.AddActor("player" , playerController);

        for (int i = 0; i < 4; i++)
        {
            testScene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 100, 200), Vector2.One * 32, Color.Purple(), testScene));
        }
        
        //-------------------End testing-------------------

        // Update
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);
        testScene.AddAction(Phase.Input, updateActorsAction);
        
        // Draw
        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        testScene.AddAction(Phase.Output ,drawActorsAction);
        
        // Start Game
        Director director = new Director(serviceFactory);
        director.Direct(testScene);
    }
}