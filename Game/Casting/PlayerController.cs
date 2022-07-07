using System.Numerics;
using System.Runtime.InteropServices;
using CSE210_Final.Game.Scripting;
using CSE210_Final.Game.Services;

namespace CSE210_Final.Game.Casting;

public class PlayerController : Actor
{

    private int _movementSpeed;
    private int _moveY;
    private int _moveX;
    private Scene _currentScene;
    private bool topCol;
    private bool botCol;
    private bool rightCol;
    private bool leftCol;
    
    public PlayerController(Vector2 pos, Vector2 size, Color color, Scene scene)
    {
        MoveTo(pos);
        SizeTo(size);
        Tint(color);
        _movementSpeed = 4;
        _currentScene = scene;
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
            topCol = wall.Overlaps(new Vector2(GetLeft() + 2, GetTop() - 2)) || // Top
                     wall.Overlaps(new Vector2(GetRight() - 2, GetTop() - 2));

            botCol = wall.Overlaps(new Vector2(GetLeft() + 2, GetBottom() + 2)) || // Bottom
                     wall.Overlaps(new Vector2(GetRight() - 2, GetBottom() + 2));

            if (topCol || botCol)    // Break the loop if collision is detected.
                break;                                      /* If this doesn't happen, collision will only work with 
                                                                the last object in the list. */
        }
        // Loop through every wall in the scene to test collision against.
        //Horizontal Collision
        foreach (SolidWall wall in allWalls) 
        {
            // There are two different spots that get checked for each side in terms of collision
            // to make collision more precise.
            rightCol = wall.Overlaps(new Vector2(GetRight() + 2, GetTop() + 2)) || // Right
                       wall.Overlaps(new Vector2(GetRight() + 2, GetBottom() - 2));

            leftCol = wall.Overlaps(new Vector2(GetLeft() - 2, GetTop() + 2)) || // Left
                      wall.Overlaps(new Vector2(GetLeft() - 2, GetBottom() - 2));

            if (rightCol || leftCol)    // Break the loop if collision is detected.
                break;                                      /* If this doesn't happen, collision will only work with 
                                                                the last object in the list. */
        }
        
        // Vertical
        if (keyboardService.IsKeyDown(KeyboardKey.Down) && !botCol) // Down
            _moveY = _movementSpeed;
        else if (keyboardService.IsKeyDown(KeyboardKey.Up) && !topCol) // Up
            _moveY = -_movementSpeed;
        else
            _moveY = 0;
        
        // Horizontal Movement
        if (keyboardService.IsKeyDown(KeyboardKey.Right) && !rightCol) // Right
            _moveX = _movementSpeed;
        else if (keyboardService.IsKeyDown(KeyboardKey.Left) && !leftCol) // Left
            _moveX = -_movementSpeed;
        else
            _moveX = 0;
        
        
        Steer(_moveX, _moveY);
        Move();
    }
}