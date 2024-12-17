using UnityEngine;
using UnityEngine.Events;
using RallyLegends.Objects;
using YG;

namespace RallyLegends.GameLogic
{
    public class CarSelector : Selector
    {
        [SerializeField] private UI.Screen _screen;
        [SerializeField] private Container _garage;

        private Car _currentCar;

        public event UnityAction<Car> CarChanged;

        private void OnEnable()
        {
            SelectProduct(CurrentProductIndex);
        }

        private void OnDisable()
        {
            for (int i = 0; i < _garage.ItemsCount; i++)
                _garage.GetItem(i).gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentCar.IsBuyed)
                LockImage.gameObject.SetActive(false);
        }

        public override void ChangeProduct(int changer)
        {
            CurrentProductIndex += changer;
            SelectProduct(CurrentProductIndex);
        }

        public override Product GetCurrentProduct() => _currentCar;

        public override void PayProduct() => _garage.BuyItem(_currentCar);

        public void SaveCarIndexes()
        {
            YandexGame.savesData.CarIndex = CurrentProductIndex;
            YandexGame.savesData.ColorIndex = GetCurrentColorIndex();
        }

        protected override void SelectProduct(int index)
        {
            NextButton.interactable = (index != _garage.ItemsCount - 1);
            PrevButton.interactable = (index != 0);

            for (int i = 0; i < _garage.ItemsCount; i++)
                _garage.GetItem(i).gameObject.SetActive(i == index);

            _currentCar = _garage.GetItem(index).GetComponent<Car>();

            CarChanged?.Invoke(_currentCar);

            SaveCarIndexes();

            if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
                ShowInfo();
        }

        protected override void ShowInfo() => LockImage.gameObject.SetActive(!_currentCar.IsBuyed);

        private int GetCurrentColorIndex() => _currentCar.GetComponentInChildren<Container>().TryGetIndexOfActiveProduct();
    }
}