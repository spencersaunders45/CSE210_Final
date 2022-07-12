﻿using System.Numerics;
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
        PlayMusicAction playMusicAction = new PlayMusicAction(serviceFactory);
        
        
        
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
        Actor screen = new Actor();
        screen.SizeTo(640, 480);
        screen.MoveTo(0, 0);

        Actor world = new Actor();
        world.SizeTo(1280, 960);
        world.MoveTo(0, 0);
        Camera camera = new Camera(playerImage, screen, world);

        scene.AddActor("player-image", playerImage);
        scene.AddActor("player" , playerController);
        scene.AddActor("label", back);
        scene.AddActor("camera", camera);
        
        
        //Skeletons
        Skeleton boss = new Skeleton(500, 340, Vector2.One * 24, Color.Red(), 24);
        Skeleton skeleton1 = new Skeleton(0, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton2 = new Skeleton(20, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton3 = new Skeleton(40, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton4 = new Skeleton(60, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton5 = new Skeleton(80, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton6 = new Skeleton(100, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton7 = new Skeleton(120, 0, Vector2.One * 16, Color.Green(), 24);
        Skeleton skeleton8 = new Skeleton(140, 0, Vector2.One * 16, Color.Green(), 24);
        scene.AddActor("boss" , boss);
        scene.AddActor("skeleton" , skeleton1);
        scene.AddActor("skeleton" , skeleton2);
        scene.AddActor("skeleton" , skeleton3);
        scene.AddActor("skeleton" , skeleton4);
        scene.AddActor("skeleton" , skeleton5);
        scene.AddActor("skeleton" , skeleton6);
        scene.AddActor("skeleton" , skeleton7);
        scene.AddActor("skeleton" , skeleton8);

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
        scene.AddAction(Phase.Output, playMusicAction);
        scene.AddAction(Phase.Output, animatePlayerAction);
        scene.AddAction(Phase.Output, drawActorsAction);
    }
}