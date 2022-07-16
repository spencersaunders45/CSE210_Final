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
    private bool _isDead;
    
    // health & damage stuff
    private Vector2 _knockbackVector;
    private float _knockbackDistance;
    private Vector2 _hitPosition;
    private float _attackInitiationTime;
    private float _attackInitiationTimer;
    private float _hitboxRadius;
    private bool _canAttack;
    private bool _isAttacking;
    private float _attackTimer;
    private float _maxAttackTime;
    private float _invincibleLength;
    private float _invincibilityTimer;
    private bool _invincible;
    private string _damageSound;
    
    private int _isMovingRight;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color, Scene scene, int health)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
        _health = health;
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

        _knockbackDistance = 36;

        _invincibleLength = 1.25f;
        _invincibilityTimer = 0;
        _invincible = false;
    }

    public void Update(IServiceFactory serviceFactory)
    {
        IKeyboardService keyboardService = serviceFactory.GetKeyboardService();
        
        HandleMovement(keyboardService);
        HandleAttack(serviceFactory, keyboardService);
        HandleInvincibility(serviceFactory);
        HandleDamage(serviceFactory.GetSettingsService(), serviceFactory.GetAudioService());
        
        _knockbackVector = Vector2.Lerp(_knockbackVector, Vector2.Zero, 0.5f);
        
        Steer(_moveX - _knockbackVector.X, _moveY - _knockbackVector.Y);
        Move();
    }

    private void HandleDamage(ISettingsService settingService, IAudioService audioService)
    {
        List<Skeleton> skeletons = _currentScene.GetAllActors<Skeleton>("skeleton");
        
        if(_damageSound == null)
            _damageSound = settingService.GetString("player_damage_sound");

        foreach (Skeleton skeleton in skeletons)
        {
            if (Vector2.Distance(GetCenter(), skeleton.GetCenter()) < 14 && skeleton.GetEnabled())
            {
                DealDamage(1, skeleton.GetCenter(), audioService);
                CheckDead();
            }
        }
    }
    private void DealDamage(int damage, Vector2 damagePos, IAudioService audioService)
    {
        if(!_invincible)
        {
            _health -= damage;

            // Set up knockback
            _knockbackVector = damagePos - GetCenter();
            _knockbackVector = Vector2.Normalize(_knockbackVector) * _knockbackDistance;

            _invincibilityTimer = 0;
            _invincible = true;
            audioService.PlaySound(_damageSound);
        }
    }

    private void CheckDead()
    {
        if (_health <= 0)
        {
            _isDead = true;
        }
    }

    private void HandleMovement(IKeyboardService keyboardService)
    {
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
        if (keyboardService.IsKeyDown(KeyboardKey.Down) && !_botCol && !_isAttacking && !_isDead) // Down
        {
            _moveY = _movementSpeed;
            _isMovingY = true;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Up) && !_topCol && !_isAttacking && !_isDead) // Up
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
        if (keyboardService.IsKeyDown(KeyboardKey.Right) && !_rightCol && !_isAttacking && !_isDead) // Right
        {
            _moveX = _movementSpeed;
            _isMovingH = true;
            _isMovingRight = 1;
        }
        else if (keyboardService.IsKeyDown(KeyboardKey.Left) && !_leftCol && !_isAttacking && !_isDead) // Left
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
    }

    private void HandleInvincibility(IServiceFactory serviceFactory)
    {
        if (_invincible)
        {
            _invincibilityTimer += serviceFactory.GetVideoService().GetDeltaTime();
        }

        if (_invincibilityTimer >= _invincibleLength)
        {
            _invincibilityTimer = 0;
            _invincible = false;
        }
    }

    private void HandleAttack(IServiceFactory serviceFactory, IKeyboardService keyboardService)
    {
        if (keyboardService.IsKeyDown(KeyboardKey.Space) && !_isAttacking && _attackTimer == 0 && _canAttack && !_isDead)
        {
            _isAttacking = true;
            _canAttack = false;
            Vector2 hitPos = new Vector2(_hitPosition.X * _isMovingRight, 0);
            // Attack regular skeletons
            foreach (Skeleton skeleton in _currentScene.GetAllActors<Skeleton>("skeleton"))
            {
                if (Vector2.Distance(GetCenter() + hitPos, 
                        skeleton.GetCenter()) < _hitboxRadius && skeleton.GetEnabled())
                {
                    skeleton.DealDamage(1, GetCenter());
                }
            }
            // Attack Boss
            Skeleton boss = _currentScene.GetFirstActor<Skeleton>("boss");
            if (Vector2.Distance(GetCenter() + hitPos, boss.GetCenter()) < _hitboxRadius && boss.GetEnabled())
            {
                boss.DealDamage(1, GetCenter());
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

        if (_isDead)
            return PlayerState.Dead;
        
        return PlayerState.Idle;
    }
}