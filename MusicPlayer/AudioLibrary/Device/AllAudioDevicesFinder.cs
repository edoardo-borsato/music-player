using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device;

public class AllAudioDevicesFinder : AudioDeviceFinderBase
{
    public AllAudioDevicesFinder(IAlcAudioErrorsManagerFactory alcAudioErrorsManagerFactory) : base(alcAudioErrorsManagerFactory)
    {
    }

    public override IEnumerable<string> GetAll()
    {
        return GetAll(AlcGetStringList.AllDevicesSpecifier);
    }
}