using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device;

public class OutputAudioDevicesFinder : AudioDeviceFinderBase
{
    public OutputAudioDevicesFinder(IAlcAudioErrorsManagerFactory errorsManagerFactory) : base(errorsManagerFactory)
    {
    }

    public override IEnumerable<string> GetAll()
    {
        return GetAll(AlcGetStringList.DeviceSpecifier);
    }
}