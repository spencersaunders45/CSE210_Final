using System.Numerics;
using CSE210_Final.Game.Casting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Scripting;

/// <summary>
/// Handles animating the PlayerController actor.
/// </summary>
public class AnimatePlayerAction : Action
{
    private IVideoService _videoService;

    private String[] _idle = new String[6];
    private String[] _run = new String[6];
    private String[] _attack = new String[5];
    private PlayerController _player;
    private Image _image;

    public AnimatePlayerAction(IServiceFactory serviceFactory, PlayerController player, Image playerImage)
    {
        _videoService = serviceFactory.GetVideoService();
        _player = player;
        _image = playerImage;
        _image.SizeTo(new Vector2(16, 32));
        
        _run[0] = "Assets/Images/Player/Run/Player_Run_0.png";
        _run[1] = "Assets/Images/Player/Run/Player_Run_1.png";
        _run[2] = "Assets/Images/Player/Run/Player_Run_2.png";
        _run[3] = "Assets/Images/Player/Run/Player_Run_3.png";
        _run[4] = "Assets/Images/Player/Run/Player_Run_4.png";
        _run[5] = "Assets/Images/Player/Run/Player_Run_5.png";

        _idle[0] = "Assets/Images/Player/Idle/Player_Idle_1.png";
        _idle[1] = "Assets/Images/Player/Idle/Player_Idle_2.png";
        _idle[2] = "Assets/Images/Player/Idle/Player_Idle_3.png";
        _idle[3] = "Assets/Images/Player/Idle/Player_Idle_4.png";
        _idle[4] = "Assets/Images/Player/Idle/Player_Idle_5.png";
        _idle[5] = "Assets/Images/Player/Idle/Player_Idle_6.png";

        _attack[0] = "Assets/Images/Player/Attack/Player_Attack_0.png";
        _attack[1] = "Assets/Images/Player/Attack/Player_Attack_1.png";
        _attack[2] = "Assets/Images/Player/Attack/Player_Attack_2.png";
        _attack[3] = "Assets/Images/Player/Attack/Player_Attack_3.png";
        _attack[4] = "Assets/Images/Player/Attack/Player_Attack_4.png";
    }

    public override void Execute(Scene scene, float deltaTime, IActionCallback callback)
    {
        if (_player.GetPlayerState() == PlayerState.Idle)
        {
            _image.MoveTo(_player.GetPosition());
            _image.Animate(_idle, 0.75f, 60, true);
            _videoService.Draw(_image, _player.IsMovingRight());
        }
        
        else if (_player.GetPlayerState() == PlayerState.Moving)
        {
            _image.MoveTo(_player.GetPosition());
            _image.Animate(_run, 0.85f, 60, true);
            _videoService.Draw(_image, _player.IsMovingRight());
        }
        
        else if (_player.GetPlayerState() == PlayerState.Attacking)
        {
            _image.MoveTo(_player.GetPosition());
            _image.Animate(_attack, 1f, 60, true);
            _image.SizeTo(new Vector2(48, 32));
            _videoService.Draw(_image, _player.IsMovingRight());
        }
        
    }
}