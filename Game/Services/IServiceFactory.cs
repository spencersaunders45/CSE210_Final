using CSE210_Final.Game.Casting;

namespace CSE210_Final.Game.Services
{
    public interface IServiceFactory
    {
        IAudioService GetAudioService();
        IKeyboardService GetKeyboardService();
        IMouseService GetMouseService();
        ISettingsService GetSettingsService();
        IVideoService GetVideoService();
    }
}