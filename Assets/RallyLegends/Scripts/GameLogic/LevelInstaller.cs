using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RallyLegends.CameraSettings;
using RallyLegends.UI;
using RallyLegends.Sound;
using RallyLegends.Control;
using RallyLegends.Objects;
using YG;

namespace RallyLegends.GameLogic
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerCarFollower _cameraFollower;
        [SerializeField] private VolumeController _volumeController;

        [Header("ObjectContainers")]
        [SerializeField] private Container _garage;
        [SerializeField] private Container _botStorage;
        [SerializeField] private Container _mapStorage;
        [Header("UIElements")]

        [SerializeField] private Stopwatch _stopwatch;
        [SerializeField] private Countdown _countdown;
        [SerializeField] private LapCounter _lapCounter;
        [SerializeField] private Speedometer _speedometer;
        [SerializeField] private OverheatWarning _overheatWarning;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private FinishScreen _finishScreen;

        private int _mapId;
        private LoftRoad _map;
        private AnimateCarAlongSpline _playerCar;
        private AnimateCarAlongSpline _botCar;

        public event UnityAction Won;
        public event UnityAction Lost;

        public int MapId => _mapId;

        private void Start()
        {
            GetComponents();
            Collect();
        }

        private void OnEnable()
        {
            _lapCounter.Finished += OnFinishedDisableView;
        }

        private void OnDisable()
        {
            _lapCounter.Finished -= OnFinishedDisableView;
        }

        public bool GetFinishResault() => _playerCar.TotalDistance > _botCar.TotalDistance;

        private UnityAction OnGetFinishResault() => _ = GetFinishResault() ? Won : Lost;

        private void GetComponents()
        {
            _map = _mapStorage.GetItem(YandexGame.savesData.MapIndex).GetComponent<LoftRoad>();
            _mapId = _map.GetComponent<Map>().Id;
            _playerCar = _garage.GetItem(YandexGame.savesData.CarIndex).GetComponent<AnimateCarAlongSpline>();
            _botCar = _botStorage.GetRandomItem().GetComponent<AnimateCarAlongSpline>();
            _playerCar.GetComponentInChildren<Container>().GetItem(YandexGame.savesData.ColorIndex).gameObject.SetActive(true);

            _garage.DeleteAllExcept(_playerCar.GetComponent<Product>());
            _botStorage.DeleteAllExcept(_botCar.GetComponent<Product>());
            _mapStorage.DeleteAllExcept(_map.GetComponent<Product>());

            Destroy(_playerCar.GetComponentInChildren<Image>().gameObject);
        }

        private void Collect()
        {
            _countdown.gameObject.SetActive(true);
            _speedometer.gameObject.SetActive(true);
            _stopwatch.gameObject.SetActive(true);
            _lapCounter.gameObject.SetActive(true);

            _tutorial.gameObject.SetActive(YandexGame.savesData.isFirstSession);

            _stopwatch.TimeStart();

            _playerCar.gameObject.SetActive(true);
            _botCar.gameObject.SetActive(true);
            _map.gameObject.SetActive(true);

            _playerCar.SetContainer(_map.GetComponent<LoftRoad>().Container);
            _botCar.SetContainer(_map.GetComponent<LoftRoad>().Container);

            _playerCar.SetCountdown(_countdown);
            _botCar.SetCountdown(_countdown);

            _playerCar.GetComponent<PlayerSpeedController>().SetOverheatWarning(_overheatWarning);

            _botCar.GetComponent<BotSpeedController>().SetTarget(_playerCar);
            _lapCounter.SetTarget(_playerCar);
            _speedometer.SetTarget(_playerCar);

            _cameraFollower.SetTarget(_playerCar.transform);

            _volumeController.AddSourse(_botCar.GetComponent<AudioSource>());
            _volumeController.AddSourse(_playerCar.GetComponent<AudioSource>());
            _volumeController.AddSourse(_playerCar.GetComponentInChildren<ExplosionStateSwitch>().AudioSource);
        }

        private void OnFinishedDisableView()
        {
            _stopwatch.TimeStop();
            _stopwatch.TimeReset();
            _countdown.ResetCountdown();

            _countdown.gameObject.SetActive(false);
            _speedometer.gameObject.SetActive(false);
            _stopwatch.gameObject.SetActive(false);
            _lapCounter.gameObject.SetActive(false);

            _finishScreen.gameObject.SetActive(true);

            OnGetFinishResault()?.Invoke();
        }
    }
}