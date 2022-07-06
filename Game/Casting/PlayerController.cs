using System.Numerics;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Casting;

public class PlayerController : Actor
{

    private int _movementSpeed;
    private int _moveY;
    private int _moveX;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
        _movementSpeed = 7;
    }

    public void Update(IServiceFactory serviceFactory)
    {
        IKeyboardService keyboardService = serviceFactory.GetKeyboardService();
        
        // Vertical
        if (keyboardService.IsKeyDown(KeyboardKey.Down)) // Down
            _moveY = _movementSpeed;
        else if (keyboardService.IsKeyDown(KeyboardKey.Up)) // Up
            _moveY = -_movementSpeed;
        else
            _moveY = 0;
        
        // Hoizontal Movement
        if (keyboardService.IsKeyDown(KeyboardKey.Right)) // Right
            _moveX = _movementSpeed;
        else if (keyboardService.IsKeyDown(KeyboardKey.Left)) // Left
            _moveX = -_movementSpeed;
        else
            _moveX = 0;
        
        
        Steer(_moveX, _moveY);
        Move();
    }
}