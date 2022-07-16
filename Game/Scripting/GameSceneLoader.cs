using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;
/// <summary>
/// Handles loading the main game scene
/// </summary>
public class GameSceneLoader : SceneLoader
{
    private IVideoService _videoService;
    public GameSceneLoader(IServiceFactory serviceFactory) : base(serviceFactory) { 
        _videoService = serviceFactory.GetVideoService();
    }


    public override void Load(Scene scene)
    {
        IServiceFactory serviceFactory = GetServiceFactory();
        serviceFactory.GetVideoService().SetBackground(Color.Black());
         

        // Define Actors
        PlayerController playerController = new PlayerController(Vector2.One * 220, new Vector2(16, 32), Color.Green(), scene, 200);
        TitleSceneLoader titleSceneLoader = new TitleSceneLoader(serviceFactory);
        Image playerImage = new Image();
        PlayMusicAction playMusicAction = new PlayMusicAction(serviceFactory);

        DrawImageAction drawImageAction = new DrawImageAction(serviceFactory);
        
        
        
        // Define Actions
        UpdateActorsAction updateActorsAction = new UpdateActorsAction(serviceFactory);
        SceneTransitionAction sceneTransitionAction =
            new SceneTransitionAction(serviceFactory, titleSceneLoader, KeyboardKey.Enter);
        DrawActorsAction drawActorsAction = new DrawActorsAction(serviceFactory);
        AnimatePlayerAction animatePlayerAction = new AnimatePlayerAction(serviceFactory, playerController, playerImage);
        DrawSkeletonAction drawSkeletonAction = new DrawSkeletonAction(serviceFactory);
        Label back = new Label();

        Image background = new Image();
        background.SizeTo(1280, 960);
        background.MoveTo(0, 0);
        background.Display("Assets/Images/background/IMAGE_SaveYourDog_Map.png");

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

        scene.AddActor("background", background);


        scene.AddActor("player-image", playerImage);
        scene.AddActor("player" , playerController);
        scene.AddActor("label", back);
        scene.AddActor("camera", camera);

        
        // Add Skeletons
        Skeleton boss = new Skeleton(1070, 286, Vector2.One * 24, Color.Red(), 16, scene, serviceFactory, true);
        scene.AddActor("boss" , boss);
        // for(int i = 0; i < 8; i++)
        // {
        //     int[] location = Constants.SEKELETON_LOCATIONS[i];
        //     Skeleton skeleton = new Skeleton(location[0], location[1], Vector2.One * 24, Color.Green(), 3, scene, serviceFactory, false);
        //     scene.AddActor("skeleton" , skeleton);
        // }
        
        //Populate level w/ walls
        // 0, 1, 2, 3, 4, 5, 10, 15, 20, 25, 30, 35, 40, 41, 42, 43, 44, 45, 50, 51, 52, 53, 54, 55
        CSVReader csvReader= new CSVReader();

        string[][] map = csvReader.ReadCSV("Assets/Tiled/CSV_SaveYourDog_Tile Layer 1.csv");
        string[][] skeletonSpawns = csvReader.ReadCSV("Assets/Tiled/CSV_SaveYourDog_Tile Layer 2.csv");
        string[] wallChecks = new[]
        {
            "0", "1", "2", "3", "4", "5", "10", "15", "20", "25", "30", "35", "40", "41", "42",
            "43", "44", "45", "50", "51", "52", "53", "54", "55"
        };
        for (int x = 0; x < map.Length; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                if (csvReader.CheckList(wallChecks, map[x][y]))
                {
                    scene.AddActor("wall", new SolidWall(new Vector2(y*16, x*16), Vector2.One * 16, Color.Purple(), scene));
                }
            }
        }

        for (int x = 0; x < skeletonSpawns.Length; x++)
        {
            for (int y = 0; y < skeletonSpawns[x].Length; y++)
            {
                if (skeletonSpawns[x][y] == "77")
                {
                    Skeleton skeleton = new Skeleton(y*16, x*16, Vector2.One * 24, Color.White(), 3, scene, serviceFactory, false);
                    scene.AddActor("skeleton" , skeleton);
                }
            }
        }
        
        // for (int i = 0; i < 20; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2(330, (i * 32)), Vector2.One * 32, Color.Purple(), scene));
        // }
        //
        // for (int i = 0; i < 23; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2(530, (i * 32) + 300), Vector2.One * 32, Color.Purple(), scene));
        // }
        //
        // for (int i = 0; i < 11; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2(710, (i * 32)), Vector2.One * 32, Color.Purple(), scene));
        // }
        // for (int i = 0; i < 11; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2(910, (i * 32) + 350), Vector2.One * 32, Color.Purple(), scene));
        // }
        //
        // for (int i = 0; i < 8; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 710, 164), Vector2.One * 32, Color.Purple(), scene));
        // }
        // for (int i = 0; i < 12; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 540, 520), Vector2.One * 32, Color.Purple(), scene));
        // }
        // for (int i = 0; i < 12; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 920, 420), Vector2.One * 32, Color.Purple(), scene));
        // }
        //
        // for (int i = 0; i < 12; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2((i * 32) + 920, 100), Vector2.One * 32, Color.Purple(), scene));
        // }
        // for (int i = 0; i < 3; i++)
        // {
        //     scene.AddActor("wall", new SolidWall(new Vector2(940, (i * 32) + 100), Vector2.One * 32, Color.Purple(), scene));
        // }

        SkeletonHandler skeletonHandler = new SkeletonHandler();
        
        // Add Actions
        scene.AddAction(Phase.Update, updateActorsAction);
        scene.AddAction(Phase.Input, sceneTransitionAction);
        scene.AddAction(Phase.Output, playMusicAction);
        scene.AddAction(Phase.Output, drawActorsAction);
        scene.AddAction(Phase.Output, animatePlayerAction);
        scene.AddAction(Phase.Output, drawSkeletonAction);
        scene.AddAction(Phase.Update, skeletonHandler);
    }

}