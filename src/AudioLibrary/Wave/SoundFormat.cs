namespace AudioLibrary.Wave;

public enum SoundFormat
{
    /// <summary>1 Channel, 8 bits per sample.</summary>
    Mono8 = 4352, // 0x00001100
    /// <summary>1 Channel, 16 bits per sample.</summary>
    Mono16 = 4353, // 0x00001101
    /// <summary>2 Channels, 8 bits per sample each.</summary>
    Stereo8 = 4354, // 0x00001102
    /// <summary>2 Channels, 16 bits per sample each.</summary>
    Stereo16 = 4355, // 0x00001103
    /// <summary>Multichannel 4.0, 8-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiQuad8Ext = 4612, // 0x00001204
    /// <summary>Multichannel 4.0, 16-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiQuad16Ext = 4613, // 0x00001205
    /// <summary>Multichannel 4.0, 32-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiQuad32Ext = 4614, // 0x00001206
    /// <summary>1 Channel rear speaker, 8-bit data. See Quadrophonic setups. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiRear8Ext = 4615, // 0x00001207
    /// <summary>1 Channel rear speaker, 16-bit data. See Quadrophonic setups. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiRear16Ext = 4616, // 0x00001208
    /// <summary>1 Channel rear speaker, 32-bit data. See Quadrophonic setups. Requires Extension: AL_EXT_MCFORMATS</summary>
    MultiRear32Ext = 4617, // 0x00001209
    /// <summary>Multichannel 5.1, 8-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi51Chn8Ext = 4618, // 0x0000120A
    /// <summary>Multichannel 5.1, 16-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi51Chn16Ext = 4619, // 0x0000120B
    /// <summary>Multichannel 5.1, 32-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi51Chn32Ext = 4620, // 0x0000120C
    /// <summary>Multichannel 6.1, 8-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi61Chn8Ext = 4621, // 0x0000120D
    /// <summary>Multichannel 6.1, 16-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi61Chn16Ext = 4622, // 0x0000120E
    /// <summary>Multichannel 6.1, 32-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi61Chn32Ext = 4623, // 0x0000120F
    /// <summary>Multichannel 7.1, 8-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi71Chn8Ext = 4624, // 0x00001210
    /// <summary>Multichannel 7.1, 16-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi71Chn16Ext = 4625, // 0x00001211
    /// <summary>Multichannel 7.1, 32-bit data. Requires Extension: AL_EXT_MCFORMATS</summary>
    Multi71Chn32Ext = 4626, // 0x00001212
    /// <summary>1 Channel, IMA4 ADPCM encoded data. Requires Extension: AL_EXT_IMA4</summary>
    MonoIma4Ext = 4864, // 0x00001300
    /// <summary>2 Channels, IMA4 ADPCM encoded data. Requires Extension: AL_EXT_IMA4</summary>
    StereoIma4Ext = 4865, // 0x00001301
    /// <summary>Ogg Vorbis encoded data. Requires Extension: AL_EXT_vorbis</summary>
    VorbisExt = 65539, // 0x00010003
    /// <summary>1 Channel, single-precision floating-point data. Requires Extension: AL_EXT_float32</summary>
    MonoFloat32Ext = 65552, // 0x00010010
    /// <summary>2 Channels, single-precision floating-point data. Requires Extension: AL_EXT_float32</summary>
    StereoFloat32Ext = 65553, // 0x00010011
    /// <summary>1 Channel, double-precision floating-point data. Requires Extension: AL_EXT_double</summary>
    MonoDoubleExt = 65554, // 0x00010012
    /// <summary>2 Channels, double-precision floating-point data. Requires Extension: AL_EXT_double</summary>
    StereoDoubleExt = 65555, // 0x00010013
    /// <summary>1 Channel, µ-law encoded data. Requires Extension: AL_EXT_MULAW</summary>
    MonoMuLawExt = 65556, // 0x00010014
    /// <summary>2 Channels, µ-law encoded data. Requires Extension: AL_EXT_MULAW</summary>
    StereoMuLawExt = 65557, // 0x00010015
    /// <summary>1 Channel, A-law encoded data. Requires Extension: AL_EXT_ALAW</summary>
    MonoALawExt = 65558, // 0x00010016
    /// <summary>2 Channels, A-law encoded data. Requires Extension: AL_EXT_ALAW</summary>
    StereoALawExt = 65559, // 0x00010017
    /// <summary>MP3 encoded data. Requires Extension: AL_EXT_mp3</summary>
    Mp3Ext = 65568, // 0x00010020
}