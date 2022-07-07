using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;


public abstract class SceneLoader
{
    private static IServiceFactory ServiceFactory;

    public SceneLoader(IServiceFactory serviceFactory)
    {
        ServiceFactory = serviceFactory;
    }

    public IServiceFactory GetServiceFactory()
    {
        return ServiceFactory;
    }

    public abstract void Load(Scene scene);

}