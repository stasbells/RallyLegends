using System;
using RallyLegends.Sound;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct SoundData
    {
        public float VolumeValue;

        public SoundData(VolumeController volumeController)
        {
            VolumeValue = volumeController.VolumeValue;
        }
    }
}