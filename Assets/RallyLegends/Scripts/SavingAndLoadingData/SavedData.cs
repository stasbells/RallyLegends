using UnityEngine;
using RallyLegends.Objects;
using RallyLegends.GameLogic;
using RallyLegends.Data.Structs;
using RallyLegends.Sound;
using YG;

namespace RallyLegends.Data
{
    public class SavedData : MonoBehaviour
    {
        [Header("SavedData")]
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Container _garage;
        [SerializeField] private Container _maps;
        [SerializeField] private VolumeController _musicVolumeController;
        [SerializeField] private VolumeController _effectsVolumeController;

        private void OnEnable()
        {
            YandexGame.GetDataEvent += GetLoad;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= GetLoad;
        }

        private void Start()
        {
            if (YandexGame.SDKEnabled == true)
                GetLoad();
        }

        private void OnApplicationPause(bool pause)
        {
            if (Application.platform == RuntimePlatform.Android)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        public void Save()
        {
            SavedDataStruct savedData = new()
            {
                WalletData = new WalletData(_wallet),
                GarageData = new ContainerData(_garage.Items),
                MapData = new ContainerData(_maps.Items),
                ColorsData = new ColorsData(_garage.Items),
                MusicVolumeData = new SoundData(_musicVolumeController),
                EffectsVolumeData = new SoundData(_effectsVolumeController)
            };

            string jsonSavedData = JsonUtility.ToJson(savedData, prettyPrint: true);

            YandexGame.savesData.JsonSavedData = jsonSavedData;

            YandexGame.SaveProgress();
        }

        public void SaveWalletData()
        {
            if (YandexGame.savesData.JsonSavedData == null)
                return;

            string jsonSavedData = YandexGame.savesData.JsonSavedData;

            SavedDataStruct savedDataFromJson = JsonUtility.FromJson<SavedDataStruct>(jsonSavedData);

            savedDataFromJson.WalletData.Money += YandexGame.savesData.Reward;
            YandexGame.savesData.Reward = 0;

            jsonSavedData = JsonUtility.ToJson(savedDataFromJson, prettyPrint: true);

            YandexGame.savesData.JsonSavedData = jsonSavedData;
            YandexGame.SaveProgress();
        }

        public void GetLoad()
        {
            if (YandexGame.savesData.JsonSavedData == null)
                return;

            string jsonSavedData = YandexGame.savesData.JsonSavedData;

            SavedDataStruct savedDataFromJson = JsonUtility.FromJson<SavedDataStruct>(jsonSavedData);

            _wallet?.LoadData(savedDataFromJson.WalletData);
            _garage?.LoadData(savedDataFromJson.GarageData);
            _maps?.LoadData(savedDataFromJson.MapData);
            _garage?.LoadData(savedDataFromJson.ColorsData);
            _effectsVolumeController?.LoadData(savedDataFromJson.EffectsVolumeData);
            _musicVolumeController?.LoadData(savedDataFromJson.MusicVolumeData);
        }
    }
}