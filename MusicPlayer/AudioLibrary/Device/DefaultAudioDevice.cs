using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device
{
    public class DefaultAudioDevice : AudioDevice
    {
        public DefaultAudioDevice(IAlcAudioErrorsManagerFactory errorsManagerFactory) : base(GetDefaultOutputDeviceName(), errorsManagerFactory)
        {
        }

        #region Utility Methods

        private static string GetDefaultOutputDeviceName()
        {
            return ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
        }

        #endregion
    }
}