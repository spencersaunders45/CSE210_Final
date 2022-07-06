using System.Numerics;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Casting;

public class SolidWall : Actor
{
    public SolidWall(Vector2 pos, Vector2 size, Color color, Scene scene)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
    }
}