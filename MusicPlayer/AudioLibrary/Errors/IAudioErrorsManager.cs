using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Errors;

public interface IAudioErrorsManager
{
    void ManageLastError();
}

public interface IAlAudioErrorsManager : IAudioErrorsManager
{
}

public interface IAlcAudioErrorsManager : IAudioErrorsManager
{
}

public interface IAlcAudioErrorsManagerFactory
{
    IAlcAudioErrorsManager Create(ALDevice device);
}

public class AlcAudioErrorsManagerFactory : IAlcAudioErrorsManagerFactory
{
    public IAlcAudioErrorsManager Create(ALDevice device)
    {
        return new AlcAudioErrorsManager(device);
    }
}