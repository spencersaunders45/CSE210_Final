using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

public class SceneTransitionAction : Action
{
    private IKeyboardService _keyboardService;
    private SceneLoader _transitionTo;
    private KeyboardKey _transitionButton;
    
    public SceneTransitionAction(IServiceFactory serviceFactory, SceneLoader transitionTo, KeyboardKey transitionButton)
    {
        _keyboardService = serviceFactory.GetKeyboardService();
        _transitionTo = transitionTo;
        _transitionButton = transitionButton;
    }

    public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
    {
        if (_keyboardService.IsKeyPressed(_transitionButton))
        {
            _transitionTo.Load(scene);
        }
    }
}