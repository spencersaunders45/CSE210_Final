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
        Scene scene = new Scene();
        
        // Scene Loaders are created to handle loading specific scenes.
        TitleSceneLoader titleSceneLoader = new TitleSceneLoader(serviceFactory);
        titleSceneLoader.Load(scene);

        // Start Game
        Director director = new Director(serviceFactory);
        director.Direct(scene);
    }
}