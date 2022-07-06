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
        PlayerController playerController = new PlayerController(Vector2.One * 100, Vector2.One * 30, Color.Green());
        testScene.AddActor("player" , playerController);
        //-------------------End testing-------------------

        // Actors
        Skeleton boss = new Skeleton(Vector2.One * 0, Vector2.One * 5, Color.Green());
        Skeleton skeleton1 = new Skeleton(Vector2.One * 10, Vector2.One * 5, Color.Green());
        Skeleton skeleton2 = new Skeleton(Vector2.One * 20, Vector2.One * 5, Color.Green());
        Skeleton skeleton3 = new Skeleton(Vector2.One * 30, Vector2.One * 5, Color.Green());
        Skeleton skeleton4 = new Skeleton(Vector2.One * 40, Vector2.One * 5, Color.Green());
        Skeleton skeleton5 = new Skeleton(Vector2.One * 50, Vector2.One * 5, Color.Green());
        Skeleton skeleton6 = new Skeleton(Vector2.One * 60, Vector2.One * 5, Color.Green());
        Skeleton skeleton7 = new Skeleton(Vector2.One * 70, Vector2.One * 5, Color.Green());
        Skeleton skeleton8 = new Skeleton(Vector2.One * 80, Vector2.One * 5, Color.Green());
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