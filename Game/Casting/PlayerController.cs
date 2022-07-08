using System.Numerics;
using System.Runtime.InteropServices;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Casting;

public enum PlayerState
{
    Idle,
    Moving,
    Attacking,
    Dead
}

/// <summary>
/// The main player actor.
/// </summary>
public class PlayerController : Actor
{

    private int _movementSpeed;
    private int _moveY;
    private int _moveX;
    private Scene _currentScene;
    private bool _topCol;
    private bool _botCol;
    private bool _rightCol;
    private bool _leftCol;
    private bool _isMovingY;
    private bool _isMovingH;
    private int _isMovingRight;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color, Scene scene)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
        _movementSpeed = 3;
        _currentScene = scene;
        _isMovingRight = 1;
    }

    public void Update(IServiceFactory serviceFactory)
    {
        IKeyboardService keyboardService = serviceFactory.GetKeyboardService();
        List<SolidWall> allWalls = _currentScene.GetAllActors<SolidWall>("wall");
        
        // Loop through every wall in the scene to test collision against.
        // Vertical Collision
        foreach (SolidWall wall in allWalls) 
        {
            // There are two different spots that get checked for each side in terms of collision
            // to make collision more precise.
            _topCol = wall.Overlaps(new Vector2(GetLeft() + 2, GetTop() - 2)) || // Top
                     wall.Overlaps(new Vector2(GetRight() - 2, GetTop() - 2));

            _botCol = wall.Overlaps(new Vector2(GetLeft() + 2, GetBottom() + 2)) || // Bottom
                     wall.Overlaps(new Vector2(GetRight() - 2, GetBottom() + 2));

            if (_topCol || _botCol)    // Break the loop if collision is detected.
                break;                                      /* If this doesn't happen, collision will only work with 
                                                                the last object in the list. */
        }
        // Loop through every wall in the scene to test collision against.
        //Horizontal Collision
        foreach (SolidWall wall in allWalls) 
        {
            // There are two different spots that get checked for each side in terms of collision
            // to make collision more precise.
            _rightCol = wall.Overlaps(new Vector2(GetRight() + 2, GetTop() + 2)) || // Right
                       wall.Overlaps(new Vector2(GetRight() + 2, GetBottom() - 2));

            _leftCol = wall.Overlaps(new Vector2(GetLeft() - 2, GetTop() + 2)) || // Left
                      wall.Overlaps(new Vector2(GetLeft() - 2, GetBottom() - 2));

            if (_rightCol || _leftCol)    // Break the loop if collision is detected.
                break;                                      /* If this doesn't happen, collision will only work with 
                                                                the last object in the list. */
        }
        
        // Vertical
        if (keyboardService.IsKeyDown(KeyboardKey.Down) && !_botCol) // Down
        {
            _moveY = _movementSpeed;
            _isMovingY = true;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Up) && !_topCol) // Up
        {
            _moveY = -_movementSpeed;
            _isMovingY = true;
        }
        else
        {
            _moveY = 0;
            _isMovingY = false;
        }
        
        // Horizontal Movement
        if (keyboardService.IsKeyDown(KeyboardKey.Right) && !_rightCol) // Right
        {
            _moveX = _movementSpeed;
            _isMovingH = true;
            _isMovingRight = 1;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Left) && !_leftCol) // Left
        {
            _moveX = -_movementSpeed;
            _isMovingH = true;
            _isMovingRight = -1;
        }
        else
        {
            _moveX = 0;
            _isMovingH = false;
        }
        
        Steer(_moveX, _moveY);
        Move();
    }

    public int IsMovingRight()
    {
        return _isMovingRight;
    }

    public PlayerState GetPlayerState()
    {
        if (_isMovingH || _isMovingY)
            return PlayerState.Moving;
        else
            return PlayerState.Idle;
    }
}