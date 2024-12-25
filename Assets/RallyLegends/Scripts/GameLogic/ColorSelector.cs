using UnityEngine;
using UnityEngine.UI;
using RallyLegends.UI;
using RallyLegends.Objects;

namespace RallyLegends.GameLogic
{
    public class ColorSelector : Selector
    {
        [SerializeField] private GarageScreen _garageScreen;
        [SerializeField] private CarSelector _carSelector;
        [SerializeField] private Image _colorImage;

        private Car _curentCar;
        private CarColor _currentColor;
        private Container _colors;

        private void Start()
        {
            _curentCar = _carSelector.GetCurrentProduct().GetComponent<Car>();
            _colors = _curentCar.GetComponentInChildren<Container>();
        }

        private void OnEnable()
        {
            _carSelector.CarChanged += OnCarChangedSetCurrent;
        }

        private void OnDisable()
        {
            _carSelector.CarChanged -= OnCarChangedSetCurrent;
        }

        private void Update()
        {
            if (_currentColor.IsBuyed)
                LockImage.gameObject.SetActive(false);
        }

        public override void ChangeProduct(int changer)
        {
            CurrentProductIndex = Mathf.Clamp(CurrentProductIndex += changer, 0, _colors.ItemsCount - 1);
            SelectProduct(CurrentProductIndex);
        }

        public override Product GetCurrentProduct() => _currentColor;

        public override void PayProduct() => _colors.BuyItem(_currentColor);

        protected override void ShowInfo() => LockImage.gameObject.SetActive(!_currentColor.IsBuyed);

        protected override void SelectProduct(int index)
        {
            NextButton.interactable = (index != _colors.ItemsCount - 1);
            PrevButton.interactable = (index != 0);

            for (int i = 0; i < _colors.ItemsCount; i++)
                _colors.GetItem(i).gameObject.SetActive(i == index);

            _currentColor = _colors.GetItem(index).GetComponent<CarColor>();
            _colorImage.sprite = _currentColor.GetComponentInChildren<Image>().sprite;
            _colors.SetCurrentIndex(index);

            if (_garageScreen.GetComponent<CanvasGroup>().alpha == 1f)
                ShowInfo();
        }

        private void OnCarChangedSetCurrent(Car car)
        {
            _curentCar = car;
            _colors = _curentCar.GetComponentInChildren<Container>();
            CurrentProductIndex = _colors.CurrentItemIndex;
            SelectProduct(CurrentProductIndex);
        }
    }
}