using System.Numerics;
using CSE210_Final.Casting;
using CSE210_Final.Directing;
using CSE210_Final.Scripting;
using CSE210_Final.Services;

namespace CSE210_Final;

public static class Program
{
    public static void Main(string[] args)
    {
        //TODO: Initialize game entry point here.
        IServiceFactory serviceFactory = new RaylibServiceFactory();
        Scene testScene = new Scene();

        //-------------------Testing-------------------
        serviceFactory.GetVideoService().SetBackground(Color.Black());
        PlayerController playerController = new PlayerController(Vector2.One * 100, Vector2.One * 30, Color.Green());
        testScene.AddActor("player" , playerController);
        //-------------------End testing-------------------

        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        testScene.AddAction(Phase.Output ,drawActorsAction);
        
        Director director = new Director(serviceFactory);
        director.Direct(testScene);
    }
}