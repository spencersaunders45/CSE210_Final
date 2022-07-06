using System.Numerics;

namespace CSE210_Final.Game.Casting;

public class PlayerController : Actor
{
    
    public PlayerController(Vector2 pos, Vector2 size, Color color)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
    }
}