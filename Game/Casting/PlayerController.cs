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
    
    // health & damage stuff
    private Vector2 _knockbackVector;
    private float _knockbackAmount;
    private int _health;
    private Vector2 _hitPosition;
    private float _attackInitiationTime;
    private float _attackInitiationTimer;
    private float _hitboxRadius;
    private bool _canAttack;
    private bool _isAttacking;
    private float _attackTimer;
    private float _maxAttackTime;
    
    private int _isMovingRight;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color, Scene scene)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
        _movementSpeed = 2;
        _currentScene = scene;
        _isMovingRight = 1;
        _isAttacking = false;
        _attackTimer = 0;
        _maxAttackTime = 0.25f;
        _attackInitiationTime = 0.35f;
        _attackInitiationTimer = 0;
        _canAttack = true;
        _hitPosition = new Vector2(16, 0);
        _hitboxRadius = 32;
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
        if (keyboardService.IsKeyDown(KeyboardKey.Down) && !_botCol && !_isAttacking) // Down
        {
            _moveY = _movementSpeed;
            _isMovingY = true;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Up) && !_topCol && !_isAttacking) // Up
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
        if (keyboardService.IsKeyDown(KeyboardKey.Right) && !_rightCol && !_isAttacking) // Right
        {
            _moveX = _movementSpeed;
            _isMovingH = true;
            _isMovingRight = 1;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Left) && !_leftCol && !_isAttacking) // Left
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

        if (keyboardService.IsKeyDown(KeyboardKey.Space) && !_isAttacking && _attackTimer == 0 && _canAttack)
        {
            _isAttacking = true;
            _canAttack = false;

            foreach (Skeleton skeleton in _currentScene.GetAllActors<Skeleton>("skeleton"))
            {
                if (Vector2.Distance(GetCenter() + new Vector2(_hitPosition.X * _isMovingRight, 0), 
                        skeleton.GetCenter()) < _hitboxRadius && skeleton.GetEnabled())
                {
                    skeleton.DealDamage(1, GetCenter());
                }
            }
        }

        if (!_canAttack)
        {
            _attackInitiationTimer += serviceFactory.GetVideoService().GetDeltaTime();
        }

        if (_attackInitiationTimer >= _attackInitiationTime)
        {
            _canAttack = true;
            _attackInitiationTimer = 0;
        }

        if (_isAttacking)
        {
            _attackTimer += serviceFactory.GetVideoService().GetDeltaTime();
            if(_attackTimer >= _maxAttackTime)
            {
                _isAttacking = false;
                _attackTimer = 0;
            }
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
        if (_isMovingH || _isMovingY && !_isAttacking)
            return PlayerState.Moving;
        
        if (_isAttacking)
            return PlayerState.Attacking;
        
        return PlayerState.Idle;
    }
}