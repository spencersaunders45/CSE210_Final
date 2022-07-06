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
        //TODO: Initialize game entry point here.
        IServiceFactory serviceFactory = new RaylibServiceFactory();
        Scene testScene = new Scene();

        //-------------------Testing-------------------
        serviceFactory.GetVideoService().SetBackground(Color.Black());
        PlayerController playerController = new PlayerController(Vector2.One * 100, Vector2.One * 16, Color.Blue());
        testScene.AddActor("player" , playerController);
        //-------------------End testing-------------------

        // Actors
        Skeleton boss = new Skeleton(500, 340, Vector2.One * 24, Color.Red());
        Skeleton skeleton1 = new Skeleton(0, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton2 = new Skeleton(20, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton3 = new Skeleton(40, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton4 = new Skeleton(60, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton5 = new Skeleton(80, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton6 = new Skeleton(100, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton7 = new Skeleton(120, 0, Vector2.One * 16, Color.Green());
        Skeleton skeleton8 = new Skeleton(140, 0, Vector2.One * 16, Color.Green());
        testScene.AddActor("boss" , boss);
        testScene.AddActor("skeleton" , skeleton1);
        testScene.AddActor("skeleton" , skeleton2);
        testScene.AddActor("skeleton" , skeleton3);
        testScene.AddActor("skeleton" , skeleton4);
        testScene.AddActor("skeleton" , skeleton5);
        testScene.AddActor("skeleton" , skeleton6);
        testScene.AddActor("skeleton" , skeleton7);
        testScene.AddActor("skeleton" , skeleton8);

        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        testScene.AddAction(Phase.Output ,drawActorsAction);
        
        Director director = new Director(serviceFactory);
        director.Direct(testScene);
    }
}