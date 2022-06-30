using System.Numerics;

namespace CSE210_Final.Casting;

public class PlayerController : Actor
{
    
    private bool _enabled = true;
    private Vector2 _position = Vector2.Zero;
    private float _rotation = 0f;
    private float _scale = 1f;
    private Vector2 _size = Vector2.Zero;
    private Color _tint = Color.White();
    private Vector2 _velocity = Vector2.Zero;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color)
    {
        _position = pos;
        _size = pos;
        _tint = color;
    }
}