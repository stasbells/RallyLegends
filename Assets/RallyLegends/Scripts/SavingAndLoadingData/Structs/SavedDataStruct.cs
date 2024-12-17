using System;

namespace RallyLegends.Data.Structs
{
    [Serializable]
    public struct SavedDataStruct
    {
        public WalletData WalletData;
        public ContainerData GarageData;
        public ContainerData MapData;
        public ColorsData ColorsData;
        public SoundData MusicVolumeData;
        public SoundData EffectsVolumeData;
    }
}