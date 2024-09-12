using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device;

public abstract class AudioDeviceFinderBase : IDevicesFinder
{
    private readonly IAlcAudioErrorsManagerFactory _errorsManagerFactory;

    protected AudioDeviceFinderBase(IAlcAudioErrorsManagerFactory errorsManagerFactory)
    {
        _errorsManagerFactory = errorsManagerFactory;
    }

    public abstract IEnumerable<string> GetAll();

    protected IEnumerable<string> GetAll(AlcGetStringList specifierString)
    {
        var audioDevice = ALDevice.Null;
        var errorsManager = _errorsManagerFactory.Create(audioDevice);
        var devices = ALC.GetString(audioDevice, specifierString);
        errorsManager.ManageLastError();
        return devices;
    }
}