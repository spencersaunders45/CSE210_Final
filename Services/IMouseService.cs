using System.Numerics;


namespace CSE210_Final.Services
{
    public interface IMouseService
    {
        Vector2 GetCoordinates();
        bool IsButtonDown(MouseButton button);
        bool IsButtonPressed(MouseButton button);
        bool IsButtonReleased(MouseButton button);
        bool IsButtonUp(MouseButton button);
    }
}