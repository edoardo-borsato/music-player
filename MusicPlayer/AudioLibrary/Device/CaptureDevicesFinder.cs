﻿using AudioLibrary.Errors;
using OpenTK.Audio.OpenAL;

namespace AudioLibrary.Device
{
    public class CaptureDevicesFinder : IDevicesFinder
    {
        public IEnumerable<string> GetAll()
        {
            var audioDevice = ALDevice.Null;
            var errorsManager = new AlcAudioErrorsManager(audioDevice);
            var devices = ALC.GetString(audioDevice, AlcGetStringList.CaptureDeviceSpecifier);
            errorsManager.ManageLastError();
            return devices;
        }
    }
}