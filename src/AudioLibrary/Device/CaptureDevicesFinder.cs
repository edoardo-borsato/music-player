using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device;

public class CaptureDevicesFinder : AudioDeviceFinderBase
{
    public CaptureDevicesFinder(IAlcAudioErrorsManagerFactory alcAudioErrorsManagerFactory) : base(alcAudioErrorsManagerFactory)
    {
    }

    public override IEnumerable<string> GetAll()
    {
        return GetAll(AlcGetStringList.CaptureDeviceSpecifier);
    }
}