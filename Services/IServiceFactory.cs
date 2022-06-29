using CSE210_Final.Casting;

namespace CSE210_Final.Services
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